using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DuAn_DoAnNhanh.Data.Enum
{
    public enum StatusOrder
    {
        Pending = 0 , /*- Đơn hàng mới tạo, đang chờ xử lý.*/
        Confirmed =1, /*- Đơn hàng đã được xác nhận bởi hệ thống hoặc người quản lý.*/
        Processing=2, /*- Đơn hàng đang được xử lý, có thể là đang đóng gói hoặc chuẩn bị để giao.*/
        Shipped=3, /*- Đơn hàng đã được chuyển cho đơn vị vận chuyển.*/
        Delivered=4, /*- Đơn hàng đã được giao thành công đến khách hàng.*/
        Canceled=5, /*- Đơn hàng bị hủy bởi khách hàng hoặc người quản lý.*/
        Returned =6,/*- Đơn hàng đã được trả lại sau khi giao.*/
        Refunded =7,/* - Đơn hàng đã hoàn tất việc hoàn tiền cho khách hàng (nếu có).*/
    }
}
