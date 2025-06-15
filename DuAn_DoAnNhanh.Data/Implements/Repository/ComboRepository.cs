using DuAn_DoAnNhanh.Data.EF;
using DuAn_DoAnNhanh.Data.Entities;
using DuAn_DoAnNhanh.Data.Interface.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DuAn_DoAnNhanh.Data.Implements.Repository
{
    public  class ComboRepository : GenericRepository<Combo>,IComboRepository
    {
        private readonly MyDBContext _context;
        public ComboRepository(MyDBContext context) : base(context)
        {
            _context = context;
        }

        public Combo? GetComboWithDetails(Guid comboID)
        {
            return _context.Combos
                .Include(c => c.ProductComboes) // Load danh sách ProductComboes
                .ThenInclude(pc => pc.Product)  // Load Product trong ProductComboes
                .FirstOrDefault(c => c.ComboID == comboID);
        }
    }
}
