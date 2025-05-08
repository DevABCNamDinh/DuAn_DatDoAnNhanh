
using DuAn_DoAnNhanh.Data.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DuAn_DoAnNhanh.Data.Entities
{
    public class Product
    {
        public Guid ProductID { get; set; }
       
        public string? ImageUrl { get; set;}
        
        public string ProductName { get; set; } = string.Empty;
        public string? Description { get; set;}
        public int Quantity { get; set;}
        public decimal Price { get; set;}
        public DateTime CreteDate { get; set;}
        public StatusProduct Status { get; set;}
        public virtual List<CartItem> CartItemes { get; set; } = new List<CartItem>();
      
        public virtual List<ProductCombo> ProductComboes { get; set; } = new List<ProductCombo>();



    }
}
