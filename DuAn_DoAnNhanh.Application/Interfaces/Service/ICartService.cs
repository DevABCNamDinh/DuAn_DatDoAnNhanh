
using DuAn_DoAnNhanh.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DuAn_DoAnNhanh.Application.Interfaces.Service
{
    public interface ICartService
    {
        void AddToCart(Guid userId, Guid ProductId, int quantity);
        List<CartItem> GetCartItems(Guid cartId);
        Cart GetCartFromUserId(Guid userId);
        void UpdateCartItem(Guid cartItemId, int quantity);
        void RemoveCartItem(Guid cartItemId);
        void ClearCart(Guid cartId);
    }
}
