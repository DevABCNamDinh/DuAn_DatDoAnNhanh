
using DuAn_DoAnNhanh.Application.Implements.Service;
using DuAn_DoAnNhanh.Data.Entities;
using DuAn_DoAnNhanh.Data.Enum;
using DuAn_DoAnNhanh.Data.ViewModel;

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
        void AddComboToCart(Guid userId, Guid ComboID, int quantity);

        List<CartItem> GetCartItems(Guid cartId);
        Cart GetCartFromUserId(Guid userId);
        void UpdateCartItem(Guid cartItemId, int quantity);
        void RemoveCartItem(Guid cartItemId);
        void ClearCart(Guid cartId);
        void CheckOut(CheckOutViewModel checkOut);

        CheckOutViewModel CheckOutView(Guid userId, Guid cartId, ReceivingType ReceivingType);
    }
}
