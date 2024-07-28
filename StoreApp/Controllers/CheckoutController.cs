using Entities.Models;
using Microsoft.AspNetCore.Mvc;
using Services.Contracts;
using StoreApp.Models;
using Stripe.Checkout;

namespace StoreApp.Controllers
{
    public class CheckoutController : Controller
    {
        private readonly IServiceManager _manager;
        private readonly Cart _cart;

        public CheckoutController(IServiceManager manager ,Cart cart)
        {
            _manager = manager;
            _cart = cart;
        }
        public IActionResult Index(Cart cart)
        {
            cart = SessionCart.GetCart(HttpContext.RequestServices);
            var countries = _manager.CountryService.GetAllCountries(false);
            var cities=_manager.CityService.GetAllCities(false);
            return View(new CheckoutViewModel
            {
                Cart = cart,
                Countries= countries,
                Cities=cities
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public IActionResult CompleteOrder([FromForm] Order order)
        {
            if (_cart.Lines.Count == 0)
            {
                ModelState.AddModelError("", "Sorry your cart is empty.");
            }
            if (ModelState.IsValid)
            {
                order.Lines = _cart.Lines.ToArray();
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
            var order=_manager.OrderService.GetOneOrder(orderId);
            var domain = "http://localhost:5260";

            var options = new SessionCreateOptions
            {
                SuccessUrl = domain + $"Checkout/Success",
                CancelUrl = domain + "Checkout/Login",
                LineItems = new List<SessionLineItemOptions>(),
                Mode = "payment"
            };
            
            foreach(var item in order.Lines)
            {
                var sessionListItem = new SessionLineItemOptions
                {
                    PriceData = new SessionLineItemPriceDataOptions
                    {
                        UnitAmount = (long)(order.TotalPrice),
                        Currency = "TL",
                        ProductData = new SessionLineItemPriceDataProductDataOptions
                        {
                            Name = item.Product.ProductName,
                        }

                    },
                    Quantity=item.Quantity
                };
                options.LineItems.Add(sessionListItem);
            }
            var service = new SessionService();
            Session session = service.Create(options);
            Response.Headers.Add("Location",session.Url);
            return new StatusCodeResult(303);
        }
    }
}
