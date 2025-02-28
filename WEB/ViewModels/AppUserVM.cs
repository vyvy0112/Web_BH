using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices;

namespace WEB.ViewModels
{
	public class AppUserVM: IdentityUser
	{
		public int UserId { get; set; }

		[Required(ErrorMessage = "UserName không được để trống")]
		public string UserName { get; set; } = null!;



		[Required(ErrorMessage = "Email không được để trống")]
		[EmailAddress(ErrorMessage = "Email không hợp lệ")]
		public string Email { get; set; } = null!;




		[DataType(DataType.Password)]
		[Required(ErrorMessage = "Mật khẩu không được để trống")]
		public string Password { get; set; } = null!;

		//public string? Role { get; set; }
	}
}
