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
        public Guid? StoreID { get; set; }

        public string FullName { get; set; }
        public string NumberPhone { get; set; }
        public string Province { get; set; } //tỉnh
        public string District { get; set; }//huyện
        public string Ward { get; set; }//xã
        public string SpecificAddress { get; set; }//địa chỉ cụ thể
        public double Latitude { get; set; } //vĩ độ
        public double Longitude { get; set; } // kinh độ
        public string FullAddress { get; set; }
        public DateTime CreateDate { get; set; }
        public AddressType? AddressType { get; set; }
        public Status Status { get; set; }
        public string? Description { get; set; }

        public virtual User User { get; set; }
        public virtual Store Store { get; set; }
        public virtual List<Bill> Bills { get; set; }

    }
}
