using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Project.Services.Discount.Services;
using Project.Shared.ControllerBases;
using Project.Shared.Services;
using System.Threading.Tasks;

namespace Project.Services.Discount.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DiscountController : CustomBaseController
    {
        private readonly IDiscountService _discountService;

        private readonly ISharedIdentityService _sharedIdentityService;

        public DiscountController(IDiscountService discountService, ISharedIdentityService sharedIdentityService)
        {
            _discountService = discountService;
            _sharedIdentityService = sharedIdentityService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return CreateActionResultInstance(await _discountService.GetAll());
        }

        [HttpGet("{id}")]
        //  [HttpGet("{id}/{name}/{ect}")] HttpMethodAttribute 
        public async Task<IActionResult> GetById(int id)
        {
            // var discount = await _discountService.GetById(id);

            return CreateActionResultInstance(await _discountService.GetById(id)); 
        }

        [HttpGet]
        [Route("/api/[controller]/[action]/{code}")] // GetById ile karışmasın
        public async Task<IActionResult> GetByCode(string code)
        {
            var userId = _sharedIdentityService.GetUserId; //  jwtoken çimnden çekilecek 

            // var discount = await _discountService.GetByCodeAndUserId(code, userId);

            return CreateActionResultInstance(await _discountService.GetByCodeAndUserId(code, userId));
        }

        [HttpPost]
        public async Task<IActionResult> Save(Entities.Discount discount)
        {
            return CreateActionResultInstance(await _discountService.Save(discount));
        }

        [HttpPut]
        public async Task<IActionResult> Update(Entities.Discount discount)
        {
            return CreateActionResultInstance(await _discountService.Update(discount));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            return CreateActionResultInstance(await _discountService.DeleteById(id));
        }

            //  {
            //      "UserID":"123",
            //      "Rate":10,
            //      "Code":"abc"
            //  }
}
}
