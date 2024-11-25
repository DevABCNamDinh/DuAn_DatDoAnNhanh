using DuAn_DoAnNhanh.Application.Interfaces.Service;
using DuAn_DoAnNhanh.Client.Models.ViewModel;
using DuAn_DoAnNhanh.Data.Entities;
using Microsoft.AspNetCore.Mvc;

namespace DuAn_DoAnNhanh.Client.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Login(LoginViewModel loginViewModel)
        {
            var user = _userService.Login(loginViewModel.Email, loginViewModel.Password);
            TempData["ReturnUrl"] = Request.Headers["Referer"].ToString();

            if (user != null)
            {
                HttpContext.Session.SetString("UserId", user.UserID.ToString());
                HttpContext.Session.SetString("UserName", user.FullName.ToString());
                TempData["Message"] = "Đăng nhập thành công";

                if (TempData["ReturnUrl"] != null)
                {
                    return Redirect(TempData["ReturnUrl"].ToString());
                }
                return RedirectToAction("Index", "Home");
            }
            else
            {
                TempData["Error"] = " Email hoặc mật khẩu không chính xác";
                TempData["OpenSignInModal"] = true;
                return Redirect(TempData["ReturnUrl"].ToString());
            }
        }
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Register(RegisterViewModel registerViewModel)
        {
            if (ModelState.IsValid)
            {
                var user = new User()
                {
                    UserID = Guid.NewGuid(),
                    FullName = registerViewModel.FullName,
                    Email = registerViewModel.Email,
                    Password = registerViewModel.Password
                    
                };
                var registeredUser = _userService.Register(user);
                HttpContext.Session.SetString("UserEmail", user.Email);
                return RedirectToAction("Login");
            }
            else
            {
                return View(registerViewModel);
            }
        }
        public IActionResult Logout()
        {
            HttpContext.Session.Remove("UserId");
            HttpContext.Session.Remove("UserName");
            return RedirectToAction("Index", "Home");
        }
    }
}
