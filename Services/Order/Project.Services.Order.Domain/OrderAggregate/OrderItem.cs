using Project.Services.Order.Domain.CORE;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Services.Order.Domain.OrderAggregate
{
    public class OrderItem : BaseEntity
    {
        // BaseEntity --> ıd 

        public string Product_ID { get; private set; }
        public string Product_Name { get; private set; }
        public decimal Product_Price { get; private set; }
        public short Amount { get; set; } // Quentity

        //public double? SubTotal_OLD { get { return Product_Price * Amount; } }
        public decimal? SubTotal { get => Product_Price * Amount; }
        //  public bool UnitsInStock { get; set; }

        public string PictureUrl { get; private set; }

        public OrderItem()
        {
        }

        public OrderItem(string productId, string productName, string pictureUrl, decimal price)
        {
            Product_ID = productId;

            Product_Name = productName;
            Product_Price = price;

            PictureUrl = pictureUrl;

            ++Amount;
        }

        public void UpdateOrderItem(string productName, string pictureUrl, decimal price, short amount)
        {
            Product_Name = productName;
            Product_Price = price;

            PictureUrl = pictureUrl;

            Amount = amount;

        }

    }
}
