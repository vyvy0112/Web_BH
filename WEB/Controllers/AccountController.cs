﻿using AutoMapper;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.CodeAnalysis.Scripting;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages;
using System.Security.Claims;
using WEB.Data;
using WEB.Reponsitory;
using WEB.ViewModels;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace WEB.Controllers
{
	public class AccountController : Controller
	{
		private readonly QuanLyBanHangContext _db;
		private readonly IMapper _mapper;
		public AccountController(QuanLyBanHangContext context,IMapper mapper)
		{
			_db = context ;
			_mapper = mapper;
		}

		#region Register
		[HttpGet]
		public IActionResult Register()
		{
			return View();
		}



		[HttpPost]
		public IActionResult Register([Bind("UserName,Email,Password")] RegisterVM model)
		{
			if (ModelState.IsValid)
			{
				var user = _mapper.Map<User>(model); //chuyển đổi sang entity AppUser
				 // Lấy UserID lớn nhất trong DB và tăng lên 1
				var lastUser = _db.Users.OrderByDescending(u => u.UserId).FirstOrDefault();
				int newIdNumber = lastUser != null ? int.Parse(lastUser.UserId) + 1 : 1;
				user.UserId = newIdNumber.ToString(); // Gán ID mới
				user.Password = model.Password.ToMd5Hash(); // mã hóa mật khẩu
				_db.Users.Add(user);
				_db.SaveChanges();
				TempData["SuccessMessage"] = "Đăng ký thành công! Vui lòng đăng nhập.";
				return RedirectToAction("Register","Account");
			}
			return View(model);
		}

		#endregion

		#region Login
		[HttpGet]		
		public IActionResult Login(string? ReturnUrl)
		{
			ViewBag.ReturnUrl = ReturnUrl;
			return View();
		}
		[HttpPost]
		public async Task<IActionResult> Login(LoginVM model, string? ReturnUrl) 
		{
			ViewBag.ReturnUrl = ReturnUrl;
			if (ModelState.IsValid) 
			{
				var user = _db.Users.SingleOrDefault(us => us.UserName == model.UserName); //kiểm tra danh sách khách khàng us.UserID == us.UserName 
				if (user == null)
				{
					ModelState.AddModelError("Lỗi", "Không có user này ");
				}
				else
				{
					if (user.Password != model.Password.ToMd5Hash())
					{
						ModelState.AddModelError("Lỗi", "Sai thông tin đăng nhập");

					}
					else
					{
						var claims = new List<Claim>
						{
							new Claim(ClaimTypes.Email,user.Email),
							new Claim(ClaimTypes.Name,user.UserName),
							new Claim("UserId",user.UserId),
							new Claim(ClaimTypes.Role,"Khách Hàng")
						};

						var claimsIdentity = new ClaimsIdentity(claims,CookieAuthenticationDefaults.AuthenticationScheme);
						var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);
						await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, claimsPrincipal);
						//await HttpContext.SignInAsync(claimsPrincipal);


						if (Url.IsLocalUrl(ReturnUrl))
						{
							return Redirect(ReturnUrl);
						}
						else
						{
							return Redirect("/Home/Index");
						}
					}
				}
					
			}
			return View();
		}

		#endregion
		[Authorize]
		public IActionResult Profile()
		{
			return View();
		}


		public async Task<IActionResult> Logout()
		{
			await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
			return RedirectToAction("Index","Product");
		}

	}
}
