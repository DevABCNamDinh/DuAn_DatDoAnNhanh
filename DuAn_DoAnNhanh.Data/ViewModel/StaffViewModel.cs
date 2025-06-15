using DuAn_DoAnNhanh.Data.Enum;
using System.ComponentModel.DataAnnotations;

namespace DuAn_DoAnNhanh.Data.ViewModel
{
    public class StaffViewModel
    {
        public Guid UserID { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập họ tên")]
        public string FullName { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập email")]
        [EmailAddress(ErrorMessage = "Email không hợp lệ")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Vui lòng chọn vai trò")]
        public Role Role { get; set; }

        public Status Status { get; set; } = Status.Activity;
    }
}
