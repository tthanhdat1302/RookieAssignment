using System.Collections.Generic;
using System.Threading.Tasks;
using Shared;

namespace CustomerSite.Services
{
    public interface IOrderClient
    {
        Task<IList<OrderVm>> GetOrder();
        Task PostOrder(OrderCreateRequest orderCreateRequest);
    }
}