using DuAn_DoAnNhanh.Application.Implements.Service;
using DuAn_DoAnNhanh.Application.Interfaces.Service;
using DuAn_DoAnNhanh.Data.EF;
using DuAn_DoAnNhanh.Data.Entities;
using DuAn_DoAnNhanh.Data.Enum;
using DuAn_DoAnNhanh.Data.ViewModel;
using DuAn_DoAnNhanh.Manage.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration.UserSecrets;
using DuAn_DoAnNhanh.Manage.Models.ViewModel;


namespace DuAn_DoAnNhanh.Manage.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserService _userService; // Inject IUserService để sử dụng logic nghiệp vụ
         private readonly MyDBContext _dbContext;
        private readonly IBillService _billService;
        public UserController(MyDBContext dBContext,  IUserService userService, IBillService billService)
        {
            _userService = userService;
            _dbContext = dBContext;
            _billService = billService;
        }

        public IActionResult Index( )
        {

            var userList = _userService.GetAllUser().Where(x=>x.Role==Role.Customer);    
            return View(userList);
        }
        [HttpGet]
        public IActionResult UserDetail(Guid id)

        {

            var billList = _dbContext.Bill.Where(x=>x.UserID == id).Include(x=>x.User).ToList();


            return RedirectToAction("GetAll", "Bill", new {userId=id});


        }

        public IActionResult GetAllBill(Guid id)
        {
            var bill = _billService.GetBillById(id);
            var listBillDetails = _dbContext.BillDetailses.Where(x => x.BillID == id).ToList();
            var billViewModel = new BillViewModel();
            billViewModel.Bill = bill;
            billViewModel.BillDetails = listBillDetails;
            return View(billViewModel);
        }

        //Dang nhap, 
        public IActionResult Login()
        {
            return View();
        }
      
        [HttpPost]
        public IActionResult Login(string email, string password)
        {
            var user = _dbContext.Users
                .FirstOrDefault(u => u.Email == email && u.Password == password && u.Role != Role.Customer);

            if (user != null)
            {
                // Lưu thông tin người dùng vào session
                HttpContext.Session.SetString("UserEmail", user.Email);
                HttpContext.Session.SetString("UserRole", user.Role.ToString());

                // Chuyển hướng đến trang thống kê
                return RedirectToAction("Index", "Statistical");
            }

            ViewBag.Error = "Email hoặc mật khẩu không đúng, hoặc bạn không có quyền truy cập.";
            return View();
        }


    }



}

