using CustomerSite.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CustomerSite.ViewComponents
{
    public class SameProductViewComponent : ViewComponent
    {
        private readonly IProductClient _productClient;

        public SameProductViewComponent(IProductClient productClient)
        {
            _productClient = productClient;
        }

        public async Task<IViewComponentResult> InvokeAsync(int id){
            var product = await _productClient.GetCateByProduct(id);
            return View(product);
        }
    }
}