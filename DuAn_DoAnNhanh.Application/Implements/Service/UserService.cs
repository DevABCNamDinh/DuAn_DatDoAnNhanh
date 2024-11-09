using DuAn_DoAnNhanh.Application.Interfaces.Repositories;
using DuAn_DoAnNhanh.Application.Interfaces.Service;
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
        public UserService(IGenericRepository<User> userRepository, IGenericRepository<Cart> cartRepository)
        {
            _cartRepository = cartRepository;
            _userRepository = userRepository;
        }
        public User Login(string email, string password)
        {
            return _userRepository.GetAll()
             .FirstOrDefault(u => u.Email == email && u.Password == password);
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
