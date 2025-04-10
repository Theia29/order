using System.Collections.Generic;
using System.Threading.Tasks;
using OrderModuleV2.Model;

namespace OrderModuleV2.Repositories
{
    public interface ICartRepository
    {
        Task AddCartItemsAsync(List<Cart> cartItems);
        Task<List<Cart>> GetCartItemsAsync(int custId);
        Task ClearCartAsync(int custId);
        Task<ICollection<Cart>> GetCartItemsByCustomerIdAsync(int custId);
    }
}