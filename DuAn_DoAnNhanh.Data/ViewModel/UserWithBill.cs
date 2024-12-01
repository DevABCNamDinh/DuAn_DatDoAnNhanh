using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DuAn_DoAnNhanh.Data.ViewModel
{
    public  class UserWithBill
    {
        public Guid UserID { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public List<BillInfo> Bills { get; set; }

        public class BillInfo
        {
            public Guid BillID { get; set; }
            public DateTime BillDate { get; set; }
            public decimal TotalAmount { get; set; }
        }
    }
}
