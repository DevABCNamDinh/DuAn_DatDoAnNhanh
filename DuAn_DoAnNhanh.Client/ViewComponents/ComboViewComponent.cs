using DuAn_DoAnNhanh.Application.Interfaces.Service;
using DuAn_DoAnNhanh.Data.EF;
using DuAn_DoAnNhanh.Data.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace DuAn_DoAnNhanh.Client.ViewComponents
{
    public class ComboViewComponent : ViewComponent
    {
        private readonly IComboService _comboService;    
        private readonly IComboDetailsService _comboDetailsService; 
        public ComboViewComponent(
            IComboService comboService,      
            IComboDetailsService comboDetailsService)
        {
            _comboService = comboService;   
            _comboDetailsService = comboDetailsService;
        }
        
        public IViewComponentResult Invoke()
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
    }
}
