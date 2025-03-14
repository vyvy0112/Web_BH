using WEB.Data;

namespace WEB.ViewModels
{
	public class ProductDetailsVM
	{

		public Product ProductsDetails { get; set; }
		public List<Review> ReviewsDetails { get; set; }
		public ReviewVM ReviewForm { get; set; }

		
	}

}
