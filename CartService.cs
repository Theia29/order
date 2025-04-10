using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OrderModuleV2.Model;
using OrderModuleV2.Models;
using OrderModuleV2.Repositories;

namespace OrderModuleV2.Services
{
    public class CartService : ICartService
    {
        private readonly ICartRepository _cartRepository;

        public CartService(ICartRepository cartRepository)
        {
            _cartRepository = cartRepository;
        }

        public async Task AddToCartAsync(CartRequestDto request)
        {
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
                    Price = request.Prices[i],
                    OrderId = 0
                };
                cartItems.Add(cart);
            }

            await _cartRepository.AddCartItemsAsync(cartItems);
        }
        catch(Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                throw new Exception("Failed to add items to cart", ex);
            }
        }

        public async Task<List<Cart>> GetCartItemsAsync(int custId)
        {
            return await _cartRepository.GetCartItemsAsync(custId);
        }

        public async Task ClearCartAsync(int custId)
        {
            await _cartRepository.ClearCartAsync(custId);
        }
    }
}