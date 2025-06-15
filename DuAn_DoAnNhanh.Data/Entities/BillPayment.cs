using DuAn_DoAnNhanh.Data.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DuAn_DoAnNhanh.Data.Entities
{
    public class BillPayment
    {
        public Guid BillPaymentID { get; set; }
        public Guid BillID { get; set; }
        public PaymentType PaymentType { get; set; }
        public float Amount { get; set; }
        public DateTime PaymentDate { get; set; }
        public StatusPayment PaymentStatus { get; set; }
        public virtual Bill Bill { get; set; } = null!;

    }
}
