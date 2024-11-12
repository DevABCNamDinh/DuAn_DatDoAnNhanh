using DuAn_DoAnNhanh.Data.Entities;
using Microsoft.AspNetCore.Mvc;

namespace DuAn_DoAnNhanh.Manage.ViewComponents
{
    public class ComboCreateViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            var combo = new Combo(); // Tạo một đối tượng Product mới
            return View(combo); // Truyền đối tượng Product vào view
        }
    }
}
