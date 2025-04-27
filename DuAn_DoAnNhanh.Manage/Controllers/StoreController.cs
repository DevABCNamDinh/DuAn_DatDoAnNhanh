using DuAn_DoAnNhanh.Application.Interfaces.Service;
using DuAn_DoAnNhanh.Data.EF;
using DuAn_DoAnNhanh.Data.Enum;
using DuAn_DoAnNhanh.Data.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace DuAn_DoAnNhanh.Manage.Controllers
{
    public class StoreController : Controller
    {
        private readonly MyDBContext _dbContext;
        private readonly IStoreService _storeService;

        public StoreController(MyDBContext dbContext, IStoreService storeService)
        {
            _dbContext = dbContext;
            _storeService = storeService;
        }

        public IActionResult Index()
        {
            var storeList = _dbContext.Stores
                .Include(x => x.Address)
                .Include(x => x.Users)
                .Where(x => x.Status == Status.Activity)
                .OrderByDescending(x => x.CreateDate)
                .ToList();

            return View(storeList);
        }

        [HttpPost]
        public async Task<IActionResult> CreateStore(StoreViewModel storeViewModel)
        {
            if (!ModelState.IsValid)
            {
                return ViewComponent("StoreCreate", new { model = storeViewModel });
            }

            try
            {
                await _storeService.CreateStoreAsync(storeViewModel);
                return Json(new { success = true });
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, "Đã xảy ra lỗi. Vui lòng thử lại.");
                return ViewComponent("StoreCreate", new { model = storeViewModel });
            }
        }

    }
}
