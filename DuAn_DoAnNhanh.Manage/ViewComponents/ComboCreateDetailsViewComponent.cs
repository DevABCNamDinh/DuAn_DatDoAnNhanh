using DuAn_DoAnNhanh.Application.Interfaces.Service;
using DuAn_DoAnNhanh.Data.EF;
using DuAn_DoAnNhanh.Data.Entities;
using DuAn_DoAnNhanh.Data.Enum;
using DuAn_DoAnNhanh.Data.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace DuAn_DoAnNhanh.Manage.ViewComponents
{
    public class ComboCreateDetailsViewComponent : ViewComponent
    {
        private readonly IProductService _productService;
        private readonly MyDBContext _dbContext;


        public ComboCreateDetailsViewComponent(IProductService productService, MyDBContext dbContext)
        {
            _productService = productService;
            _dbContext = dbContext;
        }
        public IViewComponentResult Invoke(Guid comboID)
        {
            var listProductCombo = _dbContext.productCombos.Where(x => x.ComboID == comboID&&x.Status==StatusCombo.Activity).ToList();
            var products = new List<Product>();
            foreach (var product in listProductCombo)
            {
                var pr = _productService.GetProductById(product.ProductID);
                pr.Quantity = product.Quantity;
                products.Add(pr);
            }

            var productList = _productService.GetAllProduct().Where(x => !products.Any(p => p.ProductID == x.ProductID)); // Tạo một đối tượng Product mới
            var comboDetail = new ComboDetailsViewModel();
            comboDetail.ComboID = comboID;
            comboDetail.Products = productList.ToList();
            return View(comboDetail); // Truyền đối tượng Product vào view
        }
    }
}
