using DuAn_DoAnNhanh.Data.Entities;
using DuAn_DoAnNhanh.Data.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DuAn_DoAnNhanh.Data.Interface.Repositories
{
    public interface IUserRepository : IGenericRepository<User>
    {
        public interface IUserRepository
        {
			User Authenticate(string email, string password);
		}

    }
}
