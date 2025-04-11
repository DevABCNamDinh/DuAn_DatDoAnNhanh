using DuAn_DoAnNhanh.Data.Entities;
using DuAn_DoAnNhanh.Data.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace DuAn_DoAnNhanh.Manage.ViewComponents
{
    public class StoreCreateViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            var store = new StoreViewModel(); // Tạo một đối tượng  mới
            return View(store); // Truyền đối tượng  vào view
        }
    }
}
