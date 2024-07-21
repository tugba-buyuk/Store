using Entities.Dtos;
using Entities.Models;
using Entities.RequestParameters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Services.Contracts;
using StoreApp.Models;

namespace StoreApp.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class ProductController : Controller
    {
        private readonly IServiceManager _manager;

        public ProductController(IServiceManager manager)
        {
            _manager = manager;
        }

        public IActionResult Index([FromQuery] ProductRequestParameters p)
        {
            ViewData["Title"] = "Products";


            var products = _manager.ProductService.GettAllProductsWithDetails(p);
            var pagination = new Pagination()
            {
                CurrentPage = p.PageNumber,
                ItemsPerPage = p.PageSize,
                TotalItems = _manager.ProductService.GetAllProducts(false).Count()
            };
            return View(new ProductListViewModel()
            {
                Products = products,
                Pagination = pagination
            });
        }



        public IActionResult Create()
        {
            ViewBag.Colors = GetColorsSelectList();
            ViewBag.Categories = GetCategoriesSelectList();
            TempData["info"] = "Please fill the form";
            return View();
        }

        private SelectList GetColorsSelectList()
        {
            return new SelectList(_manager.ColorService.GetAllColors(false), "ColorId", "ColorName", 1);
        }

        private SelectList GetCategoriesSelectList()
        {
            return new SelectList(_manager.CategoryService.GetAllCategories(false), "Id", "CategoryName", 1);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([FromForm] ProductDtoForInsertion productDto, List<IFormFile> files, IFormFile mainfile)
        {
            if (ModelState.IsValid)
            {
                if (mainfile is not null)
                {
                    string path = Path.Combine(Directory.GetCurrentDirectory(),
                    "wwwroot", "images", mainfile.FileName);

                    using (var stream = new FileStream(path, FileMode.Create))
                    {
                        await mainfile.CopyToAsync(stream);
                    }
                    productDto.MainImageUrl = String.Concat("/images/", mainfile.FileName);
                }
                var imageUrls = new List<string>();
                foreach (var file in files)
                {
                    if (file != null && file.Length > 0)
                    {
                        string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", file.FileName);
                        using (var stream = new FileStream(path, FileMode.Create))
                        {
                            await file.CopyToAsync(stream);
                        }
                        imageUrls.Add(String.Concat("/images/", file.FileName));
                    }
                }
                imageUrls.Add(productDto.MainImageUrl);
                

                // Update the DTO to include image URLs
                productDto.Images = imageUrls.Select(url => new PrdImage { Url = url }).ToList();
                productDto.ImageUrls = imageUrls;

                _manager.ProductService.CreateProduct(productDto);
                return RedirectToAction("Index");
            }
            return View();

        }
        public IActionResult Update([FromRoute(Name = "id")] int id)
        {
            ViewBag.Categories = GetCategoriesSelectList();
            ViewBag.Colors = GetColorsSelectList();
            var model = _manager.ProductService.GetOneProductForUpdate(id, false);
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update([FromForm] ProductDtoForUpdate productDto, List<IFormFile> files, [FromRoute(Name = "id")] int id)
        {
            var model = _manager.ProductService.GetOneProduct(id, false);
            if (productDto.Files.Count > 0 && files.Count > 0 )
            {
                var imageUrls = new List<string>();
                if (ModelState.IsValid)
                {
                    foreach (var file in files)
                    {
                        if (file != null && file.Length > 0)
                        {
                            string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", file.FileName);
                            using (var stream = new FileStream(path, FileMode.Create))
                            {
                                await file.CopyToAsync(stream);
                            }
                            imageUrls.Add(String.Concat("/images/", file.FileName));
                        }
                    }
                   
                }
                productDto.Images = imageUrls.Select(url => new PrdImage { Url = url }).ToList();
                productDto.ImageUrls = imageUrls;

                _manager.ProductService.UpdateOneProduct(productDto);
                return RedirectToAction("Index");

            }
            else
            {
                productDto.Images = model.Images;
                productDto.ImageUrls = model.ImageUrls;
                _manager.ProductService.UpdateOneProduct(productDto);
                return RedirectToAction("Index");
            }
        }

        [HttpGet]
        public IActionResult Delete([FromRoute(Name = "id")] int id)
        {
            _manager.ProductService.DeleteOneProduct(id);
            TempData["danger"] = "The product has been deleted.";
            return RedirectToAction("Index");
        }

    }
}
