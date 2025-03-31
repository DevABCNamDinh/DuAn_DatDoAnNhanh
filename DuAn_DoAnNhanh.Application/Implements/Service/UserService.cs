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

        public User Login(string Email, string Password)
        {
            return _unitOfWork.UserRepo.GetAll()
                 .FirstOrDefault(u=> u.Email == Email && u.Password == Password);
        }

        public User Register(User user)
        {
            if (_unitOfWork.UserRepo.Find(u => u.Email == user.Email) != null)
            {
                throw new Exception("Username already exists.");
            }

            _unitOfWork.UserRepo.Add(user);
            _unitOfWork.UserRepo.SaveChanges();

            var cart = new Cart()
            {
                CartID = Guid.NewGuid(),
                UserID = user.UserID
            };

            _unitOfWork.CartRepo.Add(cart);
            _unitOfWork.CartRepo.SaveChanges();

            return user;
        }



       


       
    }
}
