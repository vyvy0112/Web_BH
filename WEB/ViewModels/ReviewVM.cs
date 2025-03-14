using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using WEB.Data;

namespace WEB.ViewModels
{
	public class ReviewVM
	{
		[Key]
		public int Id { get; set; }


		[ForeignKey("Product")]
		public int ProductId { get; set; }

		[Required(ErrorMessage = "Vui lòng nhập tên")]
		public string Name { get; set; }

		[Required(ErrorMessage = "Vui lòng nhập email")]
		[EmailAddress(ErrorMessage = "Email không hợp lệ")]
		public string Email { get; set; }

		[Required(ErrorMessage = "Vui lòng nhập nội dung đánh giá")]
		public string Comment { get; set; }

		[Range(1, 5, ErrorMessage = "Số sao phải từ 1 đến 5")]
		public int Star { get; set; }


		public DateTime? CreatedAt { get; set; }

		public ReviewVM()
		{

		}
	}
}
