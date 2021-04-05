using System;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using API.Data;
using API.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace API.Controllers
{
    [Authorize(Roles="Admin")]
    public class ProductManagerController : Controller
    {
        private readonly ILogger<ProductManagerController> _logger;
        private readonly ApplicationDbContext _applicationDbContext;

        public ProductManagerController(ILogger<ProductManagerController> logger,ApplicationDbContext applicationDbContext)
        {
            _logger = logger;
            _applicationDbContext=applicationDbContext;
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(ProductCreateViewModel model)
        {
            if(ModelState.IsValid)
            {
                // string uniqueFileName=null;
                byte[] p1=null;
                if(model.Image!=null)
                {
                    using(var fs1=model.Image.OpenReadStream())
                    using(var ms1=new MemoryStream()){
                        fs1.CopyTo(ms1);
                        p1=ms1.ToArray();
                    }
                }
                Product product=new Product{
                    Name=model.Name,
                    Price=model.Price,
                    Description=model.Description,
                    CategoryId=model.CategoryId,
                    Image=p1
                };
                _applicationDbContext.Products.Add(product);
                await _applicationDbContext.SaveChangesAsync();
                return RedirectToAction("Index","Home");
            }
            return View();
        }
    }
}