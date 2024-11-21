using DuAn_DoAnNhanh.Client.Models.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace DuAn_DoAnNhanh.Client.ViewComponents
{
    public class SignUpSignInViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            var user = new RegisterViewModel(); // Tạo một đối tượng  mới
            return View(user); // Truyền đối tượng  vào view
        }
    }
}
