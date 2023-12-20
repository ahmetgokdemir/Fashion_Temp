using System.Runtime.InteropServices;

namespace Project.Services.Basket.DTOs
{
    public class CartItemDTO
    {

        public string Product_ID { get; set; }
        public string Product_Name { get; set; }
        public short Amount { get; set; } // Quentity
        public double Product_Price { get; set; }

        public double? SubTotal { get { return Product_Price * Amount; } }

        public short UnitsInStock { get; set; }

        public CartItemDTO() {

            Amount++;
        }

    }
}
