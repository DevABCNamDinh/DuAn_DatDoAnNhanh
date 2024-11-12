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
        public Guid UserID { get; set; }

        public string AddressName { get; set; }
        public string NumberPhone { get; set; }
        public virtual User User { get; set; }
    }
}
