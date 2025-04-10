using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OrderModuleV2.Model;
using OrderModuleV2.Services;

namespace OrderModuleV2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IOrderService _orderService;

        public OrderController(ApplicationDbContext context, IOrderService orderService)
        {
            _context = context;
            _orderService = orderService;
        }

        [HttpPost("place/{custId}")]
        public async Task<IActionResult> PlaceOrder(int custId)
        {
            try
            {
                var cartItems = await _context.Carts
                    .Where(c => c.CustId == custId)
                    .ToListAsync();

                if (!cartItems.Any())
                    return NotFound(new { Message = "Cart is empty. Cannot place an order." });

                var newOrder = new Order
                {
                    CustId = custId,
                    OrderStatus = "Pending",
                    TotalAmount = cartItems.Sum(c => c.TotalPrice)
                };

                _context.Orders.Add(newOrder);
                await _context.SaveChangesAsync();

                foreach (var item in cartItems)
                {
                    item.OrderId = newOrder.OrderId;
                }

                await _context.SaveChangesAsync();

                _context.Carts.RemoveRange(cartItems);
                await _context.SaveChangesAsync();

                return Ok(new { Message = "Order placed successfully.", OrderId = newOrder.OrderId });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Failed to place order", Error = ex.Message });
            }
        }

        [HttpGet("search/{orderId}")]
        public async Task<IActionResult> GetOrderDetails(int orderId)
        {
            try
            {
                var order = await _orderService.GetOrderByIdAsync(orderId);

                if (order == null)
                    return NotFound(new { Message = "Order not found." });

                return Ok(order);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Failed to retrieve order details", Error = ex.Message });
            }
        }
    }
}
