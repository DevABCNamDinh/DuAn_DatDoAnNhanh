using DuAn_DoAnNhanh.Data.Entities;
using DuAn_DoAnNhanh.Data.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DuAn_DoAnNhanh.Data.ViewModel
{
    public class CheckOutViewModel
    {
        public Guid CartID { get; set; }
        public Guid AddressID { get; set; }
        public Guid StoreID { get; set; }
        public List<CartItem> CartItemes { get; set; }  = new List<CartItem>();
        public Address Address { get; set; } = new Address();
        public Store Store { get; set; } = new Store();
        public ReceivingType ReceivingType { get; set; } //phương thức nhận hàng
        public string ReceiverName { get; set; } = string.Empty;
        public string ReceiverPhone { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
    }
}
