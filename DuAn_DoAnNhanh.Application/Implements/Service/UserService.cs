using DuAn_DoAnNhanh.Application.Interfaces.Repositories;
using DuAn_DoAnNhanh.Application.Interfaces.Service;
using DuAn_DoAnNhanh.Data.EF;
using DuAn_DoAnNhanh.Data.Entities;
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
        private readonly MyDBContext _dbContext;

        public UserService(IGenericRepository<User> userRepository, 
            IGenericRepository<Cart> cartRepository, MyDBContext dbContext)
        {
            _cartRepository = cartRepository;
            _userRepository = userRepository;
            _dbContext = dbContext;
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
            _dbContext.Users.Add(user);
            _dbContext.SaveChanges();
            //_userRepository.insert(user);
            //_userRepository.save();

            var cart = new Cart()
            {
                CartID = Guid.NewGuid(),
                UserID = user.UserID
            };
            _dbContext.Carts.Add(cart);
            _dbContext.SaveChanges();
            //_cartRepository.insert(cart);
            //_cartRepository.save();

            return user;
        }

    }
}
