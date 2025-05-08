using DuAn_DoAnNhanh.Data.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DuAn_DoAnNhanh.Data.Entities
{
    public class Store
    {
        public Guid StoreID { get; set; }
        public string StoreName { get; set; } =string.Empty;
        public Status Status { get; set; }
        public DateTime CreateDate { get; set; }
        public Guid AddressID { get; set; }
        
        public virtual Address Address { get; set; } = null!;
        public virtual List<User> Users { get; set; } = null!;

        public virtual List<Bill> Bills { get; set; } = null!;


    }
}
