using DuAn_DoAnNhanh.Data.Interfaces.Repositories;
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
using DuAn_DoAnNhanh.Data.Interface.UnitOfWork;


namespace DuAn_DoAnNhanh.Application.Implements.Service
{
    public class CartService : ICartService
    {
        private readonly IUnitOfWork _unitOfWork;
        public CartService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }


        public void AddComboToCart(Guid userId, Guid ComboID, int quantity)
        {
            try
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
                    _unitOfWork.CartItemRepo.Update(cartItem);
                }
                else
                {
                    cartItem = new CartItem
                    {
                        CartItemID = Guid.NewGuid(),
                        CartID = cart.CartID,
                        ComboID = ComboID,
                        Quantity = quantity,

                        Combo = _unitOfWork.ComboRepo.GetById(ComboID),
                        Cart = _unitOfWork.CartRepo.GetById(cart.CartID)
                    };
                    //if (quantity > cartItem.Product.Quantity)
                    //{
                    //    throw new Exception($"Cannot add more than {cartItem.Product.Quantity}.");
                    //}
                    _unitOfWork.CartItemRepo.Add(cartItem);

                }
                _unitOfWork.Complete();
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi khi thêm Combo vào giỏ hàng: " + ex.Message);
            }
           
        }

        public void AddToCart(Guid userId, Guid ProductId, int quantity)
        {
            try
            {
                var cart = GetCartFromUserId(userId);
                if (cart.CartItems == null)
                {
                    cart.CartItems = new List<CartItem>();
                }
                var cartItem = cart.CartItems.FirstOrDefault(ci => ci.ProductID == ProductId);


                if (cartItem != null)
                {                  
                    cartItem.Quantity += quantity;
                    _unitOfWork.CartItemRepo.Update(cartItem);
                }
                else
                {
                    cartItem = new CartItem
                    {
                        CartItemID = Guid.NewGuid(),
                        CartID = cart.CartID,
                        ProductID = ProductId,
                        Quantity = quantity,
                        Product = _unitOfWork.ProductRepo.GetById(ProductId),
                        Cart = _unitOfWork.CartRepo.GetById(cart.CartID)
                    };                
                    _unitOfWork.CartItemRepo.Add(cartItem);

                }
                _unitOfWork.Complete();
            }
            catch (Exception ex)
            {

                throw new Exception("Lỗi khi thêm sản phẩm vào giỏ hàng: " + ex.Message);
            }
           
        }



        public void CheckOut(CheckOutViewModel checkOut)
        {
            _unitOfWork.BeginTransaction();

            try
            {
                var userId = _unitOfWork.CartRepo.GetById(checkOut.CartID).UserID;

                var cartItems = GetCartItems(checkOut.CartID);

                if (cartItems.Count!=0)
                {

                    //bill
                    Bill bill = new Bill();
                    bill.UserID = userId;
                    bill.StoreID = checkOut.StoreID;
                    bill.BillDate = DateTime.Now;
                    bill.TotalAmount = 0;
                    bill.TotalAmountEndow = 0;
                    bill.Status = StatusOrder.Pending;
                    _unitOfWork.BillRepo.Add(bill);
                    _unitOfWork.Complete();
                    //BillDelivery
                    var billDelivery = new BillDelivery();
                    billDelivery.BillID = bill.BillID;
                    if (checkOut.ReceivingType == ReceivingType.PickUpAtStore)
                    {
                        billDelivery.ReceivingType = ReceivingType.PickUpAtStore;
                        billDelivery.ReceiverName = checkOut.ReceiverName;
                        billDelivery.ReceiverPhone = checkOut.ReceiverPhone;
                        billDelivery.ReceiverAddress = "";
                    }
                    else
                    {
                        var address = _unitOfWork.AddressRepo.GetById(checkOut.AddressID);
                        billDelivery.ReceivingType = ReceivingType.HomeDelivery;
                        billDelivery.ReceiverName = address.FullName;
                        billDelivery.ReceiverPhone = address.NumberPhone;
                        billDelivery.ReceiverAddress = address.FullAddress;
                    }
                    _unitOfWork.BillDeliveryRepo.Add(billDelivery);
                    _unitOfWork.Complete();
                    if (!string.IsNullOrEmpty(checkOut.Description))
                    {
                        var billNotes = new BillNotes();
                        billNotes.BillID = bill.BillID;
                        billNotes.NoteType = NoteType.CustomerOrder;
                        billNotes.NoteContent = checkOut.Description ?? "";
                        billNotes.CreatedDate = DateTime.Now;
                        var user = _unitOfWork.UserRepo.GetById(userId);
                        billNotes.CreatedBy = $"Người dùng: {user.FullName}, Email: {user.Email}, Id: {user.UserID}";
                        _unitOfWork.BillNotesRepo.Add(billNotes);
                        _unitOfWork.Complete();
                    }

                    foreach (var items in cartItems)
                    {
                        if (items.ProductID != null && items.ComboID == null)
                        {

                            BillDetails billDetails = new BillDetails();
                            billDetails.BillID = bill.BillID;
                            billDetails.ItemType = ItemType.Product;
                            billDetails.ItemsName = items.Product.ProductName;
                            billDetails.Image = items.Product.ImageUrl ?? "";
                            billDetails.Price = items.Product.Price;
                            billDetails.Quantity = items.Quantity;
                            billDetails.PriceEndow = items.Product.Price;
                            billDetails.Status = StatusOrder.Activity;
                            _unitOfWork.BillDetailsRepo.Add(billDetails);
                            _unitOfWork.Complete();

                        }
                        else if (items.ProductID == null && items.ComboID != null)
                        {

                            BillDetails billDetails = new BillDetails();
                            billDetails.BillID = bill.BillID;
                            var comboItems = items.Combo.ProductComboes.ToList();
                            billDetails.ItemType = ItemType.Combo;
                            billDetails.ItemsName = items.Combo.ComboName;
                            billDetails.Image = items.Combo.Image;
                            billDetails.Price = items.Combo.Price;
                            billDetails.Quantity = items.Quantity;
                            billDetails.PriceEndow = items.Combo.SetupPrice ?? items.Combo.Price;
                            billDetails.Status = StatusOrder.Activity;
                            _unitOfWork.BillDetailsRepo.Add(billDetails);
                            _unitOfWork.Complete();
                            foreach (var item in comboItems)
                            {
                                var comboItemsArchive = new ComboItemsArchive();
                                comboItemsArchive.BillDetailsID = billDetails.BillDetailsID;
                                comboItemsArchive.ProductName = item.Product.ProductName;
                                comboItemsArchive.Quantity = item.Quantity;
                                comboItemsArchive.Image = item.Product.ImageUrl ?? "";
                                comboItemsArchive.Price = item.Product.Price;
                                _unitOfWork.ComboItemsArchiveRepo.Add(comboItemsArchive);
                                _unitOfWork.Complete();
                            }

                        }
                    }
                    // Tính toán tổng tiền
                    var listBillDetails = _unitOfWork.BillDetailsRepo.FindAll(x => x.BillID == bill.BillID);
                    decimal TotalAmount = listBillDetails.Sum(x => x.Price * x.Quantity);
                    decimal TotalAmountEndow = listBillDetails.Sum(x => x.PriceEndow * x.Quantity);

                    // Cập nhật tổng tiền cho hóa đơn
                    bill.TotalAmount = TotalAmount;
                    bill.TotalAmountEndow = TotalAmountEndow;
                    _unitOfWork.BillRepo.Update(bill);
                    _unitOfWork.Complete();
                    _unitOfWork.Commit();

                }
                else
                {
                    throw new Exception("Giỏ hàng trống!");
                }
            }
            catch (Exception ex)
            {
                _unitOfWork.Rollback();
                throw new Exception(ex.Message);
            }

        }

        public CheckOutViewModel CheckOutView(Guid userId, Guid cartId, ReceivingType ReceivingType)
        {
            try
            {
                var addresses = _unitOfWork.AddressRepo.Find(x => x.UserID == userId && x.AddressType == AddressType.Default);
                var store = _unitOfWork.StoresRepo.Find(x => x.Status == Status.Activity);
                store.Address = _unitOfWork.AddressRepo.GetById(store.AddressID);
                var cartItems = _unitOfWork.CartItemRepo.GetCartItemsWithDetails(cartId);
                return new CheckOutViewModel
                {
                    Address = addresses,
                    Store = store,
                    CartItemes = cartItems,
                    ReceivingType = ReceivingType,


                };
            }
            catch (Exception)
            {

                throw;
            }

        }

        public void ClearCart(Guid cartId)
        {
            var cart = _unitOfWork.CartRepo.GetById(cartId);
            if (cart != null)
            {
                //foreach (var cartItem in cart.CartItems)
                //{
                //    _unitOfWork.CartItemRepo.Delete(cartItem);

                //}
                _unitOfWork.CartItemRepo.DeleteRange(cart.CartItems);
                _unitOfWork.Complete();
            }
        }

        public Cart GetCartFromUserId(Guid userId)
        {
            return _unitOfWork.CartRepo.GetCartByUserId(userId);
        }

        public List<CartItem> GetCartItems(Guid cartId)
        {
            var cartItems = _unitOfWork.CartItemRepo.GetCartItemsWithDetails(cartId);
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
            var cartItem = _unitOfWork.CartItemRepo.GetById(cartItemId);
            if (cartItem != null)
            {
                _unitOfWork.CartItemRepo.Delete(cartItem);
                _unitOfWork.Complete();
            }
        }

        public void UpdateCartItem(Guid cartItemId, int quantity)
        {
            var cartItem = _unitOfWork.CartItemRepo.GetById(cartItemId);
            cartItem.Quantity = quantity;
            _unitOfWork.CartItemRepo.Update(cartItem);
            _unitOfWork.Complete();
        }


    }
}
