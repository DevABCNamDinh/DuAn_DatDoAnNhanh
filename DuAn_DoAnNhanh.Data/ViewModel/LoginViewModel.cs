using DuAn_DoAnNhanh.Data.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DuAn_DoAnNhanh.Data.ViewModel
{
    public class LoginViewModel
    {

        [Required(ErrorMessage = "Vui lòng nhập địa chỉ email.")]
        [StringLength(50)]
        [EmailAddress(ErrorMessage = "Địa chỉ email không hợp lệ.")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập mật khẩu.")]
        [StringLength(50)]
        public string Password { get; set; }
        public Role Role { get; set; }
    }
}
