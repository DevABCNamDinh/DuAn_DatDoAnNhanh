using DuAn_DoAnNhanh.Application.Interfaces.Service;
using DuAn_DoAnNhanh.Data.Entities;
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
    
        public IViewComponentResult Invoke(string name)
        {
            List<Product> products;
            if(name==null)
            {
                 products = _productService.GetAllProduct();

            }
            else
            {
                 products = _productService.GetAllProduct().Where(x => x.ProductName.Contains(name)).ToList();

            }
            return View(products);
        }
    }
}
