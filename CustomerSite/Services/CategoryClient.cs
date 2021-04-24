using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Shared;
using System.Linq;
using System;
using Microsoft.Extensions.Configuration;

namespace CustomerSite.Services
{
    public class CategoryClient:ICategoryClient
    {
        private readonly IConfiguration _configuration;
        private readonly IHttpClientFactory _httpClientFactory;
        public CategoryClient(IHttpClientFactory httpClientFactory,IConfiguration configuration)
        {
            _httpClientFactory=httpClientFactory;
            _configuration=configuration;
        }

         public async Task<IList<CategoryVm>> GetCategory()
        {
            var client = _httpClientFactory.CreateClient();
            var response = await client.GetAsync(_configuration["categoryApi"]);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsAsync<IList<CategoryVm>>();
        }
    }
}