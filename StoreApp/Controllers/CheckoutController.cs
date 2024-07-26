using Entities.Models;
using Microsoft.AspNetCore.Mvc;
using Services.Contracts;
using StoreApp.Models;

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
                return RedirectToPage("/Complete", new { OrderId = order.OrderId });
            }
            else
            {
                return View();
            }

        }
    }
}
