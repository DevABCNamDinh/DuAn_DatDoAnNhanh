using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DuAn_DoAnNhanh.Data.Enum
{
    public enum StatusPayment
    {
        /// <summary>
        /// Đơn hàng mới tạo, chưa thanh toán
        /// </summary>
        Pending = 0,

        /// <summary>
        /// Đã thanh toán thành công
        /// </summary>
        Paid = 1,

        /// <summary>
        /// Thanh toán thất bại
        /// </summary>
        Failed = 2,

        /// <summary>
        /// Đang xử lý thanh toán (với cổng thanh toán)
        /// </summary>
        Processing = 3,

        /// <summary>
        /// Đã hoàn tiền (toàn phần hoặc một phần)
        /// </summary>
        Refunded = 4,

        /// <summary>
        /// Đã hủy trước khi thanh toán
        /// </summary>
        Cancelled = 5,

        /// <summary>
        /// Đã hết hạn thanh toán
        /// </summary>
        Expired = 6,

        /// <summary>
        /// Tạm giữ tiền (authorized nhưng chưa capture)
        /// </summary>
        Authorized = 7
    }
}
