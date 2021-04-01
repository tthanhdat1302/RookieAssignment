using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Shared;

namespace CustomerSite.Services
{
    public class ProductClient:IProductClient
    {
        private readonly IHttpClientFactory _httpClientFactory;
        public ProductClient(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory=httpClientFactory;
        }
        public async Task<IList<ProductVm>> GetProduct(){
            var client=_httpClientFactory.CreateClient();
            var response =await client.GetAsync("https://localhost:5001/api/product");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsAsync<IList<ProductVm>>();
        }
    }
}