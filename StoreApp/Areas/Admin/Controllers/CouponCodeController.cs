using Entities.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Contracts;

namespace StoreApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class CouponCodeController : Controller
    {
        private readonly IServiceManager _manager;

        public CouponCodeController(IServiceManager manager)
        {
            _manager = manager;
        }

        public IActionResult Index()
        {
            var couponCodes = _manager.CouponCodeService.GetAllCouponCodes(false);
            return View(couponCodes);
        }
        public IActionResult Create()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([FromForm] CouponCodeDtoForCreate couponCodeDto)
        {
            if(ModelState.IsValid)
            {
                _manager.CouponCodeService.CreateCouponCode(couponCodeDto);
                RedirectToAction("Index");
            }
            return View();
        }

        public IActionResult Update([FromRoute(Name="id")] int id)
        {
            var couponCode = _manager.CouponCodeService.GetOneCouponCodeForUpdate(id, false);
            return View(couponCode);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update([FromForm] CouponCodeDtoForUpdate couponCodeDto)
        {
            if (ModelState.IsValid)
            {
                _manager.CouponCodeService.UpdateOneCouponCode(couponCodeDto);
                RedirectToAction("Index");
            }
            return View();
        }

        public IActionResult Delete([FromRoute(Name ="id")] int id)
        {
            _manager.CouponCodeService.DeleteOneCouponCode(id);
            return RedirectToAction("Index");
        }
        public IActionResult Activate([FromRoute(Name ="id")] int id)
        {
            _manager.CouponCodeService.ChangeActivityCouponCode(id);
            return RedirectToAction("Index");
        }




    }
}
