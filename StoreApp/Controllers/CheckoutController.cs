using Microsoft.AspNetCore.Mvc;

namespace StoreApp.Controllers
{
    public class CheckoutController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
