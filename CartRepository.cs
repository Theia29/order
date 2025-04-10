using Microsoft.EntityFrameworkCore;
using OrderModuleV2.Model;

namespace OrderModuleV2.Repositories
{
    public class CartRepository : ICartRepository
    {
        private readonly ApplicationDbContext _context;

        public CartRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddCartItemsAsync(List<Cart> cartItems)
        {
            await _context.Carts.AddRangeAsync(cartItems);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Cart>> GetCartItemsAsync(int custId)
        {
            return await _context.Carts.Where(c => c.CustId == custId).ToListAsync();
        }

        public async Task ClearCartAsync(int custId)
        {
            var cartItems = await _context.Carts.Where(c => c.CustId == custId).ToListAsync();
            if (cartItems.Any())
            {
                _context.Carts.RemoveRange(cartItems);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<List<Cart>> GetCartItemsByCustomerIdAsync(int custId)
        {
            return await _context.Carts
                .Where(c => c.CustId == custId)
                .ToListAsync();
        }

        Task<ICollection<Cart>> ICartRepository.GetCartItemsByCustomerIdAsync(int custId)
        {
            throw new NotImplementedException();
        }
    }
}