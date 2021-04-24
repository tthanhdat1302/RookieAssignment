using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Shared;
using System.Linq;
using System;
using Newtonsoft.Json;
using System.Text;
using Microsoft.Extensions.Configuration;

namespace CustomerSite.Services
{
    public class ProductClient:IProductClient
    {
         private readonly IConfiguration _configuration;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IRatingClient _ratingClient;
        public ProductClient(IHttpClientFactory httpClientFactory,IRatingClient ratingClient,IConfiguration configuration)
        {
            _httpClientFactory=httpClientFactory;
            _ratingClient=ratingClient;
            _configuration=configuration;
        }
        public async Task<IList<ProductVm>> GetProduct(){
            var client=_httpClientFactory.CreateClient();
            var response =await client.GetAsync(_configuration["productApi"]);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsAsync<IList<ProductVm>>();
        }
        public async Task<ProductVm> GetProductById(int id){
            var client=_httpClientFactory.CreateClient();
            var response =await client.GetAsync(_configuration["productApi"]+id);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsAsync<ProductVm>();
        }

        public async Task<IEnumerable<ProductVm>> GetProductByCate(int id){
             var client=_httpClientFactory.CreateClient();
            var response =await client.GetAsync(_configuration["productApi"]);
            response.EnsureSuccessStatusCode();
            IList<ProductVm> a =await response.Content.ReadAsAsync<IList<ProductVm>>();
            return a.Where(x=>x.CategoryId==id);
        }

        public async Task<IEnumerable<ProductVm>> GetProductByName(string name){
            var client = _httpClientFactory.CreateClient();
            var response = await client.GetAsync(_configuration["productApi"]);
            response.EnsureSuccessStatusCode();
            IList<ProductVm> productByName = await response.Content.ReadAsAsync<IList<ProductVm>>();
            return productByName.Where(x => x.Name.StartsWith(name, StringComparison.OrdinalIgnoreCase));   
        }

        public async Task<IEnumerable<ProductVm>> GetCateByProduct(int id){
            var client = _httpClientFactory.CreateClient();
            var response = await client.GetAsync(_configuration["productApi"]);
            var pro = await client.GetAsync(_configuration["productApi"]+id);
            response.EnsureSuccessStatusCode();
            pro.EnsureSuccessStatusCode();
            IList<ProductVm> products = await response.Content.ReadAsAsync<IList<ProductVm>>();
            ProductVm product= await pro.Content.ReadAsAsync<ProductVm>();
            return products.Where(x => x.CategoryId == product.CategoryId && x.Id!=product.Id);    
        }

        public async Task PuttRatingProduct(int ProId,ProductCreateRequest model){
            var client = new HttpClient();
            double avr=await _ratingClient.FindRatingByProduct(ProId);
            var product=await GetProductById(ProId);
            model = new ProductCreateRequest(){
                Name = product.Name,
                Price = product.Price,
                Image = product.Image,
                Description = product.Description,
                RatingAVG = avr,
                CategoryId = product.CategoryId,
                ImageFile=null
            };
            await client.PutAsync(_configuration["productApi"]+"rating/" + product.Id , new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json"));
            
        }

    }
}