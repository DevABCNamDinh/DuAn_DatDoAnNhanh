using DuAn_DoAnNhanh.Application.Interfaces.Service;
using DuAn_DoAnNhanh.Data.EF;
using DuAn_DoAnNhanh.Data.Enum;
using DuAn_DoAnNhanh.Data.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DuAn_DoAnNhanh.Manage.Controllers
{
    public class BillController : Controller
    {
        private readonly MyDBContext _dbContext;
        private readonly IBillService _billService;
        public BillController(MyDBContext dBContext,IBillService billService)
        {
            _billService = billService;
            _dbContext = dBContext;
        }
        public IActionResult GetAll()
        {
            //var billList= _billService.GetAllBill();
            var billList = _dbContext.Bill.Include(x=>x.User).ToList();
                
            return View(billList);
        }
        public IActionResult Details(Guid id) {
            //var bill = _billService.GetBillById(id);
            //var billDetails = _dbContext.BillDetailses.Where(x=>x.BillID==id).ToList();
            //List<BillProductComboViewModel> billProductComboViewModel = new List<BillProductComboViewModel>();
            //foreach (var product in billDetails)
            //{
            //    BillProductComboViewModel abc = new BillProductComboViewModel();
            //    if (product.ComboID != null && product.ProductID == null)
            //    {
            //        abc.Image = _dbContext.Combos.Find(product.ComboID).Image;
                    
            //        string NameCombo= _dbContext.Combos.Find(product.ComboID).ComboName+" Gồm: ";
            //        foreach(var combo in _dbContext.productCombos.Where(x => x.ComboID == product.ComboID&&x.Status== StatusCombo.Activity).ToList())
            //        {
            //            NameCombo = NameCombo+ combo.Quantity+" " + _dbContext.Products.Find(combo.ProductID).ProductName+" ";
            //        }
            //        abc.Name =NameCombo;
            //        abc.Type = "Combo";
            //    }
            //    if (product.ComboID == null && product.ProductID != null)
            //    {
            //        abc.Image = _dbContext.Products.Find(product.ProductID).ImageUrl;
            //        abc.Name = _dbContext.Products.Find(product.ProductID).ProductName;
            //        abc.Type = "Sản phẩm lẻ";
            //    }

            //    abc.Quantity=product.Quantity;
            //    abc.Price=product.Price;
            //    billProductComboViewModel.Add(abc);
            //}
            //BillViewModel billViewModel = new BillViewModel()
            //{
            //    Bill = bill,
            //    BillDetails = billProductComboViewModel
            //};
            var bill = _billService.GetBillById(id);
            var listBillDetails = _dbContext.BillDetailses.Where(x=>x.BillID == id).ToList();
            var billViewModel = new BillViewModel();
            billViewModel.Bill = bill;
            billViewModel.BillDetails = listBillDetails;
            return View(billViewModel);
        }
        [HttpPost]
        public IActionResult UpdateStatus(Guid billId, StatusOrder status)
        { 
            var billUpdate = _billService.GetBillById(billId);
            billUpdate.Status = status;
            _billService.UpdateBill(billUpdate);
            return RedirectToAction("Details","Bill",new { id =billId });
        }
    }
}
