using Entities.Models;
using Microsoft.AspNetCore.Mvc;
using Services.Contracts;
using StoreApp.Models;
using Stripe;
using Stripe.Checkout;
using Twilio.TwiML.Fax;

namespace StoreApp.Controllers
{
    public class CheckoutController : Controller
    {
        private readonly IServiceManager _manager;
        private readonly Cart _cart;

        public CheckoutController(IServiceManager manager, Cart cart)
        {
            _manager = manager;
            _cart = cart;
        }
        public IActionResult Index(Cart cart, string couponCode)
        {
            cart = SessionCart.GetCart(HttpContext.RequestServices);
            var countries = _manager.CountryService.GetAllCountries(false);
            var cities = _manager.CityService.GetAllCities(false);
            return View(new CheckoutViewModel
            {
                Cart = cart,
                Countries = countries,
                Cities = cities,
                CouponCode = couponCode
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CompleteOrder([FromForm] Order order, [FromForm] string couponCode)
        {
            if (_cart.Lines.Count == 0)
            {
                ModelState.AddModelError("", "Sorry your cart is empty.");
            }
            if (ModelState.IsValid)
            {
                order.Lines = _cart.Lines.ToArray();
                order.DiscountAmount = _cart.Discount;
                order.CouponCode = couponCode;
                _manager.OrderService.SaveOrder(order);

                _cart.Clear();
                var orderId = order.OrderId;
                return RedirectToAction("Payment", new { orderId = orderId });
            }
            else
            {
                return View();
            }

        }
        public IActionResult Payment(int orderId)
        {
            var order = _manager.OrderService.GetOneOrder(orderId);
            var domain = "http://localhost:5260";
            var couponCode = order.CouponCode;
            var discount = order.DiscountAmount;


            var options = new SessionCreateOptions
            {
                SuccessUrl = $"{domain}/Checkout/Success?orderId={order.OrderId}",
                CancelUrl = domain + "/account/login",
                LineItems = new List<SessionLineItemOptions>(),
                Mode = "payment"
            };

            foreach (var item in order.Lines)
            {
                var sessionListItem = new SessionLineItemOptions
                {
                    PriceData = new SessionLineItemPriceDataOptions
                    {
                        UnitAmount = (long)((item.Product.Price * item.Quantity) * 100),
                        Currency = "usd",
                        ProductData = new SessionLineItemPriceDataProductDataOptions
                        {
                            Name = item.Product.ProductName,
                        }

                    },
                    Quantity = item.Quantity
                };
                options.LineItems.Add(sessionListItem);
            }
            var couponService = new CouponService();
            Coupon stripeCoupon = null;
            try
            {
                stripeCoupon = couponService.Get(couponCode);
            }
            catch (Exception)
            {
                stripeCoupon = null;
            }

            if (stripeCoupon != null)
            {
                options.Discounts = new List<SessionDiscountOptions>
                {
                    new SessionDiscountOptions
                    {
                        Coupon = stripeCoupon.Id
                    }
                };
                
            }
            var service = new SessionService();
            Session session = service.Create(options);
            Response.Headers.Add("Location", session.Url);
            return new StatusCodeResult(303);
        }

        public async Task<IActionResult> Success([FromQuery] int orderId)
        {
            var order=_manager.OrderService.GetOneOrder(orderId);

            var emailMessage = new EmailMessageModel(
                toAddress: order.Email,
                subject: "Payment Successfully Completed",
                body: $@"
                Dear {order.FullName},
    
                Thank you for your payment! Your order has been successfully processed.

                Order Details:
                - Order ID: {order.OrderId}
                - Total Amount: ${order.TotalPrice}

                Items Purchased:
                {string.Join(Environment.NewLine, order.Lines.Select(item =>
                    $"- {item.Product.ProductName} - Quantity: {item.Quantity} - Price: ${item.Product.Price}"))}

                If you have any questions or need further assistance, please feel free to contact us.

                Thank you for choosing our service!
                "
            );
            var message = $"Dear {order.FullName}, your order of {order.TotalPrice} dollars has been successfully received.It will be shipped as soon as possible.Thank you for choosing us.";
            await _manager.EmailService.Send(emailMessage);
            _manager.SMSService.SendSms(order.PhoneNumber, message);
            return View(order);
        }
    }
}
