using WEB.Data;

namespace WEB.ViewModels
{
	public class ViewDetail
	{
		public virtual ICollection<Review> Reviews { get; set; } = new List<Review>();
		public virtual ICollection<Product> Products { get; set; } = new List<Product>();

	}
}
