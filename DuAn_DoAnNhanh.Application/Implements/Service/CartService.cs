﻿using DuAn_DoAnNhanh.Application.Interfaces.Repositories;
using DuAn_DoAnNhanh.Application.Interfaces.Service;
using DuAn_DoAnNhanh.Data.EF;
using DuAn_DoAnNhanh.Data.Entities;
using DuAn_DoAnNhanh.Data.Enum;
using DuAn_DoAnNhanh.Data.ViewModel;
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
        private readonly IGenericRepository<Bill> _billRepository;
        private readonly IGenericRepository<BillDetails> _billDetailsRepository;


        private readonly IGenericRepository<Combo> _comboRepository;
        private readonly MyDBContext _context;

        public CartService(IGenericRepository<Cart> cartRepository,
            IGenericRepository<CartItem> cartItemRepository,
            IGenericRepository<Product> productRepository,
            IGenericRepository<Combo> comboRepository,
            IGenericRepository<Bill> billRepository,
            IGenericRepository<BillDetails> billDetailsRepository,

            MyDBContext context)
        {
            _cartItemRepository = cartItemRepository;
            _productRepository = productRepository;
            _cartRepository = cartRepository;
            _comboRepository = comboRepository;
            _billRepository = billRepository;
            _billDetailsRepository = billDetailsRepository;
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

       

        public void CheckOut(Guid cartId,Guid addressId, Guid storeId)
        {
            var cartItems = GetCartItems(cartId);
            //bill
            Bill bill = new Bill();
            bill.UserID = _cartRepository.GetById(cartId).UserID;
            bill.AddressID = addressId;
            bill.StoreID = storeId;
            bill.BillDate = DateTime.Now;   
            bill.TotalAmount = 0;
            bill.TotalAmountEndow = 0;
            bill.PaymentType = PaymentType.Cash;
            bill.ReceivingType = ReceivingType.HomeDelivery;
            bill.Status = StatusOrder.Pending;
            _billRepository.insert(bill);
            _billRepository.save();
            //BillDetails
            
            foreach (var items in cartItems)
            {
                if(items.ProductID != null&&items.ComboID==null) {
                    
                    BillDetails billDetails = new BillDetails();
                    billDetails.BillID=bill.BillID;
                    billDetails.PCName = items.Product.ProductName;
                    billDetails.Image = items.Product.ImageUrl;
                    billDetails.Price = items.Product.Price;
                    billDetails.Quantity = items.Quantity;
                    billDetails.PriceEndow = items.Product.Price;
                    billDetails.Status=StatusOrder.Activity;
                    _billDetailsRepository.insert(billDetails);
                    _billDetailsRepository.save();
                    
                }else if(items.ProductID == null && items.ComboID != null)
                {
                  
                    BillDetails billDetails = new BillDetails();
                    billDetails.BillID = bill.BillID;
                    string comboName=" ";
                    foreach (var item in items.Combo.ProductComboes.ToList())
                    {
                        comboName = comboName +" "+item.Quantity+" "+ item.Product.ProductName;
                    }
                    billDetails.PCName = items.Combo.ComboName +"("+ comboName+")";
                    billDetails.Image = items.Combo.Image;
                    billDetails.Price = items.Combo.Price;
                    billDetails.Quantity = items.Quantity;
                    billDetails.PriceEndow = items.Combo.SetupPrice.Value;
                    billDetails.Status = StatusOrder.Activity;
                    _billDetailsRepository.insert(billDetails);
                    _billDetailsRepository.save();
                }
            }

            var listBillDetails = _billDetailsRepository.GetAll().Where(x=>x.BillID==bill.BillID);
            decimal TotalAmount = listBillDetails.Sum(x => x.Price*x.Quantity);
            decimal TotalAmountEndow = listBillDetails.Sum(x => x.PriceEndow * x.Quantity);
            var billUpdate = _billRepository.GetById(bill.BillID);
            bill.TotalAmount= TotalAmount;
            bill.TotalAmountEndow= TotalAmountEndow;
            _billRepository.update(bill);
            _billRepository.save();


        }

        public CheckOutViewModel CheckOutView(Guid userId, Guid cartId)
        {
            try
            {
                var addresses = _context.addresses.Where(x => x.UserID == userId).ToList();
                var stores = _context.Stores.Where(x=>x.Status== Status.Activity).ToList();
                var cartItems = _context.CartItems.Include(x => x.Cart)
                   .Include(y => y.Combo)
                   .ThenInclude(x => x.ProductComboes)
                   .ThenInclude(x => x.Product)
                   .Include(z => z.Product)
                   .Where(ci => ci.CartID == cartId
                   && (ci.Combo.Status ==StatusCombo.Activity
                   || ci.Product.Status == StatusProduct.Activity)).ToList();
            return new CheckOutViewModel
            {
                Address = addresses,
                Stores = stores,
                cartItemes = cartItems
            };
            }
            catch (Exception)
            {

                throw;
            }
          
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
                .Where(ci => ci.CartID == cartId
                &&(ci.Combo.Status==StatusCombo.Activity
                ||ci.Product.Status==StatusProduct.Activity)).ToList();
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

        public void UpdateCartItem(Guid cartItemId, int quantity)
        {
            var cartItem = _cartItemRepository.GetById(cartItemId);         
            cartItem.Quantity = quantity;
            _cartItemRepository.update(cartItem);
            _cartItemRepository.save();
        }

       
    }
}
