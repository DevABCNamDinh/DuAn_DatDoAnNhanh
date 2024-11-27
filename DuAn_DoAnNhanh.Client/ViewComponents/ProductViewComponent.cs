using DuAn_DoAnNhanh.Application.Interfaces.Service;
using Microsoft.AspNetCore.Mvc;

namespace DuAn_DoAnNhanh.Client.ViewComponents
{
    public class ProductViewComponent : ViewComponent
    {
        private readonly IProductService _productService;
        public ProductViewComponent(IProductService productService)
        {
            _productService = productService;
        }
    
        public IViewComponentResult Invoke()
        {

            var products = _productService.GetAllProduct();
            return View(products);
        }
    }
}
