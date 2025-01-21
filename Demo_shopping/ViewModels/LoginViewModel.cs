using System.ComponentModel.DataAnnotations;

namespace Demo_shopping.ViewModels
{
	public class LoginViewModel
	{
		public int Id { get; set; }
		[Required(ErrorMessage = "Vui lòng nhập Username")]
		public string UserName { get; set; }
		[DataType(DataType.Password), Required(ErrorMessage = "Vui lòng nhập Password")]
		public string Password { get; set; }
		public string ReturnUrl { get; set; }

	}
}
