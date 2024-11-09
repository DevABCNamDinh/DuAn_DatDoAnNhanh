using DuAn_DoAnNhanh.Application.Interfaces.Repositories;
using DuAn_DoAnNhanh.Application.Interfaces.Service;
using DuAn_DoAnNhanh.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DuAn_DoAnNhanh.Application.Implements.Service
{
    public class CartService : ICartService
    {
        private readonly IGenericRepository<Cart> _cartRepository;
        private readonly IGenericRepository<CartItem> _cartItemRepository;
        private readonly IGenericRepository<Product> _productRepository;
        public CartService(IGenericRepository<Cart> cartRepository,
            IGenericRepository<CartItem> cartItemRepository,
            IGenericRepository<Product> productRepository)
        {
            _cartItemRepository = cartItemRepository;
            _productRepository = productRepository;
            _cartRepository = cartRepository;
        }
        public void AddToCart(Guid userId, Guid ProductId, int quantity)
        {
            throw new NotImplementedException();
        }

        public void ClearCart(Guid cartId)
        {
            throw new NotImplementedException();
        }

        public Cart GetCartFromUserId(Guid userId)
        {
            throw new NotImplementedException();
        }

        public List<CartItem> GetCartItems(Guid cartId)
        {
            throw new NotImplementedException();
        }

        public void RemoveCartItem(Guid cartItemId)
        {
            throw new NotImplementedException();
        }

        public void UpdateCartItem(Guid cartItemId, int quantity)
        {
            throw new NotImplementedException();
        }
    }
}
