using DuAn_DoAnNhanh.Data.EF;
using DuAn_DoAnNhanh.Data.Entities;
using DuAn_DoAnNhanh.Data.Interface.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DuAn_DoAnNhanh.Data.Implements.Repository
{
    public class UserRepository : GenericRepository<User>,IUserRepository
    {
        private readonly MyDBContext _context;
        public UserRepository(MyDBContext context) : base(context)
        {
            _context = context;
        }
		public User Authenticate(string email, string password)
		{
			return _context.Users.FirstOrDefault(u => u.Email == email && u.Password == password);
		}
	}
}
