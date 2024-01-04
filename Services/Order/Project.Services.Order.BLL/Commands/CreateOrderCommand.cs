using MediatR;
using Project.Services.Order.BLL.DTOs;
using Project.Shared.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Services.Order.BLL.Commands
{
    public class CreateOrderCommand : IRequest<Response<CreatedOrderDto>> // CreateOrderCommandHandler.cs'de sonunda, CreatedOrderDto.cs'nin field'i olan OrderId geriye dönülecek...
    {
        public string Customer_ID { get; set; }

        public List<OrderItemDto> OrderItems { get; set; }

        public AddressDto Address { get; set; }
    }
}
