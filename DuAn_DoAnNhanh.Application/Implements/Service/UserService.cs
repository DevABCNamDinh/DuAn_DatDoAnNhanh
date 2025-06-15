using DuAn_DoAnNhanh.Application.Implements.Repository;
using DuAn_DoAnNhanh.Application.Interfaces.Repositories;
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

namespace DuAn_DoAnNhanh.Application.Implements.Service
{
    public class UserService : IUserService
    {
        private readonly IGenericRepository<User> _userRepository;

        private readonly IGenericRepository<Cart> _cartRepository;
        private readonly IGenericRepository<Bill> _billRepository;
 

        public UserService(IGenericRepository<User> userRepository, 
            IGenericRepository<Cart> cartRepository, MyDBContext dbContext)
        {
            _cartRepository = cartRepository;
            _userRepository = userRepository;
            //_dbContext = dbContext;
        }

		public List<User> GetAllUser()
		{
			 return _userRepository.GetAll();
		}

		public User GetUserById(Guid id)
		{
			return _userRepository.GetById(id);
		}

        public User GetUserWithBills(Guid userId)
        {
            var user = _userRepository.GetById(userId); // Lấy thông tin người dùng
            if (user != null)
            {
                user.Orderes = GetUserBills(userId); // Tải hóa đơn cho người dùng
            }
           
            return user;
        }
        private List<Bill> GetUserBills(Guid userId)
        {
            return _billRepository.GetAll().Where(b => b.UserID == userId).ToList(); // Lấy hóa đơn của người dùng
        }

        public User Login(string Email, string Password)
        {
            return _userRepository.GetAll()
                 .FirstOrDefault(u=> u.Email == Email && u.Password == Password);
        }

        public User Register(User user)
        {
            if (_userRepository.FindBy(u => u.Email == user.Email) != null)
            {
                throw new Exception("Username already exists.");
            }

            _userRepository.insert(user);
            _userRepository.save();

            var cart = new Cart()
            {
                CartID = Guid.NewGuid(),
                UserID = user.UserID
            };

            _cartRepository.insert(cart);
            _cartRepository.save();

            return user;
           


        }
       







    }
}
