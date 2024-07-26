using Entities.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Services.Contracts;
using StoreApp.Infrastructure.Extensions;
using StoreApp.Models;

namespace StoreApp.Pages
{
    public class CartModel : PageModel
    {
        private readonly IServiceManager _manager;
        private readonly Cart _cart;

        public Cart Cart { get; set; }
        public string ReturnUrl { get; set; } = "/";

        public CartModel(IServiceManager manager, Cart cartService)
        {
            _manager = manager;
            Cart = cartService;
        }

        public void OnGet(string returnUrl)
        {
            ReturnUrl = returnUrl ?? "/";
            //Session içine cart mevcut ise onu al deðilse yeni oluþtur.
            //Cart = HttpContext.Session.GetJson<Cart>("cart") ?? new Cart();
        }


        [ValidateAntiForgeryToken]
        public IActionResult OnPost(int productId, string returnUrl,string productSize, string productColor,int productQuantity)
        {
            Product? product = _manager.ProductService.GetOneProduct(productId, false);
            if (product is not null)
            {
                //Cart = HttpContext.Session.GetJson<Cart>("cart") ?? new Cart();
                Cart.AddItem(product, productQuantity,productColor,productSize);
                //HttpContext.Session.SetJson<Cart>("cart",Cart);
            }
            return RedirectToPage(new {returnUrl=returnUrl});
        }

        [ValidateAntiForgeryToken]
        public IActionResult OnPostRemove(int id, string returnUrl)
        {
            //Cart = HttpContext.Session.GetJson<Cart>("cart") ?? new Cart();

            var line = Cart.Lines.FirstOrDefault(cl => cl.Product.Id == id);
            if (line != null)
            {
                Cart.RemoveLine(line.Product);
                //HttpContext.Session.SetJson<Cart>("cart", Cart);
            }
            return Page();
        }

        [ValidateAntiForgeryToken]
        public IActionResult OnPostApplyCoupon(decimal cartTotal,string couponCode)
        {
            var coupon = _manager.CouponCodeService.GetCouponCodeByName(couponCode, false);
            var cart = SessionCart.GetCart(HttpContext.RequestServices);
            if (coupon is null || coupon.IsActive == false)
            {
                TempData["CouponMessage"] = "Invalid coupon code.";
            }
            else
            {
                decimal discountAmountDecimal = (cartTotal * coupon.CouponCodeDiscount)/100;
                int discountAmount = (int)Math.Round(discountAmountDecimal);

                cart.ApplyDiscount(discountAmount);
                var newTotal =cart.ComputeTotalValue();
                cart.DiscountedTotalPrice = newTotal;
                HttpContext.Session.SetJson<Cart>("cart", cart);
                Cart = SessionCart.GetCart(HttpContext.RequestServices);
                TempData["CouponMessageSuccess"] = "Coupon applied successfully.";
                
            }
            
            return Page();
        }
    }
}
