﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using CustomerSite.Models;
using CustomerSite.Services;
using Microsoft.AspNetCore.Http;

namespace CustomerSite.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IProductClient _productClient;

        public HomeController(ILogger<HomeController> logger,IProductClient productClient)
        {
            _logger = logger;
            _productClient=productClient;
        }

        public async Task<IActionResult> Index()
        {
            var products=await _productClient.GetProduct();
            return View(products);
        }

         public async Task<IActionResult> Category(int id)
        {
            var products=await _productClient.GetProductByCate(id);
            return View(products);
        }
         public async Task<IActionResult> Detail(int id)
        {
            var products=await _productClient.GetProductById(id);
            return View(products);
        }

        public async Task<IActionResult> Search(IFormCollection form){
            var products = await _productClient.GetProductByName(form["name"].ToString());
            return View(products);
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
