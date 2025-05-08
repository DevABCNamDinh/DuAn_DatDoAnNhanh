using DuAn_DoAnNhanh.Data.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DuAn_DoAnNhanh.Data.Entities
{
    public class User
    {
        public Guid UserID { get; set; }
        public Guid? StoreID { get; set; }


        public string FullName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public DateTime CreateDate { get; set; }
        public Status Status { get; set; }
        public Role Role { get; set; }
        public virtual Cart Cart { get; set; }
        public virtual Store? Store { get; set; }

        public virtual List<Bill> Orderes { get; set; }
        public virtual List<Address> Addresses { get; set; }

    }
}
