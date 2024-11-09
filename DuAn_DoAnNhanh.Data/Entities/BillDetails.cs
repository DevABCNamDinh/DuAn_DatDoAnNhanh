using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DuAn_DoAnNhanh.Data.Entities
{
    public class BillDetails
    {
        public Guid OrderDetailsID { get; set; }
        public Guid ProductID { get; set; }
        public Guid OrderID { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public virtual Bill Order { get; set; }
        public virtual Combo Combo { get; set; }

        public virtual Product Product { get; set; }



    }
}
