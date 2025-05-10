using DuAn_DoAnNhanh.Data.EF;
using DuAn_DoAnNhanh.Data.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace DuAn_DoAnNhanh.Manage.Controllers
{
    public class StatisticalController : Controller
    {
        private readonly MyDBContext _dbContext;

        public StatisticalController(MyDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IActionResult Index()
        {
            var bill = _dbContext.Bill.Where(b => b.Status == Data.Enum.StatusOrder.Completed).ToList();
            var bill1 = _dbContext.Bill.Where(b => b.Status == Data.Enum.StatusOrder.Pending).ToList();
            var bill2 = _dbContext.Bill.Where(b => b.Status == Data.Enum.StatusOrder.Shipping).ToList();

            var totalAmount = bill.Sum(b => b.TotalAmountEndow);
            var sumBill = bill.Count();
            var sumBill1 = bill1.Count();
            var sumBill2 = bill2.Count();

            var model = new StatisticalViewModel
            {
                TotalAmount = totalAmount,
                CompletedBillCount = sumBill,
                PendingBillCount = sumBill1,
                ShippingBillCount = sumBill2
            };
            return View(model);
        }

        public IActionResult T2()
        {
            var bill = _dbContext.Bill.Where(b => b.Status == Data.Enum.StatusOrder.Completed).ToList();
            var sumBill = bill.Count();
            return View(sumBill);
        }

        public IActionResult GetChartData()
        {
            var completedBills = _dbContext.Bill
                .Where(b => b.Status == Data.Enum.StatusOrder.Completed && b.BillDate.Year == 2024 && b.BillDate.Month <= 4)
                .Count();

            var shippingBills = _dbContext.Bill
                .Where(b => b.Status == Data.Enum.StatusOrder.Shipping && b.BillDate.Year == 2024 && b.BillDate.Month <= 4)
                .Count();

            var canceledBills = _dbContext.Bill
                .Where(b => b.Status == Data.Enum.StatusOrder.Cancelled && b.BillDate.Year == 2024 && b.BillDate.Month <= 4)
                .Count();

            var monthlyData = new List<int>();
            for (int month = 1; month <= 4; month++)
            {
                int count = _dbContext.Bill.Count(b => b.BillDate.Year == 2024 && b.BillDate.Month == month);
                monthlyData.Add(count);
            }

            return Json(new
            {
                CompletedBills = completedBills,
                ShippingBills = shippingBills,
                CanceledBills = canceledBills,
                MonthlyData = monthlyData
            });
        }
    }
}
