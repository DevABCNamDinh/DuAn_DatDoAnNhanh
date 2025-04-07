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

       
        public User Login(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return null;
            var normalizedEmail = email.Trim().ToLower();

            return _unitOfWork.UserRepo
                .GetAll()
                .FirstOrDefault(u => u.Email.ToLower() == normalizedEmail);
        }


    
        public User Register(User user)
        {
            // Chuẩn hóa email
            var normalizedEmail = user.Email.Trim().ToLower();

            // Kiểm tra trùng email
            if (_unitOfWork.UserRepo.Find(u => u.Email.ToLower() == normalizedEmail) != null)
            {
                throw new Exception("Email đã tồn tại.");
            }

            // Mã hóa mật khẩu
            user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);

            // Lưu email đã chuẩn hóa
            user.Email = normalizedEmail;

            // Thêm user
            _unitOfWork.UserRepo.Add(user);
            _unitOfWork.UserRepo.SaveChanges();

            // Tạo cart
            var cart = new Cart()
            {
                CartID = Guid.NewGuid(),
                UserID = user.UserID
            };
            _unitOfWork.CartRepo.Add(cart);
            _unitOfWork.CartRepo.SaveChanges();

            return user;
        }






        public User Authenticate(string email, string password)
        {
            var user = _unitOfWork.UserRepo.GetAll()
        .FirstOrDefault(u => u.Email == email);

            if (user == null)
            {
                return null; // Không tìm thấy user
            }

            if (user.Role == Role.Customer)
            {
                return null; // Không có quyền truy cập
            }

            if (user.Password != password) // ⚠️ Cần hash mật khẩu trong thực tế
            {
                return null;
            }

            return user;
        }

        // Băm mật khẩu
        public User GetUserByEmail(string email)
        {
            return _unitOfWork.UserRepo.GetAll()
                .FirstOrDefault(u => u.Email == email);
        }



    }
}
