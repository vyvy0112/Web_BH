namespace WEB.ViewModels
{
	public class CartItem
	{

		public int ProductId { get; set; }

		public string Image { get; set; }
		public string ProductName { get; set; }
		public double Price { get; set; }

		public int Quantity { get; set; }

		public double TotalPrice => Price * Quantity;	
		public CartItem() //đặt hàng tự động tạo ra => giỏ hàng khoong có sp
		{

		}

		


	}

	public class CheckoutVM()
	{
		public bool giongkhachhang { get; set; }
		public string? UserName { get; set; }

		public string? Adrress { get; set; }

		public string? Email { get; set; }

		public string? Note { get; set; }

	}
}
