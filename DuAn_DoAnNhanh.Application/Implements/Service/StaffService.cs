using DuAn_DoAnNhanh.Application.Interfaces.Service;
using DuAn_DoAnNhanh.Data.EF;
using DuAn_DoAnNhanh.Data.Entities;
using DuAn_DoAnNhanh.Data.Enum;
using DuAn_DoAnNhanh.Data.ViewModel;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace DuAn_DoAnNhanh.Application.Implements.Service
{
    public class StaffService : IStaffService
    {
        private readonly MyDBContext _dbContext;
        private readonly IEmailService _emailService;

        public StaffService(MyDBContext dbContext, IEmailService emailService)
        {
            _dbContext = dbContext;
            _emailService = emailService;
        }

        public async Task CreateStaffAsync(StaffViewModel staffViewModel)
        {
            string password = GenerateRandomPassword();
            string hashedPassword = BCrypt.Net.BCrypt.HashPassword(password);
            User user = new User()
            {
                UserID = Guid.NewGuid(),
                FullName = staffViewModel.FullName,
                Email = staffViewModel.Email,
                Password = hashedPassword,
                Role = staffViewModel.Role,
                Status = staffViewModel.Status,
                CreateDate = DateTime.Now
            };

            _dbContext.Users.Add(user);
            await _dbContext.SaveChangesAsync();

            string subject = "Thông tin tài khoản nhân viên";
            string body = $@"
                <p>Xin chào <strong>{staffViewModel.FullName}</strong>,</p>
                <p>Bạn đã được thêm vào hệ thống với vai trò <strong>{staffViewModel.Role}</strong>.</p>

                <h4>🔐 Thông tin đăng nhập:</h4>
                <ul>
                    <li><strong>Email:</strong> {staffViewModel.Email}</li>
                    <li><strong>Mật khẩu:</strong> {password}</li>
                </ul>

                <p style='color: red;'><b>Lưu ý:</b> Vui lòng đổi mật khẩu sau khi đăng nhập.</p>
                <p>Trân trọng,<br>Hệ thống quản lý</p>
            ";

            await _emailService.SendEmailAsync(staffViewModel.Email, subject, body);
        }
        public async Task UpdateStaffAsync(StaffViewModel staffViewModel)
        {
            var user = await _dbContext.Users.FindAsync(staffViewModel.UserID);
            if (user == null)
            {
                throw new Exception("User not found");
            }

            user.FullName = staffViewModel.FullName;
            user.Email = staffViewModel.Email;
            user.Role = staffViewModel.Role;
            user.Status = staffViewModel.Status;

            await _dbContext.SaveChangesAsync();

            string subject = "Cập nhật thông tin tài khoản";
            string body = $@"
                <p>Xin chào <strong>{staffViewModel.FullName}</strong>,</p>
                <p>Thông tin tài khoản của bạn đã được cập nhật.</p>

                <h4>📋 Thông tin mới:</h4>
                <ul>
                    <li><strong>Họ tên:</strong> {staffViewModel.FullName}</li>
                    <li><strong>Email:</strong> {staffViewModel.Email}</li>
                    <li><strong>Vai trò:</strong> {staffViewModel.Role}</li>
                    <li><strong>Trạng thái:</strong> {(staffViewModel.Status == Status.Activity ? "Hoạt động" : "Không hoạt động")}</li>
                </ul>

                <p>Nếu bạn không yêu cầu cập nhật này, vui lòng liên hệ quản trị viên.</p>
                <p>Trân trọng,<br>Hệ thống quản lý</p>
            ";

            await _emailService.SendEmailAsync(staffViewModel.Email, subject, body);
        }


        private string GenerateRandomPassword(int length = 8)
        {
            const string valid = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890!@#$%^&*";
            Random rnd = new Random();
            return new string(Enumerable.Repeat(valid, length).Select(s => s[rnd.Next(s.Length)]).ToArray());
        }
    }
}
