using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Shared;
using System.Linq;
using System;

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

        public async Task<IEnumerable<ProductVm>> GetProductByName(string name){
            var client = _httpClientFactory.CreateClient();
            var response = await client.GetAsync("https://localhost:5001/api/product");
            response.EnsureSuccessStatusCode();
            IList<ProductVm> productByName = await response.Content.ReadAsAsync<IList<ProductVm>>();
            return productByName.Where(x => x.Name.StartsWith(name, StringComparison.OrdinalIgnoreCase));   
        }

        public async Task<IEnumerable<ProductVm>> GetCateByProduct(int id){
            var client = _httpClientFactory.CreateClient();
            var response = await client.GetAsync("https://localhost:5001/api/product");
            var pro = await client.GetAsync("https://localhost:5001/api/product/"+id);
            response.EnsureSuccessStatusCode();
            pro.EnsureSuccessStatusCode();
            IList<ProductVm> products = await response.Content.ReadAsAsync<IList<ProductVm>>();
            ProductVm product= await pro.Content.ReadAsAsync<ProductVm>();
            return products.Where(x => x.CategoryId == product.CategoryId && x.Id!=product.Id);    
        }

    }
}