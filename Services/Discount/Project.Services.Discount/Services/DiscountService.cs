using Project.Shared.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Project.Services.Discount.Services
{
    public class DiscountService : IDiscountService
    {
        public Task<Response<NoContent>> DeleteById(int id)
        {
            throw new System.NotImplementedException();
        }

        public Task<Response<List<Entities.Discount>>> GetAll()
        {
            throw new System.NotImplementedException();
        }

        public Task<Response<Entities.Discount>> GetByCodeAndUserId(string code, string userID)
        {
            throw new System.NotImplementedException();
        }

        public Task<Response<Entities.Discount>> GetById(int id)
        {
            throw new System.NotImplementedException();
        }

        public Task<Response<NoContent>> Save(Entities.Discount discount)
        {
            throw new System.NotImplementedException();
        }

        public Task<Response<NoContent>> Update(Entities.Discount discount)
        {
            throw new System.NotImplementedException();
        }
    }
}
