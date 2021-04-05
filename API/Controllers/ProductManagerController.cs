using System;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using API.Data;
using API.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace API.Controllers
{
    [Authorize(Roles="Admin")]
    public class ProductManagerController : Controller
    {
        private readonly ILogger<ProductManagerController> _logger;
        private readonly IHostingEnvironment _hostingEnvironment;
        private readonly ApplicationDbContext _applicationDbContext;

        public ProductManagerController(ILogger<ProductManagerController> logger,IHostingEnvironment hostingEnvironment,ApplicationDbContext applicationDbContext)
        {
            _logger = logger;
            _hostingEnvironment=hostingEnvironment;
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
                string uniqueFileName=null;
                if(model.Image!=null)
                {
                    string uploadsFolder=Path.Combine(_hostingEnvironment.WebRootPath,"images");
                    uniqueFileName= Guid.NewGuid().ToString()+'_'+model.Image.FileName;
                    string filePath=Path.Combine(uploadsFolder,uniqueFileName);
                    model.Image.CopyTo(new FileStream(filePath,FileMode.Create));
                }
                Product product=new Product{
                    Name=model.Name,
                    Price=model.Price,
                    Description=model.Description,
                    CategoryId=model.CategoryId,
                    Image=uniqueFileName
                };
                _applicationDbContext.Products.Add(product);
                await _applicationDbContext.SaveChangesAsync();
                return RedirectToAction("Index","Home");
            }
            return View();
        }
    }
}