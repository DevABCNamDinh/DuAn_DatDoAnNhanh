using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DuAn_DoAnNhanh.Data.Entities
{
    public class ComboItemsArchive
    {
        public Guid ArchiveID { get; set; }
        public Guid BillDetailsID { get; set; }
        public string ProductName { get; set; } = string.Empty;
        public string Image { get; set; } = string.Empty;
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public virtual BillDetails BillDetails { get; set; } = null!;
    }
}
