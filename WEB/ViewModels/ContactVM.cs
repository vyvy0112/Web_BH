using System.ComponentModel.DataAnnotations;

namespace WEB.ViewModels
{
	public partial class ContactVM
	{
		[Key]
		public int Id { get; set; }

		public string FullName { get; set; } = null!;

		public string Email { get; set; } = null!;

		public string Message { get; set; } = null!;

		public DateTime? CreatedDate { get; set; } = DateTime.Now;


		public bool? Status { get; set; } = false; //false là chưa xử lý 

	}
}
