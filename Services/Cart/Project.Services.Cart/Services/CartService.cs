using Project.Services.Cart.DTOs;
using Project.Services.Cart.Services;
using Project.Shared.DTOs;
using StackExchange.Redis;
using System;
using System.Text.Json;
using System.Threading.Tasks;

namespace Project.Services.Cart.Services
{
    public class CartService : ICartService
    {
        private readonly RedisService _redisService;
        private readonly CartDTO _cartDTO;
        private readonly IDatabase _db;


        public CartService(RedisService redisService)
        {
            _redisService = redisService;

            _db = _redisService.GetDb();

            // _cartDTO = HttpContext.Session.GetObject<CartDTO>("cartsession") == null ? new CartDTO() : HttpContext.GetObject<CartDTO>("cartsession");

            //  HttpContext.Session.SetObject
            //  HttpContext.Session.SetObject("manipulatedData_ufdj", null);
            //  HttpContext.Session.GetObject<UserFoodJunctionDTO>("manipulatedData_ufdj");

            //if (_cartDTO.MyCartList == null)♦4♦♦
            //{
            //    _cartDTO = new CartDTO();
            //}

            if (_cartDTO == null)
            {
                _cartDTO = new CartDTO();
            }
        }

        public async Task<Response<bool>> AddToCart(CartItemDTO _cartItem, string user_ID)
        {
            var existCart = await _db.StringGetAsync(user_ID);
            CartDTO _cart = JsonSerializer.Deserialize<CartDTO>(existCart);

            _cart.AddToCart(_cartItem);
            _cart.User_ID = user_ID;

            var status = await _db.StringSetAsync(_cart.User_ID, JsonSerializer.Serialize(_cart));

            return status ? Response<bool>.Success(204) : Response<bool>.Fail("Cart could not save", 500);
        }

        public async Task<Response<bool>> DeleteFromCart(string product_ID, string user_ID)
        {
            var existCart = await _db.StringGetAsync(user_ID);
            CartDTO _cart = JsonSerializer.Deserialize<CartDTO>(existCart);

            _cart.DeleteFromCart(product_ID);

            var status = false;

            if (_cart.MyCartList().Count == 0)
            {
                status = await _db.KeyDeleteAsync(_cart.User_ID);
                // var status = await _redisService.GetDb().KeyDeleteAsync(userId);
            }
            else
            {
                //await _db.KeyDeleteAsync(_cartDTO.User_ID);  
                status = await _db.StringSetAsync(_cart.User_ID, JsonSerializer.Serialize(_cart));
            }

            return status ? Response<bool>.Success(204) : Response<bool>.Fail("Cart not found", 404);
        }

        public async Task<Response<CartDTO>> GetCart(string user_ID)
        {
            //_cartDTO.User_ID = user_ID;
            var existCart = await _db.StringGetAsync(user_ID);

            if (string.IsNullOrEmpty(existCart))
            {
                return Response<CartDTO>.Fail("Cart not found", 404);
            }

            return Response<CartDTO>.Success(JsonSerializer.Deserialize<CartDTO>(existCart), 200); // ** Deserialize

        }

        public Task<Response<NoContent>> Update_AmountofCartItem(CartItemDTO item, short _amount)
        {
            throw new NotImplementedException();
        }
    }
}
