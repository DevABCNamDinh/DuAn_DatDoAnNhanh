using DuAn_DoAnNhanh.Data.Enum;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DuAn_DoAnNhanh.Data.Entities
{
    public class BillDelivery
    {
        public Guid DeliveryID { get; set; }
        public Guid BillID { get; set; }
        public ReceivingType ReceivingType { get; set; } //phương thức nhận hàng
        public string ReceiverName { get; set; } = string.Empty; //tên người nhận
        public string ReceiverPhone { get; set; } = string.Empty; //số điện thoại người nhận
        public string ReceiverAddress { get; set; } = string.Empty; //địa chỉ người nhận
        public decimal Distance { get; set; }
        public decimal DeliveryFee { get; set; }
        public virtual Bill Bill { get; set; } = null!;
    }
}
