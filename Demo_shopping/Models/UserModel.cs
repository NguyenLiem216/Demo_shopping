using System.ComponentModel.DataAnnotations;

namespace Demo_shopping.Models
{
	public class UserModel
	{
		public int Id { get; set; }
		[Required(ErrorMessage ="Vui lòng nhập Username")]
		public string UserName { get; set; }
		[Required(ErrorMessage = "Vui lòng nhập Email"),EmailAddress]
		public string Email { get; set; }
		[DataType(DataType.Password),Required(ErrorMessage ="Vui lòng nhập Password")]
		public string Password { get; set; }


	}
}
