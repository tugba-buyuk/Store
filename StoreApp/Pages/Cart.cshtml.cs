using Entities.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Services.Contracts;
using StoreApp.Infrastructure.Extensions;

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
        public IActionResult OnPost(int productId, string returnUrl)
        {
            Product? product = _manager.ProductService.GetOneProduct(productId, false);
            if (product is not null)
            {
                //Cart = HttpContext.Session.GetJson<Cart>("cart") ?? new Cart();
                Cart.AddItem(product, 1);
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
    }
}
