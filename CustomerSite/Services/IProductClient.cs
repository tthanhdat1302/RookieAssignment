using System.Collections.Generic;
using System.Threading.Tasks;
using Shared;

namespace CustomerSite.Services
{
    public interface IProductClient
    {
          Task<IList<ProductVm>> GetProduct();
          Task<ProductVm> GetProductById(int id);
          Task<IEnumerable<ProductVm>> GetProductByCate(int id);
          Task<IEnumerable<ProductVm>> GetProductByName(string name);
          Task<IEnumerable<ProductVm>> GetCateByProduct(int id);
          Task PuttRatingProduct(int ProId,ProductCreateRequest model);
    }
}