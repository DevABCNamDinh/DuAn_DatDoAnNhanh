using DuAn_DoAnNhanh.Application.Interfaces.Service;
using DuAn_DoAnNhanh.Data.ViewModel;
using DuAn_DoAnNhanh.Data.EF;
using DuAn_DoAnNhanh.Data.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace DuAn_DoAnNhanh.Client.ViewComponents
{
    public class EndowViewComponent : ViewComponent
    {     
        private readonly IComboDetailsService _comboDetailsService;
        private readonly MyDBContext _context;
        public EndowViewComponent(       
            IComboDetailsService comboDetailsService,
            MyDBContext context)
        {
            _comboDetailsService = comboDetailsService;
            _context = context;
        }

        public IViewComponentResult Invoke()
        {
            // Lấy danh sách combo
            var comboList = _context.Combos.Where(x=>x.SetupPrice!=null).ToList();

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
