using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DuAn_DoAnNhanh.Data.Entities
{
    public class CartItem
    {
        public Guid CartItemID { get; set; }
        public Guid ProductID { get; set; }
        public Guid ComboID { get; set; }

        public Guid CartID { get; set; }
        public int Quantity { get; set; }
        public virtual Cart Cart { get; set; }
        public virtual Combo Combo { get; set; }

        public virtual Product Product { get; set; }
    }
}
