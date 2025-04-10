using OrderModuleV2.Model;
using OrderModuleV2.Repositories;
using OrderModuleV2.Services;
using System.Threading.Tasks;

namespace OrderModuleV2.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly ICartRepository _cartRepository;

        public OrderService(IOrderRepository orderRepository, ICartRepository cartRepository)
        {
            _orderRepository = orderRepository;
            _cartRepository = cartRepository;
        }

        public async Task<Order> PlaceOrderAsync(int custId)
        {
            var cartItems = await _cartRepository.GetCartItemsByCustomerIdAsync(custId);
            if (cartItems == null || !cartItems.Any())
                throw new Exception("Cart is empty");

            double totalAmount = (double)cartItems.Sum(c => c.Price * c.Quantity);

            var newOrder = new Order
            {
                CustId = custId,
                TotalAmount = (decimal)totalAmount,
                OrderStatus = "Pending",
            };

            await _orderRepository.SaveOrderAsync(newOrder);

            // Clear cart after placing order
            await _cartRepository.ClearCartAsync(custId);

            return newOrder;
        }

        public async Task<Order> GetOrderByIdAsync(int orderId)
        {
            return await _orderRepository.GetOrderByIdAsync(orderId);
        }
    }

}