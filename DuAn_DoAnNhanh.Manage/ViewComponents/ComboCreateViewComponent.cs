using DuAn_DoAnNhanh.Data.ViewModel;
using DuAn_DoAnNhanh.Application.Interfaces.Service;
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
                AvailableProducts = _productService.GetAllProduct()
            };
            return View("~/Views/Shared/Components/ComboCreate/Default.cshtml", model);
        }
    }
}