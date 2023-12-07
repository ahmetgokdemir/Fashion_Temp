using Project.Services.Category.DTOs;
using Project.Shared.DTOs;

namespace Project.Services.Category.Services
{
    public interface IProductService
    {
        Task<Response<List<ProductDTO>>> GetAllAsync();

        Task<Response<ProductDTO>> GetByIdAsync(string id);

        // Task<Response<List<ProductDTO>>> GetAllByUserIdAsync(string userId);

        Task<Response<ProductDTO>> CreateAsync(ProductCreateDTO productCreateDTO);

        // NoContent
        Task<Response<NoContent>> UpdateAsync(ProductUpdateDTO productUpdateDTO);

        Task<Response<NoContent>> DeleteAsync(string id);
    }
}
