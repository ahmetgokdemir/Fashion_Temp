using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Project.Services.Category.DTOs;
using Project.Services.Category.Services;
using Project.Shared.ControllerBases;

namespace Project.Services.Category.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : CustomBaseController // CustomBaseController --> SHARED 
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var response = await _productService.GetAllAsync();

            return CreateActionResultInstance(response);
        }

        //courses/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            var response = await _productService.GetByIdAsync(id);

            return CreateActionResultInstance(response);
        }

        //[HttpGet]
        //[Route("/api/[controller]/GetAllByUserId/{userId}")]
        //public async Task<IActionResult> GetAllByUserId(string userId)
        //{
        //    var response = await _courseService.GetAllByUserIdAsync(userId);

        //    return CreateActionResultInstance(response);
        //}

        [HttpPost]
        public async Task<IActionResult> Create(ProductCreateDTO productCreateDTO)
        {
            var response = await _productService.CreateAsync(productCreateDTO);

            return CreateActionResultInstance(response);
        }

        [HttpPut]
        public async Task<IActionResult> Update(ProductUpdateDTO productUpdateDTO)
        {
            var response = await _productService.UpdateAsync(productUpdateDTO);

            return CreateActionResultInstance(response);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var response = await _productService.DeleteAsync(id);

            return CreateActionResultInstance(response);
        }
    }
}
