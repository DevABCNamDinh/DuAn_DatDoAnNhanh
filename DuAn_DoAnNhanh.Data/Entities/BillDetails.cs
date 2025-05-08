using DuAn_DoAnNhanh.Data.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DuAn_DoAnNhanh.Data.Entities
{
    public class BillDetails
    {
        public Guid BillDetailsID { get; set; }
        public Guid BillID { get; set; }
        public string PCName { get; set; } = string.Empty;
        public string Image { get; set; } = string.Empty;

        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public decimal PriceEndow { get; set; }

        public StatusOrder Status { get; set; }

        public virtual Bill Order { get; set; } = null!;
     



    }
}
