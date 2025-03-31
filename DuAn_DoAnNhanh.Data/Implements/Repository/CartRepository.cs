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
    public class CartRepository : GenericRepository<Cart>, ICartRepository
    {
        private readonly MyDBContext _context;
        public CartRepository(MyDBContext context) : base(context)
        {
            _context = context;
        }
        public Cart? GetCartByUserId(Guid userId)
        {
            return _context.Carts
                .Include(x => x.CartItems) // Load danh sách CartItems
                .FirstOrDefault(x => x.UserID == userId);
        }
    }
}
