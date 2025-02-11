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
        public string StoreName { get; set; }
        public Status Status { get; set; }
        public virtual Address Address { get; set; }
        public virtual User User { get; set; }

        public virtual List<Bill> Bills { get; set; }


    }
}
