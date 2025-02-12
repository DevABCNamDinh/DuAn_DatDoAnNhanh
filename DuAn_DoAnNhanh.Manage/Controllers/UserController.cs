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




        //}


    }



    }

