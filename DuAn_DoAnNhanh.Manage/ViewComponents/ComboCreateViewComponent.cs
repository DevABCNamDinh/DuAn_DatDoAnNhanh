using DuAn_DoAnNhanh.Application.Interfaces.Service;
using DuAn_DoAnNhanh.Data.Entities;
using DuAn_DoAnNhanh.Data.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace DuAn_DoAnNhanh.Manage.ViewComponents
{
    public class ComboCreateViewComponent : ViewComponent
    {
        private readonly IProductService _productService;

        public ComboCreateViewComponent(IProductService productService)
        {
            _productService = productService;
        }

        public IViewComponentResult Invoke()
        {
            var model = new ComboCreateViewModel
            {
                AvailableProducts = _productService.GetAllProduct(),
                 SelectedProducts = new List<SelectedProduct>() // Khởi tạo danh sách
            };
            return View("~/Views/Shared/Components/ComboCreate/Default.cshtml", model);
        }
    }
}
