using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Shared;
using System.Linq;
using System;

namespace CustomerSite.Services
{
    public class CategoryClient:ICategoryClient
    {
        private readonly IHttpClientFactory _httpClientFactory;
        public CategoryClient(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory=httpClientFactory;
        }

         public async Task<IList<CategoryVm>> GetCategory()
        {
            var client = _httpClientFactory.CreateClient();
            var response = await client.GetAsync("https://localhost:5001/api/category");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsAsync<IList<CategoryVm>>();
        }
    }
}