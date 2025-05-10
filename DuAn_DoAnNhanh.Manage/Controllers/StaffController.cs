using DuAn_DoAnNhanh.Application.Implements.Service;
using DuAn_DoAnNhanh.Application.Interfaces.Service;
using DuAn_DoAnNhanh.Data.EF;
using DuAn_DoAnNhanh.Data.Enum;
using DuAn_DoAnNhanh.Data.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace DuAn_DoAnNhanh.Manage.Controllers
{
    public class StaffController : Controller
    {
        private readonly MyDBContext _dbContext;
        private readonly IStaffService _staffService;
        private readonly IUserService _userService;

        public StaffController(MyDBContext dbContext, IStaffService staffService, IUserService userService)
        {
            _dbContext = dbContext;
            _staffService = staffService;
            _userService = userService;
        }

        public IActionResult Index()
        {
            var allUsers = _userService.GetAllUser();
            var nonAdminUsers = allUsers.Where(u => u.Role != Role.Admin).ToList();
            return View(nonAdminUsers);
        }


        [HttpPost]
        public async Task<IActionResult> CreateStaff(StaffViewModel staffViewModel)
        {
            if (!ModelState.IsValid)
            {
                return ViewComponent("StaffCreate", new { model = staffViewModel });
            }

            try
            {
                await _staffService.CreateStaffAsync(staffViewModel);
                return Json(new { success = true });
            }
            catch (Exception)
            {
                ModelState.AddModelError("", "Có lỗi xảy ra, vui lòng thử lại.");
                return ViewComponent("StaffCreate", new { model = staffViewModel });
            }
        }

        [HttpGet]
        public async Task<IActionResult> Update(Guid id)
        {
            return ViewComponent("StaffUpdate", new { userId = id });
        }
        [HttpPost]
        public async Task<IActionResult> UpdateStaff(StaffViewModel staffViewModel)
        {
            if (!ModelState.IsValid)
            {
                return ViewComponent("StaffUpdate", new { userId = staffViewModel.UserID });
            }

            try
            {
                await _staffService.UpdateStaffAsync(staffViewModel);
                return Json(new { success = true });
            }
            catch (Exception)
            {
                ModelState.AddModelError("", "Có lỗi xảy ra, vui lòng thử lại.");
                return ViewComponent("StaffUpdate", new { userId = staffViewModel.UserID });
            }
        }


    }
}

