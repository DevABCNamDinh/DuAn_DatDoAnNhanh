using DuAn_DoAnNhanh.Data.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DuAn_DoAnNhanh.Data.Entities
{
    public class ProductCombo
    {
        public Guid ProductComboID { get; set; }
        public Guid ProductID { get; set; }
        public Guid ComboID { get; set; }
        public int Quantity { get; set; }
        public StatusCombo Status {  get; set; }
        public virtual Product Product { get; set; }
        public virtual Combo Combo { get; set; }


    }
}
