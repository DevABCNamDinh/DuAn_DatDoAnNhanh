using DuAn_DoAnNhanh.Application.Interfaces.Repositories;
using DuAn_DoAnNhanh.Application.Interfaces.Service;
using DuAn_DoAnNhanh.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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

            var cart = GetCartFromUserId(userId);
            if (cart.CartItems == null)
            {
                cart.CartItems = new List<CartItem>();
            }
            var cartItem = cart.CartItems.FirstOrDefault(ci => ci.ProductID == ProductId);
            if (cartItem != null)
            {
                if (cartItem.Quantity + quantity > cartItem.Product.Quantity)
                {
                    throw new Exception($"Cannot add more than {cartItem.Product.Quantity} .");
                }
                cartItem.Quantity += quantity;
                _cartItemRepository.update(cartItem);
            }
            else
            {
                cartItem = new CartItem
                {
                    CartItemID = Guid.NewGuid(),
                    CartID = cart.CartID,
                    ProductID = ProductId,
                    Quantity = quantity,
                    Product = _productRepository.GetById(ProductId),
                    Cart = _cartRepository.GetById(cart.CartID)
                };
                if (quantity > cartItem.Product.Quantity)
                {
                    throw new Exception($"Cannot add more than {cartItem.Product.Quantity}.");
                }
                _cartItemRepository.insert(cartItem);
            }
            _cartItemRepository.save();
        }

        public void ClearCart(Guid cartId)
        {
            throw new NotImplementedException();
        }

        public Cart GetCartFromUserId(Guid userId)
        {
            return _cartRepository.GetAll().FirstOrDefault(c => c.UserID == userId);
        }

        public List<CartItem> GetCartItems(Guid cartId)
        {
            var cartItems = _cartItemRepository.GetAll()
                .Where(ci => ci.CartID == cartId).ToList();
            if (cartItems != null)
            {
                return cartItems;
            }
            else
            {
                return new List<CartItem>();
            }
        }

        public void RemoveCartItem(Guid cartItemId)
        {
            throw new NotImplementedException();
        }

       

        //public IEnumerable<CartItemViewModel> GetCartItemsWithDetails(Guid cartId)
        //{
        //    // Lấy danh sách CartItems theo CartId
        //    var cartItems = _cartItemRepository.GetAll()
        //        .Where(ci => ci.CartID == cartId)
        //        .ToList();

        //    // Lấy danh sách ProductIDs để giảm số lần truy vấn
        //    var productIds = cartItems.Select(ci => ci.ProductID).Distinct().ToList();

        //    // Lấy toàn bộ thông tin sản phẩm từ ProductRepository
        //    var products = _productRepository.GetAll()
        //        .Where(p => productIds.Contains(p.ProductID))
        //        .ToDictionary(p => p.ProductID, p => p); // Chuyển sang Dictionary để tra cứu nhanh

        //    // Join CartItems với Product để tạo CartItemViewModel
        //    var cartItemDetails = cartItems
        //        .Select(ci =>
        //        {
        //            var product = products.ContainsKey(ci.ProductID) ? products[ci.ProductID] : null;
        //            return new CartItemViewModel
        //            {
        //                ProductId = ci.ProductID,
        //                ProductName = product?.ProductName ?? "Unknown Product", // Giá trị mặc định nếu không tìm thấy
        //                Price = product?.Price ?? 0, // Giá trị mặc định nếu không tìm thấy
        //                Quantity = ci.Quantity
        //            };
        //        })
        //        .ToList();

        //    return cartItemDetails; // Trả về danh sách CartItemViewModel
        //}

        public void UpdateCartItem(Guid cartItemId, int quantity)
        {
            throw new NotImplementedException();
        }

       
    }
}
