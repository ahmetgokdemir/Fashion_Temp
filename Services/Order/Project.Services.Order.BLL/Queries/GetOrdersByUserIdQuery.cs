using MediatR;
using Project.Services.Order.BLL.DTOs;
using Project.Shared.DTOs;


namespace Project.Services.Order.BLL.Queries
{
    public class GetOrdersByUserIdQuery : IRequest<Response<List<OrderDto>>>
    {
        public string User_ID { get; set; } // mediatR yapacağı: controller içinde (GetOrders) user_id parametresi ile bu class (GetOrdersByUserIdQuery.cs) çağırılır mediatR da bu class'a bağlı query handler'ı (GetOrdersByUserIdQueryHandler.cs) tetikler..

    }
}
