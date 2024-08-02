using Entities.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Contracts;

namespace StoreApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class OrderController : Controller
    {
        private readonly IServiceManager _manager;
        private readonly IEmailService _emailService;

        public OrderController(IServiceManager manager, IEmailService emailService)
        {
            _manager = manager;
            _emailService = emailService;
        }

        public IActionResult Index()
        {
            var orders = _manager.OrderService.Orders;
            return View(orders);
        }
        [HttpPost]
        public async Task<IActionResult> Complete([FromForm] int id)
        {
            _manager.OrderService.Complete(id);
            var order = _manager.OrderService.GetOneOrder(id);
            var TotalAmount = order.TotalPrice;
            var userEmail = order.Email;
            var emailMessage = new EmailMessageModel(
                       toAddress: userEmail,
                       subject: "Your order is shipped",
                       body: $"Your {TotalAmount} dollar purchase from www.tugba.com has been shipped. You can track your cargo on our website."
                   );

            await _emailService.Send(emailMessage);
            return RedirectToAction("Index");
        }
    }
}
