using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DuAn_DoAnNhanh.Data.ViewModel
{
    public class StoreViewModel
    {
        public string ManagerName { get; set; }
        public string Email { get; set; }
        public string StoreName { get; set; }
        public string NumberPhone { get; set; }
        public string Province { get; set; } //tỉnh
        public string District { get; set; }//huyện
        public string Ward { get; set; }//xã
        public string SpecificAddress { get; set; }//địa chỉ cụ thể
    }
}
