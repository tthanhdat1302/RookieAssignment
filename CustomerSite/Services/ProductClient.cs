using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Shared;
using System.Linq;

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
        public async Task<ProductVm> GetProductById(int id){
            var client=_httpClientFactory.CreateClient();
            var response =await client.GetAsync("https://localhost:5001/api/product/"+id);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsAsync<ProductVm>();
        }

        public async Task<IEnumerable<ProductVm>> GetProductByCate(int id){
             var client=_httpClientFactory.CreateClient();
            var response =await client.GetAsync("https://localhost:5001/api/product");
            response.EnsureSuccessStatusCode();
            IList<ProductVm> a =await response.Content.ReadAsAsync<IList<ProductVm>>();
            return a.Where(x=>x.CategoryId==id);
        }
    }
}