using DuAn_DoAnNhanh.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DuAn_DoAnNhanh.Data.ViewModel
{
    public class CheckOutViewModel
    {
        public List<CartItem> cartItemes { get; set; }
        public List<Address> Address { get; set; }
        public List<Store> Stores { get; set; }
    }
}
