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
        public Guid BillID { get; set; }
        public Guid UserID { get; set; }
        public Guid? StoreID { get; set; }
        public DateTime BillDate { get; set; }
        public StatusOrder Status { get; set; }
        public decimal TotalAmount { get; set; }
        public decimal TotalAmountEndow { get; set; }
      


        public virtual User User { get; set; } = null!;
        public virtual Store Store { get; set; } = null!;
        public virtual BillDelivery BillDelivery { get; set; } = null!;


        public virtual List<BillDetails> BillDetails { get; set; } = null!;
        public virtual List<BillPayment> BillPayments { get; set; } = null!;
        public virtual List<BillNotes> BillNotes { get; set; } = null!;



    }
}
