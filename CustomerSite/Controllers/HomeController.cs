using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using CustomerSite.Models;
using CustomerSite.Services;
using Microsoft.AspNetCore.Http;
using Shared;

namespace CustomerSite.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IProductClient _productClient;
        private readonly IRatingClient _ratingClient;

        public HomeController(ILogger<HomeController> logger,IProductClient productClient,IRatingClient ratingClient)
        {
            _logger = logger;
            _productClient=productClient;
            _ratingClient=ratingClient;
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
             ProductCreateRequest productFormVm = new ProductCreateRequest();
            await _productClient.PuttRatingProduct(id,productFormVm);
            var products = await _productClient.GetProductById(id);
            return View(products);
        }

        public async Task<IActionResult> Search(IFormCollection form){
            var products = await _productClient.GetProductByName(form["name"].ToString());
            return View(products);
        }
         public async Task<IActionResult> Rating(IFormCollection form) {
            int proId=int.Parse(form["proId"]);
            string userName=form["userName"].ToString();
            int rate=int.Parse(form["rate"]);
            if(await _ratingClient.SearchRating(proId,userName ) != null) {
                  _ratingClient.RemoveRating(proId,userName);
            }                          
            await _ratingClient.PostRatingByID(proId,userName,rate);
            
            return RedirectToAction("Index","Home");
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
