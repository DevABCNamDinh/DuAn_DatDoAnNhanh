using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DuAn_DoAnNhanh.Data.ViewModel
{
    public class StoreViewModel
    {
        [Required(ErrorMessage ="Vui lòng nhập tên quản lý!")]
        public string ManagerName { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập email!")]
        [EmailAddress(ErrorMessage ="Email không đúng định dạng!")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập tên cửa hàng!")]
        public string StoreName { get; set; }
        [Required(ErrorMessage = "Vui lòng nhập số điện thoại!")]
        [RegularExpression(@"^\d{10}$", ErrorMessage = "Số điện thoại không hợp lệ!")]
        public string NumberPhone { get; set; }
        [Required(ErrorMessage = "Vui lòng chọn tỉnh/ thành phố!")]
        public string Province { get; set; } //tỉnh
        [Required(ErrorMessage = "Vui lòng chọn quận/ huyện!")]
        public string District { get; set; }//huyện
        [Required(ErrorMessage = "Vui lòng chọn xã/ phường!")]
        public string Ward { get; set; }//xã
        [Required(ErrorMessage = "Vui lòng nhập địa chỉ cụ thể!")]
        public string SpecificAddress { get; set; }//địa chỉ cụ thể
    }
}
