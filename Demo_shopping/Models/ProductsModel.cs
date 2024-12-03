using System.ComponentModel.DataAnnotations;

namespace Demo_shopping.Models
{
    public class ProductsModel
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MinLength(4, ErrorMessage = "Yêu cầu nhập Tên Sản phẩm")]
        public string Name { get; set; }

        [Required]
        [MinLength(4, ErrorMessage = "Yêu cầu nhập Mô tả Sản phẩm")]
        public string Description { get; set; }
        [Required]
        [MinLength(4, ErrorMessage = "Yêu cầu nhập Giá Sản phẩm")]
        public decimal Price { get; set; }
        [Required]
        public string Slug { get; set; }

        public int BrandId { get; set; }
        public int CategoryId { get; set; }

        public CategoriesModel Category { get; set; }
        public BrandsModel Brand { get; set; }

        public string Image { get; set; }
    }
}
