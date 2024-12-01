using DuAn_DoAnNhanh.Data.Entities;
using DuAn_DoAnNhanh.Data.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DuAn_DoAnNhanh.Application.Interfaces.Service
{
    public interface IUserService
    {
        User Register(User user);
        User Login( string Email, string Password);


		List<User> GetAllUser();
		User GetUserById(Guid id);
        User GetUserWithBills(Guid userId); // Lấy thông tin người dùng cùng với hóa đơn
    }
}
