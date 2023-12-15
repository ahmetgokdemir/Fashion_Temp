using Project.Shared.DTOs;
using System.Collections.Generic;
using System.Reflection;
using System.Threading.Tasks;

namespace Project.Services.Discount.Services
{
    public interface IDiscountService
    {
        Task<Response<List<Entities.Discount>>> GetAll();
        Task<Response<Entities.Discount>> GetById(int id);

        Task<Response<NoContent>> Save(Entities.Discount discount); 
        Task<Response<NoContent>> Update(Entities.Discount discount);
        Task<Response<NoContent>> DeleteById(int id);

        Task<Response<Entities.Discount>> GetByCodeAndUserId(string code, string userID);
        //Task<Response<Entities.Discount>> GetByCodeAndUserId(string code);

    }
}
