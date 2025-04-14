using DuAn_DoAnNhanh.Application.Interfaces.Service;
using DuAn_DoAnNhanh.Application.Interfaces.Service.Jwt;
using DuAn_DoAnNhanh.Data.EF;
using DuAn_DoAnNhanh.Data.Enum;
using DuAn_DoAnNhanh.Data.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace DuAn_DoAnNhanh.Manage.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUserService _userService;
        private readonly IJwtService _jwtService;



        public AccountController(IUserService userService, IJwtService jwtService)
        {
            _userService = userService;
            _jwtService = jwtService;



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
                return View(login);
            }

            var user = _userService.Login(login);

            if (user != null)
            {
                if (user.Role == Role.Admin
                || user.Role == Role.Manager
                || user.Role == Role.Employee)
                {
                    var token = _jwtService.GenerateToken(login.Email,user.Role.ToString());
                    // Lưu Token vào Session
                    HttpContext.Session.SetString("AuthToken", token);
                    HttpContext.Session.SetString("UserName", user.FullName);

                    if (user.Role == Role.Admin)
                    {
                        return RedirectToAction("Index", "Home");
                    }
                    else if (user.Role == Role.Manager || user.Role == Role.Employee)
                    {
                        return RedirectToAction("Index", "Home", new { Areas = "ManageStore" });
                    }
                }
              
            }
            ViewBag.Error = "Email hoặc mật khẩu không đúng, hoặc bạn không có quyền truy cập.";
            return View(login);
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Remove("AuthToken");
            HttpContext.Session.Remove("UserName");
            return RedirectToAction("Login", "Account");
        }
    }
}

