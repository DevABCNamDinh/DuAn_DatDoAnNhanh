using DuAn_DoAnNhanh.Application.Interfaces.Service;
using DuAn_DoAnNhanh.Data.EF;
using DuAn_DoAnNhanh.Data.Entities;
using DuAn_DoAnNhanh.Data.Enum;
using DuAn_DoAnNhanh.Data.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DuAn_DoAnNhanh.Client.Controllers
{
    public class ComboController : Controller
    {
        private readonly IComboService _comboService;
        private readonly IProductService _productService;
        private readonly IComboDetailsService _comboDetailsService;


        private readonly MyDBContext _dbContext;
        public ComboController(MyDBContext dbContext,
            IComboService comboService, 
            IProductService productService,
            IComboDetailsService comboDetailsService)
        {
            _comboService=comboService;
            _productService=productService;
            _dbContext = dbContext;
            _comboDetailsService=comboDetailsService;
        }
        public IActionResult GetAll()
        {
            // Lấy danh sách combo
            var comboList = _comboService.GetAllCombo();

            // Tạo danh sách ViewModel để chứa combo và sản phẩm của mỗi combo
            var comboWithProductsList = comboList.Select(combo => new ComboWithProductsViewModel
            {

                Combo = combo,
                Products = _comboDetailsService.listProductInCombo(combo.ComboID).ToList() // Giả sử bạn có dịch vụ _productService để lấy sản phẩm theo ComboID
            }).ToList();

            return View(comboWithProductsList);
        }
        public IActionResult Details(Guid id)
        {
            var comboDetail = new ComboWithProductsViewModel();
            var listProductCombo = _dbContext.productCombos.Where(x => x.ComboID == id && x.Status == StatusCombo.Activity).ToList();
            var combo = _comboService.GetComboById(id);
            var products = new List<Product>();
            foreach (var product in listProductCombo)
            {
                var pr = _productService.GetProductById(product.ProductID);
                pr.Quantity = product.Quantity;
                products.Add(pr);
            }
            comboDetail.Combo = combo;
            comboDetail.Products = products;
            return View(comboDetail);
        }
    }
}
