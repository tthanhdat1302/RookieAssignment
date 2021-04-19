using System.Collections.Generic;
using System.Threading.Tasks;
using Shared;

namespace CustomerSite.Services
{
    public interface ICategoryClient
    {
         Task<IList<CategoryVm>> GetCategory();
    }
}