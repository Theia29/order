using Microsoft.EntityFrameworkCore;
using OrderModuleV2.Model;
using OrderModuleV2.Repositories;

namespace OrderModuleV2.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly ApplicationDbContext _context;

        public OrderRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Order> PlaceOrderAsync(int custid)
        {
            var cartItems = await _context.Carts
                .Where(c => c.CustId == custid)
                .ToListAsync();

            if (!cartItems.Any()) return null;

            var newOrder = new Order
            {
                CustId = custid,
                OrderStatus = "Pending",
                TotalAmount = cartItems.Sum(c => c.TotalPrice),
                
            };

            foreach (var item in cartItems)
            {
                item.Order = newOrder; // Link back (EF will set OrderId)
            }

            _context.Orders.Add(newOrder);
            await _context.SaveChangesAsync();

            return newOrder;
        }


        public async Task<Order> GetOrderByIdAsync(int orderId)
        {
            return await _context.Orders
                .FirstOrDefaultAsync(o => o.OrderId == orderId);
        }

        public Task SaveOrderAsync(Order newOrder)
        {
            throw new NotImplementedException();
        }
    }
}