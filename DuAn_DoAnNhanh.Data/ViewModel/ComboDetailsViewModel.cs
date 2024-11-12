using DuAn_DoAnNhanh.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DuAn_DoAnNhanh.Data.ViewModel
{
    public class ComboDetailsViewModel
    {
        public Guid ComboID { get; set; }
      

        public List<Product> Products { get; set; }
        public int Quantity { get; set; }
    }
}
