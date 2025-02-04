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
        public IActionResult GetAll(Guid? userId, StatusOrder? status)
        {
            var bills = _dbContext.Bill
            .Include(x => x.User)
            .OrderByDescending(x => x.BillDate)
            .AsQueryable();
            if (userId != null && userId !=Guid.Empty)
            {
                bills = bills.Where(x => x.UserID == userId);
                    
            }

            if (status != null)
            {
                // Lọc theo trạng thái nếu status được truyền
                bills = bills.Where(x => x.Status == status);
            }

            var billList = bills.OrderByDescending(x => x.BillDate).ToList();
            ViewBag.SelectedStatus = status; // Gửi trạng thái đã chọn về View
            return View(billList);

        }
        public IActionResult Details(Guid id) {
          
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
