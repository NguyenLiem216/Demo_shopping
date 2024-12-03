
namespace Demo_shopping.Models
{
    public class CartItemModel
    {
        public long ProductId { get; set; }
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public decimal Total
        {
            get { return Quantity * Price; }
        }
        public string Image { get; set; }
        public CartItemModel()
        {

        }
        public CartItemModel(ProductsModel products)
        {
            ProductId = products.Id;
            ProductName = products.Name;
            Price = products.Price;
            Quantity = 1;
            Image = products.Image;
        }
    }
}
