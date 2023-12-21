using Project.Services.Cart.DTOs;
using Project.Shared.DTOs;
using System.Threading.Tasks;

namespace Project.Services.Cart.Services
{
    public interface ICartService
    {
        public Task<Response<CartDTO>> GetCart(string user_ID);

        public Task<Response<bool>> AddToCart(CartItemDTO _cartItem, string user_ID); // CartItemDTO item

        public Task<Response<bool>> DeleteFromCart(string product_ID, string user_ID);

        public Task<Response<NoContent>> Update_AmountofCartItem(CartItemDTO item, short _amount);

    }
}
