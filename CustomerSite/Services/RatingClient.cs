using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Shared;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.Extensions.Configuration;

namespace CustomerSite.Services
{
    public class RatingClient : IRatingClient
    {
         private readonly IConfiguration _configuration;
        private readonly IHttpClientFactory _httpClientFactory;

        public RatingClient(IHttpClientFactory httpClientFactory,IConfiguration configuration)
        {
            _httpClientFactory = httpClientFactory;
            _configuration=configuration;
        }

        public async Task<IList<RatingVm>> GetRating()
        {
            var client = _httpClientFactory.CreateClient();
            var response = await client.GetAsync(_configuration["ratingApi"]);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsAsync<IList<RatingVm>>();

        }

        public async Task PostRatingByID(int ProId, string UserName, int RatingScore)
        {
            var client = new HttpClient();
            RatingVm jsonInString = new RatingVm { ProductID = ProId, UserName = UserName, RatingScore = RatingScore };
            await client.PostAsync(_configuration["ratingApi"], new StringContent(JsonConvert.SerializeObject(jsonInString), Encoding.UTF8, "application/json"));

        }
        public async Task<RatingVm> SearchRating(int ProId, string UserName)
        {
            var client = _httpClientFactory.CreateClient();
            var response = await client.GetAsync(_configuration["ratingApi"]);
            response.EnsureSuccessStatusCode();
            IList<RatingVm> ratingVms = await response.Content.ReadAsAsync<IList<RatingVm>>();
            return ratingVms.FirstOrDefault(x => x.ProductID == ProId && x.UserName == UserName);
        }


        public async Task RemoveRating(int ProId, string UserName)
        {
            RatingVm ratingVms = await SearchRating(ProId, UserName);
            var client = new HttpClient();
            await client.DeleteAsync(_configuration["ratingApi"] + ratingVms.RatingId);


        }

        public async Task<double> FindRatingByProduct(int ProId)
        {
            var client = _httpClientFactory.CreateClient();
            var response = await client.GetAsync(_configuration["ratingApi"]);
            response.EnsureSuccessStatusCode();
            IList<RatingVm> ratingVms = await response.Content.ReadAsAsync<IList<RatingVm>>();
            var ratings = ratingVms.Where(x => x.ProductID == ProId);
                double average = 0;
                int tong = 0;
                 int count;
                if(ratings.Count()!=0)
                    count = 0;
                else
                    count=1;
                foreach (var item in ratings)
                {
                    count++;
                    tong += item.RatingScore;
                }
                average = tong / count;
                return average;
        }

    }
}