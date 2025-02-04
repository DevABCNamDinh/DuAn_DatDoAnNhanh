using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DuAn_DoAnNhanh.Data.Enum
{
    public enum StatusOrder
    {
        Pending = 0,  /*-Chờ xác nhận*/
        Confirmed =1, /*- Đơn hàng đã được xác nhận bởi hệ thống hoặc người quản lý.*/
        Shipping = 2, /*- Đơn hàng đã được chuyển cho đơn vị vận chuyển.*/
        Completed = 3, /*- Đơn hàng đã được giao thành công đến khách hàng.*/
        Cancelled = 4, /*- Đơn hàng bị hủy bởi khách hàng hoặc người quản lý.*/       
        Activity = 5,
        InActivity =6,
    }
}
