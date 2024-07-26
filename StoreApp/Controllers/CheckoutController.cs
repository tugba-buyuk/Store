﻿using Entities.Models;
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
    }
}
