using DuAn_DoAnNhanh.Application.Interfaces.Service;
using DuAn_DoAnNhanh.Data.Entities;
using DuAn_DoAnNhanh.Data.Enum;
using DuAn_DoAnNhanh.Data.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace DuAn_DoAnNhanh.Client.Controllers
{
    public class CartController : Controller
    {
        private readonly ICartService _cartService;
        public CartController(ICartService cartService)
        {
            _cartService = cartService;
        }
        public IActionResult Cart ()
        {
        try
        {
            var UserId = HttpContext.Session.GetString("UserId");
            var cart = _cartService.GetCartFromUserId(Guid.Parse(UserId));
            var cartItems = _cartService.GetCartItems(cart.CartID);
            return View(cartItems);
        }
        catch (Exception)
        {
            TempData["OpenSignInModal"] = true;
            return RedirectToAction("Index", "Home");
        }
               
        }
    [HttpPost]
        public IActionResult AddToCart(Guid ProductId, int quantity)
        {
            var userId = HttpContext.Session.GetString("UserId");
            TempData["ReturnUrl"] = Request.Headers["Referer"].ToString();
            if (string.IsNullOrEmpty(userId))
            {
            // Trả về đoạn script mở modal
            TempData["OpenSignInModal"] = true;
               
            return Redirect(TempData["ReturnUrl"].ToString());
             
            }
            try
            {
                _cartService.AddToCart(Guid.Parse(userId), ProductId, quantity);        
                TempData["Message"] = $"{quantity} sản phẩm được thêm vào giỏ hàng";
            return Redirect(TempData["ReturnUrl"].ToString());
        }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
            }
        return Redirect(TempData["ReturnUrl"].ToString());
    }
    [HttpPost]
    public IActionResult AddComboToCart(Guid ComboId, int quantity)
    {
        var userId = HttpContext.Session.GetString("UserId");
        TempData["ReturnUrl"] = Request.Headers["Referer"].ToString();
        if (string.IsNullOrEmpty(userId))
        {
            // Trả về đoạn script mở modal
            TempData["OpenSignInModal"] = true;
                
                
               
        }
        try
        {
            _cartService.AddComboToCart(Guid.Parse(userId), ComboId, quantity);
            TempData["Message"] = $"{quantity} sản phẩm được thêm vào giỏ hàng";
            return Redirect(TempData["ReturnUrl"].ToString());
        }
        catch (Exception ex)
        {
            TempData["Error"] = ex.Message;
        }
        return Redirect(TempData["ReturnUrl"].ToString());
    }

    [HttpPost]
        public IActionResult UpdateCartItem(Guid cartItemId, int quantity)
        {
            try
            {
                _cartService.UpdateCartItem(cartItemId, quantity);
                TempData["Message"] = "Cập nhật giỏ hàng thành công";
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
            }
            return RedirectToAction("Cart");
        }

        [HttpPost]
        public IActionResult RemoveCartItem(Guid cartItemId)
        {
            _cartService.RemoveCartItem(cartItemId);
            TempData["Message"] = "Đã xóa sản phẩm ra khỏi giỏ hàng.";
            return RedirectToAction("Cart");
        }

        [HttpPost]
        public IActionResult ClearCart()
        {
            var userId = HttpContext.Session.GetString("UserId");
            var cart = _cartService.GetCartFromUserId(Guid.Parse(userId));
            if (cart != null)
            {
                _cartService.ClearCart(cart.CartID);
                TempData["Message"] = "Đã xóa sạch giỏ hàng";
            }
            return RedirectToAction("Cart");
        }
        [HttpGet]
        public IActionResult CheckOut(ReceivingType receivingType)
        {
            try
            {  
                var userId = HttpContext.Session.GetString("UserId");
                var cart = _cartService.GetCartFromUserId(Guid.Parse(userId));
                var checkOutView = _cartService.CheckOutView(Guid.Parse(userId), cart.CartID,receivingType);
                if (checkOutView is not null)
                {
                    return View(checkOutView);
                }
                return NoContent();             
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"Lỗi: {ex.Message}";
                return NoContent();
            }

        }
        [HttpPost]
        public IActionResult CheckOut(CheckOutViewModel checkOut)
        {
            var userId = HttpContext.Session.GetString("UserId");
            var cart = _cartService.GetCartFromUserId(Guid.Parse(userId));
            var checkOutView = _cartService.CheckOutView(Guid.Parse(userId), cart.CartID, checkOut.ReceivingType);
            try
            {
                if (checkOut.AddressID == Guid.Empty|| checkOut.StoreID == Guid.Empty || checkOut.ReceiverName==string.Empty||checkOut.ReceiverPhone==string.Empty)
                {
                    if (checkOut.StoreID == Guid.Empty)
                    {
                        TempData["ErrorStore"] = "Cửa hàng đang bị lỗi!";
                    }
                    if (checkOut.ReceivingType == ReceivingType.HomeDelivery)
                    {
                        if (checkOut.AddressID == Guid.Empty)
                        {
                            TempData["ErrorAddress"] = "Địa chỉ không được để trống!";
                            return RedirectToAction("CheckOut", new { receivingType = ReceivingType.HomeDelivery });
                        }

                    }
                    if(checkOut.ReceivingType == ReceivingType.PickUpAtStore)
                    {
                        if (checkOut.ReceiverName == string.Empty || checkOut.ReceiverPhone == string.Empty)
                        {
                            TempData["ErrorReceiver"] = "Thông tin không được để trống!";
                            return RedirectToAction("CheckOut", new { receivingType = ReceivingType.PickUpAtStore });
                        }
                    }


                   
                   
                }
                checkOut.CartID=cart.CartID;
                _cartService.CheckOut(checkOut);
                TempData["Message"] = "Đặt hàng thành công.";
                if (cart != null)
                {
                    _cartService.ClearCart(cart.CartID);
                }
                return RedirectToAction("Index", "Home");


            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"Đặt hàng thất bại! Lỗi: {ex.Message}";          
                return View(checkOutView);
            }
        }

    }
}
