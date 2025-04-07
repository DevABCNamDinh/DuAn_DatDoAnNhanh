using DuAn_DoAnNhanh.Application.Interfaces.Service;
using DuAn_DoAnNhanh.Data.EF;
using DuAn_DoAnNhanh.Data.Enum;
using DuAn_DoAnNhanh.Data.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace DuAn_DoAnNhanh.Manage.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUserService _userService; 
     
     
        public AccountController( IUserService userService)
        {
            _userService = userService;
       
           
        }
        public IActionResult Index()
        {
            return View();
           
        }
    
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(LoginViewModel login)
        {

			if (!ModelState.IsValid)
			{
				ViewBag.Error = "Vui lòng nhập đầy đủ thông tin.";
				return View();
			}

			var user = _userService.Authenticate(login.Email, login.Password);

			if (user != null)
			{
				// Lưu thông tin vào session
				HttpContext.Session.SetString("UserEmail", user.Email);
				HttpContext.Session.SetString("UserRole", user.Role.ToString());

				// Điều hướng theo vai trò
				switch (user.Role.ToString().ToLower())
				{
					case "admin":
						return RedirectToAction("Index", "Account");
				
					default:
						return RedirectToAction("Index", "Store");
				}
			}

			ViewBag.Error = "Email hoặc mật khẩu không đúng, hoặc bạn không có quyền truy cập.";
			return View();
		}
    }
    }

