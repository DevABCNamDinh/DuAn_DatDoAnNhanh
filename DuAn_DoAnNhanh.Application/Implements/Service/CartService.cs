using DuAn_DoAnNhanh.Application.Interfaces.Repositories;
using DuAn_DoAnNhanh.Application.Interfaces.Service;
using DuAn_DoAnNhanh.Data.EF;
using DuAn_DoAnNhanh.Data.Entities;
using Microsoft.EntityFrameworkCore;
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

        private readonly IGenericRepository<Combo> _comboRepository;
        private readonly MyDBContext _context;

        public CartService(IGenericRepository<Cart> cartRepository,
            IGenericRepository<CartItem> cartItemRepository,
            IGenericRepository<Product> productRepository,
            IGenericRepository<Combo> comboRepository,

            MyDBContext context)
        {
            _cartItemRepository = cartItemRepository;
            _productRepository = productRepository;
            _cartRepository = cartRepository;
            _comboRepository = comboRepository;
            _context = context;
        }

        public void AddComboToCart(Guid userId, Guid ComboID, int quantity)
        {
            var cart = GetCartFromUserId(userId);
            if (cart.CartItems == null)
            {
                cart.CartItems = new List<CartItem>();
            }
            var cartItem = cart.CartItems.FirstOrDefault(ci => ci.ComboID == ComboID);


            if (cartItem != null)
            {
                //if (cartItem.Quantity + quantity > cartItem.Product.Quantity)
                //{
                //    throw new Exception($"Cannot add more than {cartItem.Product.Quantity} . co trong gio");
                //}
                cartItem.Quantity += quantity;
                _cartItemRepository.update(cartItem);
            }
            else
            {
                cartItem = new CartItem
                {
                    CartItemID = Guid.NewGuid(),
                    CartID = cart.CartID,
                    ComboID = ComboID,
                    Quantity = quantity,
                    Combo = _comboRepository.GetById(ComboID),
                    Cart = _cartRepository.GetById(cart.CartID)
                };
                //if (quantity > cartItem.Product.Quantity)
                //{
                //    throw new Exception($"Cannot add more than {cartItem.Product.Quantity}.");
                //}
                _cartItemRepository.insert(cartItem);
            }
            _cartItemRepository.save();
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
                //if (cartItem.Quantity + quantity > cartItem.Product.Quantity)
                //{
                //    throw new Exception($"Cannot add more than {cartItem.Product.Quantity} . co trong gio");
                //}
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
                //if (quantity > cartItem.Product.Quantity)
                //{
                //    throw new Exception($"Cannot add more than {cartItem.Product.Quantity}.");
                //}
                _cartItemRepository.insert(cartItem);
            }
            _cartItemRepository.save();
        }

        public void ClearCart(Guid cartId)
        {
            var cart = _cartRepository.GetById(cartId);
            if (cart != null)
            {
                foreach (var cartItem in cart.CartItems)
                {
                    _cartItemRepository.delete(cartItem);
                }
                _cartItemRepository.save();
            }
        }

        public Cart GetCartFromUserId(Guid userId)
        {
            return _context.Carts.Include(x => x.CartItems).FirstOrDefault(x => x.UserID == userId);
        }

        public List<CartItem> GetCartItems(Guid cartId)
        {
            var cartItems = _context.CartItems.Include(x=>x.Cart)
                .Include(y=>y.Combo)
                .ThenInclude(x=>x.ProductComboes)
                .ThenInclude(x => x.Product)
                .Include(z=>z.Product) 
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
            var cartItem = _cartItemRepository.GetById(cartItemId);
            if (cartItem != null)
            {
                _cartItemRepository.delete(cartItem);
                _cartItemRepository.save();
            }
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
            var cartItem = _cartItemRepository.GetById(cartItemId);         
            cartItem.Quantity = quantity;
            _cartItemRepository.update(cartItem);
            _cartItemRepository.save();
        }

       
    }
}
