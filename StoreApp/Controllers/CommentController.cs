using Entities.Dtos;
using Entities.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Services.Contracts;

namespace StoreApp.Controllers
{
    public class CommentController : Controller
    {
        private readonly IServiceManager _manager;
        private readonly UserManager<IdentityUser> _userManager;

        public CommentController(IServiceManager manager, UserManager<IdentityUser> userManager)
        {
            _manager = manager;
            _userManager = userManager;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddComment([FromForm] CommentDtoForCreate commentDto)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("Get", "Product", new { id = commentDto.ProductId });
            }

            //var comment = new Comment
            //{
            //    ProductId = commentDto.ProductId,
            //    Rating = commentDto.Rating,
            //    CommentText = commentDto.CommentText,
            //    UserId = commentDto.UserId,
            //    CreatedDate = DateTime.Now
            //};

            _manager.CommentService.CreateComment(commentDto);

            return RedirectToAction("Get", "Product", new { id = commentDto.ProductId }); 
        }
        [HttpPost]
        [ValidateAntiForgeryToken]  
        public IActionResult UpdateComment([FromForm] CommentDtoForUpdate commentDto)
        {
            var comment = _manager.CommentService.GetComment(commentDto.CommentId, false);
            if (!ModelState.IsValid)
            {
                return RedirectToAction("Get", "Product", new { id = commentDto.ProductId });
            }
            _manager.CommentService.UpdateComment(commentDto, commentDto.UserId);
            return RedirectToAction("Get", "Product", new { id = commentDto.ProductId });
        }

        public IActionResult DeleteComment([FromForm] int CommentId, string UserId,int ProductId)
        {
            var comment = _manager.CommentService.GetComment(CommentId, false);
            if(comment is not null)
            {
                _manager.CommentService.DeleteComment(CommentId, UserId);
            }
            return RedirectToAction("Get", "Product", new { id = ProductId });

        }
    }
}
