using Project.Services.Order.Domain.CORE;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Services.Order.Domain.OrderAggregate
{
    public class Order : BaseEntity, IAggregateRoot
    {
        // BaseEntity --> ıd 

        //properties
        public DateTime CreatedDate { get; private set; } = DateTime.Now;
        public Address Address { get; private set; }

        public string User_ID { get; private set; } // Customer

        // field
        private readonly List<OrderItem> _orderItems;
        public IReadOnlyCollection<OrderItem> OrderItems => _orderItems; // unable to set..
        public IReadOnlyCollection<OrderItem> OrderItems_2 { get; }  
        public IReadOnlyCollection<OrderItem> OrderItems_3 { get { return _orderItems; } }
        public IReadOnlyCollection<OrderItem> OrderItems_4 { get => _orderItems; }
        public IReadOnlyCollection<OrderItem> OrderItems_5 { get; set; }

        public Order()
        {
        }

        public Order(string userID, Address address)
        {
            _orderItems = new List<OrderItem>(); // çağrı hocanın bahsettiği durum...
            CreatedDate = DateTime.Now;
            User_ID = userID;
            Address = address;
        }

        public void AddOrderItem(string product_ID, string product_Name, decimal product_Price, string pictureUrl) {

            var Is_existProduct = _orderItems.Any(x => x.Product_ID == product_ID);

            if (!Is_existProduct)
            {
                var newOrderItem = new OrderItem(product_ID,product_Name,pictureUrl, product_Price);

                _orderItems.Add(newOrderItem);
                return;
            }

           OrderItem existProductItem = _orderItems.Where(x => x.Product_ID == product_ID).SingleOrDefault();
            ++ existProductItem.Amount;



        }

        public decimal? GetTotalPrice => _orderItems.Sum(x => x.SubTotal);
        //public double? TotalPrice { get => _cartItems.Sum(x => x.Value.SubTotal); }

    }
}
