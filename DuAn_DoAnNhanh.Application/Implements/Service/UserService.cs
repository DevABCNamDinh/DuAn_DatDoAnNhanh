using DuAn_DoAnNhanh.Data.Implements.Repository;
using DuAn_DoAnNhanh.Data.Interfaces.Repositories;
using DuAn_DoAnNhanh.Application.Interfaces.Service;
using DuAn_DoAnNhanh.Data.EF;
using DuAn_DoAnNhanh.Data.Entities;
using DuAn_DoAnNhanh.Data.ViewModel;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DuAn_DoAnNhanh.Data.Interface.UnitOfWork;
using DuAn_DoAnNhanh.Data.Enum;
using BCrypt.Net;

namespace DuAn_DoAnNhanh.Application.Implements.Service
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;

        public UserService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IEnumerable<User> GetAllUser()
        {
            return _unitOfWork.UserRepo.GetAll();
        }

        public User GetUserById(Guid id)
        {
            return _unitOfWork.UserRepo.GetById(id);
        }

        public User GetUserWithBills(Guid userId)
        {
            var user = _unitOfWork.UserRepo.GetById(userId); // Lấy thông tin người dùng
            if (user != null)
            {
                user.Orderes = GetUserBills(userId); // Tải hóa đơn cho người dùng
            }

            return user;
        }
        private List<Bill> GetUserBills(Guid userId)
        {
            return _unitOfWork.BillRepo.GetAll().Where(b => b.UserID == userId).ToList(); // Lấy hóa đơn của người dùng
        }


        public User? Login(LoginViewModel login)
        {
            if (string.IsNullOrWhiteSpace(login.Email) || string.IsNullOrWhiteSpace(login.Password))
                return null;           
            var user = _unitOfWork.UserRepo
                .GetAll()
                .FirstOrDefault(u => u.Email== login.Email.Trim().ToLower()&& BCrypt.Net.BCrypt.Verify(login.Password, u.Password));
            return user;
        }



        public User Register(User user)
        {
            if (user == null)
                throw new ArgumentNullException(nameof(user));

            // 1. Chuẩn hoá email
            var normalizedEmail = user.Email.Trim().ToLowerInvariant();
            if (string.IsNullOrWhiteSpace(normalizedEmail))
                throw new ArgumentException("Email không được để trống.");

            // 2. Kiểm tra trùng email
            bool exists = _unitOfWork.UserRepo
                .GetAll()
                .Any(u => u.Email.ToLowerInvariant() == normalizedEmail);
            if (exists)
                throw new InvalidOperationException("Email đã tồn tại.");

            // 3. Gán các trường hệ thống
            user.UserID = Guid.NewGuid();
            user.Email = normalizedEmail;
            user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);
            user.CreateDate = DateTime.UtcNow;
            user.Role = Role.Customer;
            user.Status = Status.Activity;

            // 4. Thêm User và Cart
            _unitOfWork.UserRepo.Add(user);
            var cart = new Cart
            {
                CartID = Guid.NewGuid(),
                UserID = user.UserID
            };
            _unitOfWork.CartRepo.Add(cart);

            // 5. Lưu một lần cho cả hai
            _unitOfWork.Complete();  // hoặc SaveChanges()

            return user;
           


        }

    }
}
