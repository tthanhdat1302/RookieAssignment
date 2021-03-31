using CustomerSite.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CustomerSite.ViewComponents
{
    public class CategoryMenuViewComponent : ViewComponent
    {
        private readonly ICategoryClient _categoryClient;

        public CategoryMenuViewComponent(ICategoryClient categoryClient)
        {
            _categoryClient = categoryClient;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var cates = await _categoryClient.GetCategory();

            return View(cates);
        }
    }
}