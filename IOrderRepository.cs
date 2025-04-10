using OrderModuleV2.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OrderModuleV2.Repositories
{
    public interface IOrderRepository
    {
        Task<Order> PlaceOrderAsync(int custId);
        Task<Order> GetOrderByIdAsync(int orderId);
        Task SaveOrderAsync(Order newOrder);
    }
}