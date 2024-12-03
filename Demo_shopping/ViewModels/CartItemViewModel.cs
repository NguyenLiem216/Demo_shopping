using Demo_shopping.Models;

namespace Demo_shopping.ViewModels
{
    public class CartItemViewModel
    {
        public List<CartItemModel> cartItem { get; set; }
        public decimal GrandTotal { get; set; }
    }
}
