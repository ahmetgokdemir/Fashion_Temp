using System.Collections.Generic;
using System.Linq;

namespace Project.Services.Cart.DTOs
{
    public class CartDTO
    {
        public string User_ID { get; set; }

        public string DiscountCode { get; set;}

        public Dictionary<string,CartItemDTO> _cartItems { get; set; }

        public CartDTO()
        {
            _cartItems = new Dictionary<string, CartItemDTO>();
        }

        public List<CartItemDTO> MyCartList()
        {
            return _cartItems.Select(x => x.Value).ToList();

            // return _cartItems.Values.ToList();
         }

        public void AddToCart(CartItemDTO item)
        {
            if (_cartItems.ContainsKey(item.Product_ID))
            {
                ++ _cartItems[item.Product_ID].Amount;
                return; // break;
            }
            
            _cartItems.Add(item.Product_ID, item);

        }
        
        public void DeleteFromCart(string id)
        {
            if (_cartItems[id].Amount > 1)
            {

                --_cartItems[id].Amount; // _cartItems[id].Amount -= 1;
                return;
            }

            _cartItems.Remove(id);

        }

        public void Update_AmountofCartItem(string id, short _amount)
        {

            //_cartItems[id].Amount = _amount;

            _cartItems.Where(x => x.Value.Product_ID == id).Single().Value.Amount = _amount;
         }

        public decimal? TotalPrice { get => _cartItems.Sum(x => x.Value.SubTotal); }

        //public decimal? TotalPrice
        //{
        //    get
        //    {
        //        return _cartItems.Sum(x => x.Value.SubTotal); // encupslation , read only field (set'i yok (atama yapılamaz).. sadece get var..)

        //    }
        //}

        //public double? TotalPrice_2 { get; private set; }

        //Todo:Ödev Update

    }
}
