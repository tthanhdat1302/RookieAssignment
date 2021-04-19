using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Shared;

namespace CustomerSite.Services
{
    public class OrderClient:IOrderClient
    {
         private readonly IHttpClientFactory _httpClientFactory;

        public OrderClient(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IList<OrderVm>> GetOrder(){
            var client = _httpClientFactory.CreateClient();
            var response = await client.GetAsync("https://localhost:5001/api/order");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsAsync<IList<OrderVm>>();

        }

        public async Task PostOrder(OrderCreateRequest orderCreateRequest){
            var client = new HttpClient();
            OrderCreateRequest jsonInString= new OrderCreateRequest{OrderQuantity=orderCreateRequest.OrderQuantity,OrderTotal=orderCreateRequest.OrderTotal,UserName=orderCreateRequest.UserName};
            await client.PostAsync("https://localhost:5001/api/order", new StringContent(JsonConvert.SerializeObject(jsonInString), Encoding.UTF8, "application/json")).ConfigureAwait(false);
            
        }
        // public async Task<RatingVm> SearchRating(int ProId,string UserName){
        //      var client = _httpClientFactory.CreateClient();
        //     var response = await client.GetAsync("https://localhost:5001/api/rating");
        //     response.EnsureSuccessStatusCode();
        //     IList<RatingVm> ratingVms = await response.Content.ReadAsAsync<IList<RatingVm>>();
        //     return ratingVms.FirstOrDefault(x => x.ProductID == ProId && x.UserName == UserName);
        // }


        // public async Task RemoveRating(int ProId,string UserName){
        //     RatingVm ratingVms= await SearchRating(ProId,UserName);
        //     var client = new HttpClient();
        //     await client.DeleteAsync("https://localhost:5001/api/rating/"+ratingVms.RatingId);
             
             
        // }
    }
}