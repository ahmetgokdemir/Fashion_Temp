using Microsoft.AspNetCore.Mvc;
using Project.Services.Cart.DTOs;
using Project.Services.Cart.Services;
using Project.Shared.ControllerBases;
using Project.Shared.Services;
using System.Threading.Tasks;

namespace Project.Services.Cart.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : CustomBaseController
    {
        private readonly ICartService _cartService;
        private readonly ISharedIdentityService _sharedIdentityService;

        public CartController(ICartService cartService, ISharedIdentityService sharedIdentityService)
        {
            _cartService = cartService;
            _sharedIdentityService = sharedIdentityService;
        }

        [HttpGet]
        public async Task<IActionResult> GetCart()
        {
            //HttpContext.Session.SetObject("manipulatedData_ufdj", null);
            //HttpContext.Session.GetObject<CartItemDTO>("manipulatedData_ufdj");

            //var claims = HttpContext.User.Claims;
            //var claims_2 = User.Claims;

            return CreateActionResultInstance(await _cartService.GetCart(_sharedIdentityService.GetUserId));
        }

        [HttpPost]
        public async Task<IActionResult> AddToCart(CartItemDTO _cartItem)
        {
            string user_ID = _sharedIdentityService.GetUserId;
            var response = await _cartService.AddToCart(_cartItem, user_ID);

            return CreateActionResultInstance(response);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteFromCart(string product_ID)

        {
            return CreateActionResultInstance(await _cartService.DeleteFromCart(product_ID, _sharedIdentityService.GetUserId));
        }
    }
}
