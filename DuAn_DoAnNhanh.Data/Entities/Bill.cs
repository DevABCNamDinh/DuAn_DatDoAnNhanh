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
        public Guid? AddressID { get; set; }
        public Guid? StoreID { get; set; }
        public DateTime BillDate { get; set; }
        public StatusOrder Status { get; set; }
        public decimal TotalAmount { get; set; }
        public decimal TotalAmountEndow { get; set; }
        public ReceivingType ReceivingType { get; set; } //phương thức nhận hàng
        public PaymentType PaymentType { get; set; }
        public string? ReceiverName { get; set; } 
        public string? ReceiverPhone { get; set; }
        public string? Description { get; set; }


        public virtual User User { get; set; }
        public virtual Store Store { get; set; }
        public virtual Address Address { get; set; }


        public virtual List<BillDetails> BillDetails { get; set; }


    }
}
