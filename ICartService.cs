using System.Collections.Generic;
using System.Threading.Tasks;
using OrderModuleV2.Model;
using OrderModuleV2.Models;

namespace OrderModuleV2.Services
{
    public interface ICartService
    {
        Task AddToCartAsync(CartRequestDto request);
        Task<List<Cart>> GetCartItemsAsync(int custId);
        Task ClearCartAsync(int custId);
    }
}