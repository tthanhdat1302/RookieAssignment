using System.Collections.Generic;
using System.Threading.Tasks;
using Shared;

namespace CustomerSite.Services
{
    public interface IProductClient
    {
          Task<IList<ProductVm>> GetProduct();
    }
}