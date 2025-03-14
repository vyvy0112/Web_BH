using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WEB.Data;
using WEB.Models;
using WEB.ViewModels;

namespace WEB.Controllers
{
    public class HomeController : Controller

    {
        private QuanLyBanHangContext _db;
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger,QuanLyBanHangContext db)
        {
            _logger = logger;
            _db = db;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

		public IActionResult Contact()
		{
			return View();
		}
		[HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Contact(Contact model)
		{
            if(ModelState.IsValid)
            {
                _db.Contacts.Add(model);
                await _db.SaveChangesAsync();
				TempData["SuccessMessage"] = "Gửi liên hệ thành công";

			}
			return View(model);

		}

		public IActionResult PageNotFound()
		{
			return View();
		}
	}
}
