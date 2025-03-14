using Azure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using WEB.Data;
using WEB.ViewModels;
using X.PagedList;

namespace WEB.Controllers
{
	public class ProductController : Controller
	{

		QuanLyBanHangContext db = new QuanLyBanHangContext();

		private readonly ILogger<ProductController> _logger;
		public ProductController(ILogger<ProductController> logger)
		{
			_logger = logger;
			
		}
		public IActionResult Index(int? page) //hiển thị danh sách sản phẩm phân trang
		{
			int pageSize = 12;
			int pageNumber = page == null || page < 0 ? 1: page.Value;
			var listproducts = db.Products.AsNoTracking().OrderBy(x=>x.ProductName);
			PagedList<Product> products = new PagedList<Product>(listproducts, pageNumber, pageSize);
			if (products == null)
			{
				return NotFound();
			}
			return View(products);
		}
		


		public IActionResult ProductbyCategory(int? id, int? page) //hiển thị sản phẩm theo loại 
		{
			if (id == null || !db.Categories.Any(c => c.CategoryId == id))
			{
				return NotFound("Không tìm thấy loại sản phẩm này");
			}
			int pageSize = 12;
			int pageNumber = page == null || page < 0 ? 1 : page.Value;
			var listproducts = db.Products.AsNoTracking().Where(x=>x.CategoryId==id).OrderBy(x => x.ProductName);
			PagedList<Product> products = new PagedList<Product>(listproducts, pageNumber, pageSize);

			return View(products);

		}

		public IActionResult Detail(int? id)
		{

			//var product = db.Products.Include(x => x.CategoryId)
			//	.SingleOrDefault(x => x.ProductId == id);
			var product = db.Products
	        .Include(x => x.Category) // Bao gồm thông tin danh mục sản phẩm
	        .Include(x => x.Reviews)  // Bao gồm đánh giá sản phẩm
	        .SingleOrDefault(x => x.ProductId == id);


			if (product == null)
			{
				return NotFound();
			}

			//var model = new ProductVM()
			//{
			//	ProductId = product.ProductId,
			//	ProductName = product.ProductName,
			//	CategoryId = product.CategoryId,
			//	Price = product.Price,
			//	Discount = product.Discount,
			//	Description = product.Description,
			//	Image = product.Image,
			//	ShortDescription = product.ShortDescription,
			//	Capacity = product.Capacity,
			//	Weight = product.Weight,
			//	Pin = product.Pin,
			//	CategoryName = product.Category?.CategoryName //lấy tên thương hiệu
			//};

			var model = new ProductDetailsVM()
			{
				ProductsDetails = product,
				ReviewsDetails = product.Reviews.ToList(), //danh sách đánh giá
				ReviewForm = new ReviewVM(), //form đánh giá
				
			};

			return View(model);
		}

		public IActionResult Search(string? query)
		{
			var hanghoa = db.Products.AsQueryable();
			if (query != null)
			{
				hanghoa = hanghoa.Where(p => p.ProductName.Contains(query));
			}
			var result = hanghoa.Select(p => new ProductViewModel
			{
				ProductId = p.ProductId,
				ProductName = p.ProductName,
				Price= p.Price,
				Image = p.Image,
				Weight = p.Weight,
				Pin = p.Pin,
				Capacity = p.Capacity,
				CategoryName = p.Category.CategoryName
				
			});
			return View(result);
		}

		// Lấy danh sách đánh giá theo sản phẩm
		[HttpGet]		      
		public IActionResult Reviews(int productId)
		{
			var product = db.Products.FirstOrDefault(x=>x.ProductId == productId);
			if (product == null)
			{
				return NotFound();
			}
			var model = new ReviewVM()
			{
				ProductId = productId
			};
			return View(model);
		}

		
		[HttpPost]
		public IActionResult Reviews([Bind("ProductId,Name,Email,Comment, Star, CreatedAt")] ReviewVM re)
		{
			var review = new Review
			{
				Name = re.Name,
				Email = re.Email,
				Comment = re.Comment,
				ProductId = re.ProductId,
				Star = re.Star,
				CreatedAt = DateTime.Now
			};

			db.Reviews.Add(review);
			db.SaveChanges();
			TempData["SuccessMessage"] = "Đánh giá thành công";

			return RedirectToAction("Detail", new { id = re.ProductId });
		}

	}

}

