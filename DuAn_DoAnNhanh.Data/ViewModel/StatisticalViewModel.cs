using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DuAn_DoAnNhanh.Data.ViewModel
{
    public class StatisticalViewModel
    {
        public decimal TotalAmount { get; set; }
        public int CompletedBillCount { get; set; }
        public int PendingBillCount { get; set; }
        public int ShippingBillCount { get; set; }

        // Dữ liệu cho các biểu đồ
        public List<int> MonthlyRevenue { get; set; } // Doanh thu theo tháng
        public List<int> CompletedOrders { get; set; } // Đơn hoàn thành theo tháng
        public List<int> ShippingOrders { get; set; } // Đơn đang giao theo tháng
        public List<int> CanceledOrders { get; set; } // Đơn bị hủy theo tháng



    }
}
