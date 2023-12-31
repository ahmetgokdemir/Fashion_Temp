using Dapper;
using Microsoft.Extensions.Configuration;
using Npgsql;
using Project.Shared.DTOs;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Project.Services.Discount.Services
{
    public class DiscountService : IDiscountService
    {
        private readonly IConfiguration _configuration;
        private readonly IDbConnection _dbConnection;

        public DiscountService(IConfiguration configuration)
        {
            _configuration = configuration;
            _dbConnection = new NpgsqlConnection(_configuration.GetConnectionString("PostgreSql")); // ** NpgsqlConnection -- PostgreSql(appsettşngs.json)
        }

        public async Task<Response<NoContent>> DeleteById(int id)
        {
            var status = await _dbConnection.ExecuteAsync("delete from discount where id=@Id", new { Id = id });

            // var status_2 = await _dbConnection.ExecuteAsync("delete from discount where id=@Id", new {id});

            return status > 0 ? Response<NoContent>.Success(204) : Response<NoContent>.Fail("Discount not found", 404);
        }

        public async Task<Response<List<Entities.Discount>>> GetAll()
        {
            var discounts = await _dbConnection.QueryAsync<Entities.Discount>("Select * from discount");

            return Response<List<Entities.Discount>>.Success(discounts.ToList(), 200);
 
        }

        public async Task<Response<Entities.Discount>> GetByCodeAndUserId(string code, string userID)
        {
            var discounts = await _dbConnection.QueryAsync<Entities.Discount>("select * from discount where userid=@UserID and code=@Code", new { UserID = userID, Code = code });

            var hasDiscount = discounts.FirstOrDefault();

            if (hasDiscount == null)
            {
                return Response<Entities.Discount>.Fail("Discount not found", 404);
            }

            return Response<Entities.Discount>.Success(hasDiscount, 200);
        }

        public async Task<Response<Entities.Discount>> GetById(int id)
        {
            var discount = (await _dbConnection.QueryAsync<Entities.Discount>("Select * from discount where id=@Id", new {id})).SingleOrDefault();

            if (discount == null)
            {
                return Response<Entities.Discount>.Fail("İndirim bulunamadı!", 404);
            }

            return Response<Entities.Discount>.Success(discount, 200);
        }

        public async Task<Response<NoContent>> Save(Entities.Discount discount)
        {
            int saveStatus = await _dbConnection.ExecuteAsync("Insert into discount (userId, rate, code) Values (@UserID, @Rate, @Code)", discount);

            if (saveStatus > 0)
            {
                return Response<NoContent>.Success(204);
            }

            return Response<NoContent>.Fail("İşlem sırasında hata meydana geldi!", 500);
        }

        public async Task<Response<NoContent>> Update(Entities.Discount discount)
        {
            int updatestatus = await _dbConnection.ExecuteAsync("update discount set userid=@UserID, code=@Code, rate=@Rate where id=@Id", new { Id = discount.Id, UserID = discount.UserID, Code = discount.Code, Rate = discount.Rate });

            // int updatestatus = await _dbConnection.ExecuteAsync("update discount set userid=@UserID, code=@Code, rate=@Rate where id=@Id", discount);

            if (updatestatus > 0)
            {
                return Response<NoContent>.Success(204);
            }

            return Response<NoContent>.Fail("İndirim bulunamadı!", 404);
        }
    }
}
