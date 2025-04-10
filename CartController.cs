using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OrderModuleV2.Model;
using OrderModuleV2.Models;
using System.Linq;
using System.Threading.Tasks;

namespace OrderModuleV2.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public CartController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpPost("add")]
        public async Task<IActionResult> AddToCart([FromBody] CartRequestDto request)
        {
            if (request == null || request.ItemIds.Count != request.Quantities.Count)
                return BadRequest(new { message = "Invalid request data." });

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                List<Cart> cartItems = new List<Cart>();

              
                for (int i = 0; i < request.ItemIds.Count; i++)
                {
                    var cart = new Cart
                    {
                        CustId = request.CustId,
                        ItemId = request.ItemIds[i],
                        ResId = request.ResIds[i],
                        Quantity = request.Quantities[i],
                        Price = request.Prices[i]
                    };
                    cartItems.Add(cart);
                }

                _context.Carts.AddRange(cartItems);
                await _context.SaveChangesAsync();

                return Ok(new { message = "Items added to cart successfully." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    message = "Failed to add items to cart",
                    error = ex.Message
                });
            }
        }


        [HttpGet("get/{custId}")]
        public async Task<IActionResult> GetCart(int custId)
        {
            try
            {
                var cart = await _context.Carts
                    .Where(c => c.CustId == custId)
                    .ToListAsync();

                if (!cart.Any()) return NotFound(new { Message = "Cart is empty." });

                return Ok(cart);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    Message = "Failed to retrieve cart",
                    Error = ex.Message
                });
            }
        }

        [HttpDelete("clear/{custId}")]
        public async Task<IActionResult> ClearCart(int custId)
        {
            try
            {
                var cartItems = await _context.Carts
                    .Where(c => c.CustId == custId)
                    .ToListAsync();

                if (!cartItems.Any()) return NotFound(new { Message = "Cart is already empty." });

                _context.Carts.RemoveRange(cartItems);
                await _context.SaveChangesAsync();
                return Ok(new { Message = "Cart cleared successfully." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    Message = "Failed to clear cart",
                    Error = ex.Message
                });
            }
        }

        [HttpPost("logout/{custId}")]
        public async Task<IActionResult> Logout(int custId)
        {
            try
            {
                
                await using var transaction = await _context.Database.BeginTransactionAsync();

                var cartItems = await _context.Carts
                    .Where(c => c.CustId == custId)
                    .ToListAsync();

                if (cartItems.Any())
                {
                    _context.Carts.RemoveRange(cartItems);
                    await _context.SaveChangesAsync();
                }

                await transaction.CommitAsync(); 

                return Ok(new { Message = "User logged out. Cart cleared." });
            }
            catch (Exception ex) 
            {
                return StatusCode(500, new
                {
                    Message = "Failed to logout",
                    Error = ex.Message
                });
            }
        }
    }


}

