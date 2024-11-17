using DuAn_DoAnNhanh.Application.Interfaces.Service;
using DuAn_DoAnNhanh.Data.Entities;
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
            //var cart = _cartService.GetCartFromUserId(userId); 
            //if (cart == null)
            //{
            //    return View("EmptyCart"); 
            //}

            //var cartItems = _cartService.GetCartItems(cart.CartID); 
            //return View(cartItems);
            var UserId = HttpContext.Session.GetString("UserId");
            var cart = _cartService.GetCartFromUserId(Guid.Parse(UserId));
            var cartItems = _cartService.GetCartItems(cart.CartID);
            return View(cartItems);
        }
        [HttpPost]
        public IActionResult AddToCart(Guid ProductId, int quantity)
        {
            var userId = HttpContext.Session.GetString("UserId");
            if (string.IsNullOrEmpty(userId))
            {
                return RedirectToAction("Login", "User");
            }
            try
            {
                _cartService.AddToCart(Guid.Parse(userId), ProductId, quantity);
                TempData["Message"] = $"{quantity} books have been added to your cart.";
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
            }
            return RedirectToAction("Details", "Book", new { id = ProductId });
        }

        [HttpPost]
        public IActionResult UpdateCartItem(Guid cartItemId, int quantity)
        {
            try
            {
                _cartService.UpdateCartItem(cartItemId, quantity);
                TempData["Message"] = "Cart item updated successfully.";
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
            TempData["Message"] = "Cart item removed successfully.";
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
                TempData["Message"] = "Cart cleared successfully.";
            }
            return RedirectToAction("Cart");
        }
    }
}
