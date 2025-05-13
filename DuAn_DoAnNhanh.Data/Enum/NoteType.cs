using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DuAn_DoAnNhanh.Data.Enum
{
    public enum NoteType
    {
        /// <summary>
        /// Ghi chú chung
        /// </summary>
        General = 0,

        /// <summary>
        /// Ghi chú đặt hàng từ khách
        /// </summary>
        CustomerOrder = 1,

        /// <summary>
        /// Ghi chú giao hàng
        /// </summary>
        Delivery = 2,

        /// <summary>
        /// Ghi chú thanh toán
        /// </summary>
        Payment = 3,

        /// <summary>
        /// Ghi chú nội bộ
        /// </summary>
        Internal = 4,

        /// <summary>
        /// Ghi chú hủy đơn
        /// </summary>
        Cancellation = 5,

        /// <summary>
        /// Ghi chú đổi trả
        /// </summary>
        Return = 6
    }
}
