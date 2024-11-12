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
            var user = _userService.Login( loginViewModel.Email, loginViewModel.Password);
            if (user != null)
            {
               
                HttpContext.Session.SetString("UserEmail", user.Email.ToString());

                return RedirectToAction("Index", "Home");
            }
            else
            {
                ViewBag.Error = "Invalid username or password";
                return View(loginViewModel);
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
    }
}
