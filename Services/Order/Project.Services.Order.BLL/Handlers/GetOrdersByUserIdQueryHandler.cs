using MediatR;
using Microsoft.EntityFrameworkCore;
using Project.Services.Order.BLL.DTOs;
using Project.Services.Order.BLL.Mapping;
using Project.Services.Order.BLL.Queries;
using Project.Services.Order.Infrastructure;
using Project.Shared.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Services.Order.BLL.Handlers
{
    // GetOrdersByUserIdQuery --> REQUEST; Response<List<OrderDto>> --> Response
    internal class GetOrdersByUserIdQueryHandler : IRequestHandler<GetOrdersByUserIdQuery, Response<List<OrderDto>>>
    {
        private readonly OrderDbContext _context;

        public GetOrdersByUserIdQueryHandler(OrderDbContext context)
        {
            _context = context;
        }

        public async Task<Response<List<OrderDto>>> Handle(GetOrdersByUserIdQuery request, CancellationToken cancellationToken)
        {
            // 10:25 request.UserId --> GetOrdersByUserIdQuery.UserId
            var orders = await _context.Orders.Include(x => x.OrderItems).Where(x => x.Customer_ID == request.User_ID).ToListAsync();

            if (!orders.Any())
            {
                return Response<List<OrderDto>>.Success(new List<OrderDto>(), 200);
            }

            var ordersDto = ObjectMapper.Mapper.Map<List<OrderDto>>(orders);

            return Response<List<OrderDto>>.Success(ordersDto, 200);

            //throw new NotImplementedException();
        }
    }
}
