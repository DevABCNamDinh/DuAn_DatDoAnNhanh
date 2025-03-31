using DuAn_DoAnNhanh.Data.EF;
using DuAn_DoAnNhanh.Data.Entities;
using DuAn_DoAnNhanh.Data.Enum;
using DuAn_DoAnNhanh.Data.Interface.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DuAn_DoAnNhanh.Data.Implements.Repository
{
    public class CartItemRepository : GenericRepository<CartItem>, ICartItemRepository
    {
        private readonly MyDBContext _context;
        public CartItemRepository(MyDBContext context) : base(context)
        {
            _context = context;
        }

        public List<CartItem> GetCartItemsWithDetails(Guid cartId)
        {
            return _context.CartItems.Include(x => x.Cart)
                   .Include(y => y.Combo)
                   .ThenInclude(x => x.ProductComboes)
                   .ThenInclude(x => x.Product)
                   .Include(z => z.Product)
                   .Where(ci => ci.CartID == cartId
                   && (ci.Combo.Status == StatusCombo.Activity
                   || ci.Product.Status == StatusProduct.Activity)).ToList();

        }
    }
}
