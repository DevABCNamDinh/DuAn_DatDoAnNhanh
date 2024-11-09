using DuAn_DoAnNhanh.Data.Entities;
using Microsoft.AspNetCore.Mvc;

namespace DuAn_DoAnNhanh.Manage.ViewComponents
{
    public class ProductCreateViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            var product = new Product(); // Tạo một đối tượng Product mới
            return View(product); // Truyền đối tượng Product vào view
        }
    }
}
