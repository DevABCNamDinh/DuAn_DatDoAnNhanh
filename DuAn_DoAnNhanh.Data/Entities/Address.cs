using DuAn_DoAnNhanh.Data.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DuAn_DoAnNhanh.Data.Entities
{
    public class Address
    {
        public Guid AddressID { get; set; }
        public Guid? UserID { get; set; }

        public string FullName { get; set; } = string.Empty;
        public string NumberPhone { get; set; } = string.Empty;
        public string Province { get; set; } = string.Empty; //tỉnh
        public string District { get; set; } = string.Empty;//huyện
        public string Ward { get; set; } = string.Empty;//xã
        public string SpecificAddress { get; set; } = string.Empty;//địa chỉ cụ thể
        public double Latitude { get; set; } //vĩ độ
        public double Longitude { get; set; } // kinh độ
        public string FullAddress { get; set; } = string.Empty;
        public DateTime CreateDate { get; set; }
        public AddressType? AddressType { get; set; }
        public Status Status { get; set; }
        public string? Description { get; set; }

        public virtual User User { get; set; } = new User();
        public virtual Store Store { get; set; } = new Store();
        public virtual List<Bill> Bills { get; set; } = new List<Bill>();

    }
}
