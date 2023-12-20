namespace Project.Services.Basket.DTOs
{
    public class CartDTO
    {
        public string User_ID { get; set; }

        public string DiscountCode { get; set;}

        public Dictionary<short,CartItemDTO> _cartItems { get; set; }

        public CartDTO()
        {
            _cartItems = new Dictionary<short, CartItemDTO>();
        }
    }
}
