using System.Collections.Generic;
using System.Threading.Tasks;
using Shared;

namespace CustomerSite.Services
{
    public interface IRatingClient
    {
        Task<double> FindRatingByProduct(int ProId);
        Task RemoveRating(int ProId,string UserName);
        Task<RatingVm> SearchRating(int ProId,string UserName);
        Task PostRatingByID(int ProId,string UserName,int RatingScore);
        Task<IList<RatingVm>> GetRating();
    }
}