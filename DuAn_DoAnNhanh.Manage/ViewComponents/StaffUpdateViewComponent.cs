using DuAn_DoAnNhanh.Data.Entities;
using DuAn_DoAnNhanh.Data.ViewModel;
using DuAn_DoAnNhanh.Application.Interfaces.Service;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using DuAn_DoAnNhanh.Data.EF;

namespace DuAn_DoAnNhanh.Manage.ViewComponents
{
    public class StaffUpdateViewComponent : ViewComponent
    {
        private readonly IStaffService _staffService;
        private readonly MyDBContext _dbContext;

        public StaffUpdateViewComponent(IStaffService staffService, MyDBContext dbContext)
        {
            _staffService = staffService;
            _dbContext = dbContext;
        }

        public async Task<IViewComponentResult> InvokeAsync(Guid userId)
        {
            var user = await _dbContext.Users.FindAsync(userId);
            if (user == null)
            {
                return View("Error"); // Nếu không tìm thấy nhân viên
            }

            var staffViewModel = new StaffViewModel
            {
                UserID = user.UserID,
                FullName = user.FullName,
                Email = user.Email,
                Role = user.Role,
                Status = user.Status
            };

            return View(staffViewModel);
        }
    }
}
