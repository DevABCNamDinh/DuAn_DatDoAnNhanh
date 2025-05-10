using DuAn_DoAnNhanh.Data.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DuAn_DoAnNhanh.Data.Entities
{
    public class Address
    {
        public Guid AddressID { get; set; }
        public Guid? UserID { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập họ tên.")]
        [StringLength(100, ErrorMessage = "Họ tên không được vượt quá 100 ký tự.")]
        public string FullName { get; set; } = string.Empty;
        [Required(ErrorMessage = "Vui lòng nhập số điện thoại.")]
        [Phone(ErrorMessage = "Số điện thoại không hợp lệ.")]
        [StringLength(20, ErrorMessage = "Số điện thoại không được vượt quá 20 ký tự.")]
        public string NumberPhone { get; set; } = string.Empty;

        [Required(ErrorMessage = "Vui lòng chọn tỉnh/thành phố.")]
        public string Province { get; set; }  = string.Empty;//tỉnh
        [Required(ErrorMessage = "Vui lòng chọn quận/huyện.")]
        public string District { get; set; } = string.Empty;//huyện

        [Required(ErrorMessage = "Vui lòng chọn phường/xã.")]
        public string Ward { get; set; } = string.Empty;//xã
        [Required(ErrorMessage = "Vui lòng nhập địa chỉ cụ thể.")]
        [StringLength(255, ErrorMessage = "Địa chỉ cụ thể không được vượt quá 255 ký tự.")]
        public string SpecificAddress { get; set; } = string.Empty;//địa chỉ cụ thể

        public double Latitude { get; set; } //vĩ độ
        public double Longitude { get; set; } // kinh độ
        public string FullAddress { get; set; } = string.Empty;
        public DateTime CreateDate { get; set; }
        public AddressType? AddressType { get; set; }
        public Status Status { get; set; }
        public string? Description { get; set; }

        public virtual User User { get; set; } = null!;
        public virtual Store Store { get; set; } = null!;
        public virtual List<Bill> Bills { get; set; } =null!;

    }
}
