using DuAn_DoAnNhanh.Data.Enum;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DuAn_DoAnNhanh.Data.Entities
{
    public class Bill
    {
        public Guid OrderID { get; set; }
        public Guid UserID { get; set; }

        public DateTime OrderDate { get; set; }
        public StatusOrder Status { get; set; }
        public virtual User User { get; set; }
        public virtual List<BillDetails> BillDetails { get; set; }


    }
}
