using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Shared;
using Microsoft.AspNetCore.Http.Extensions; 

namespace CustomerSite.Services
{
    public class RatingClient : IRatingClient
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public RatingClient(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IList<RatingVm>> GetRating(){
            var client = _httpClientFactory.CreateClient();
            var response = await client.GetAsync("https://localhost:5001/api/rating");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsAsync<IList<RatingVm>>();

        }

        public async Task PostRatingByID(int ProId,string UserName,int RatingScore){
            var client = new HttpClient();
            RatingVm jsonInString= new RatingVm{ProductID=ProId,UserName=UserName,RatingScore=RatingScore};
            await client.PostAsync("https://localhost:5001/api/rating", new StringContent(JsonConvert.SerializeObject(jsonInString), Encoding.UTF8, "application/json"));
            
        }
        public async Task<RatingVm> SearchRating(int ProId,string UserName){
             var client = _httpClientFactory.CreateClient();
            var response = await client.GetAsync("https://localhost:5001/api/rating");
            response.EnsureSuccessStatusCode();
            IList<RatingVm> ratingVms = await response.Content.ReadAsAsync<IList<RatingVm>>();
            return ratingVms.FirstOrDefault(x => x.ProductID == ProId && x.UserName == UserName);
        }


        public async Task RemoveRating(int ProId,string UserName){
            RatingVm ratingVms= await SearchRating(ProId,UserName);
            var client = new HttpClient();
            await client.DeleteAsync("https://localhost:5001/api/rating/"+ratingVms.RatingId);
             
             
        }

        public async Task<double> FindRatingByProduct(int ProId){
            var client = _httpClientFactory.CreateClient();
            var response = await client.GetAsync("https://localhost:5001/api/rating");
            response.EnsureSuccessStatusCode();
            IList<RatingVm> ratingVms = await response.Content.ReadAsAsync<IList<RatingVm>>();
            var ratings=ratingVms.Where(x => x.ProductID == ProId );
            double average = 0;
            int tong=0;
            int count = 0;
            foreach(var item in ratings) {
                count++;
                tong+=item.RatingScore;  
            }
            average=tong/count;
            return average;
        }

    }
}