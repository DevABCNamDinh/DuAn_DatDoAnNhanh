    using DuAn_DoAnNhanh.Data.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DuAn_DoAnNhanh.Data.Entities
{
    public class Combo
    {
        public Guid ComboID { get; set; }
        public string ComboName { get; set; }
        public string Image { get; set; }
        public decimal Price { get; set; }
        public StatusCombo Status { get; set; }
        public string? Description { get; set; }
        public DateTime CreteDate { get; set; }


        public virtual List<BillDetails> BillDetails { get; set; }
        public virtual List<ProductCombo> ProductComboes { get; set; }
        public virtual List<CartItem> CartItemes { get; set; }


    }
}
