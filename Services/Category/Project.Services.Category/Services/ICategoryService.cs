using Project.Services.Category.DTOs;
using Project.Shared.DTOs;

namespace Project.Services.Category.Services
{
    public interface ICategoryService
    {
        Task<Response<List<CategoryDTO>>> GetAllAsync();
        Task<Response<CategoryDTO>> CreateAsync(CategoryDTO category);
        Task<Response<CategoryDTO>> GetByIdAsync(string id);
    }
}
