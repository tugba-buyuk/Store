﻿using Entities.Models;
using Entities.RequestParameters;
using Microsoft.AspNetCore.Mvc;
using Repositories;
using Repositories.Contracts;
using Services.Contracts;
using StoreApp.Models;

namespace StoreApp.Controllers
{
    public class ProductController : Controller
    {
        //Dependency Injection
        private readonly IServiceManager _manager;

        public ProductController(IServiceManager manager)
        {
            _manager = manager;
        }

        public IActionResult Index(ProductRequestParameters p)
        {
            var products = _manager.ProductService.GettAllProductsWithDetails(p).ToList();
            var pagination = new Pagination()
            {
                CurrentPage = p.PageNumber,
                ItemsPerPage=p.PageSize,
                TotalItems=_manager.ProductService.GetAllProducts(false).Count()

            };
            return View(new ProductListViewModel()
            {
                Pagination= pagination,
                Products= products
            });
        }

        public IActionResult Get([FromRoute(Name ="id")] int id)
        {
            var product= _manager.ProductService.GetOneProduct(id, false);
            var comments=_manager.CommentService.GetCommentsForProduct(id,false);
            var model = new ProductDetailViewModel
            {
                Product = product,
                Comments= comments
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SearchingBar([FromForm] string searchTerm, ProductRequestParameters p)
        {
            var products=_manager.ProductService.GetProductsWithSearch(searchTerm);
            var pagination = new Pagination()
            {
                CurrentPage = p.PageNumber,
                ItemsPerPage = p.PageSize,
                TotalItems = products.Count()

            };
            return View(new ProductListViewModel()
            {
                Pagination = pagination,
                Products = products
            });
        }


    }
}
