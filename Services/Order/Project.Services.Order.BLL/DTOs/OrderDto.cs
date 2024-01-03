using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Services.Order.BLL.DTOs
{
    public class OrderDto
    {
        // BaseEntity'den gelen Id (public class Order : Entity, IAggregateRoot)
        public int Id { get; set; }
        public DateTime CreatedDate { get; set; }

        public AddressDto Address { get; set; }

        public string BuyerId { get; set; }

        // List oldu --> public IReadOnlyCollection<OrderItem> OrderItems => _orderItems;
        public List<OrderItemDto> OrderItems { get; set; }
    }
}
