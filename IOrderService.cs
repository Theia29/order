using OrderModuleV2.Model;
using System.Threading.Tasks;

namespace OrderModuleV2.Services
{
    public interface IOrderService
    {
        Task<Order> PlaceOrderAsync(int custId);
        Task<Order> GetOrderByIdAsync(int orderId);
    }
}