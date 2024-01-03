using System.Runtime.InteropServices;

namespace Project.Services.Cart.DTOs
{
    public class CartItemDTO
    {

        public string Product_ID { get; set; }
        public string Product_Name { get; set; }
        public short Amount { get; set; } // Quentity
        public decimal Product_Price { get; set; } // no double

        //public double? SubTotal_OLD { get { return Product_Price * Amount; } }
        public decimal? SubTotal { get => Product_Price * Amount;  }

        public bool UnitsInStock { get; set; }

        public CartItemDTO() {

            ++Amount;
            //Amount++;
        }

    }
}
