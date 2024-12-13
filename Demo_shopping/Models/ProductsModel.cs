using Demo_shopping.Repositories.Validations;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Demo_shopping.Models
{
    public class ProductsModel
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Yêu cầu nhập Tên Sản phẩm")]
        [MinLength(4, ErrorMessage = "Tên Sản phẩm phải có ít nhất 4 ký tự")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Yêu cầu nhập Mô tả Sản phẩm")]
        [MinLength(4, ErrorMessage = "Mô tả Sản phẩm phải có ít nhất 4 ký tự")]
        public string Description { get; set; }
        [Required(ErrorMessage = "Yêu cầu nhập Giá Sản phẩm")]
        [Range(1, double.MaxValue, ErrorMessage = "Giá Sản phẩm phải lớn hơn 0")]
        public decimal Price { get; set; }
        public string Slug { get; set; }

        public int BrandId { get; set; }
        public int CategoryId { get; set; }

        public CategoriesModel Category { get; set; }
        public BrandsModel Brand { get; set; }

        public string Image { get; set; }
        [NotMapped]
        [FileExtension]
        public IFormFile ImageUpload { get; set; }
    }
}
