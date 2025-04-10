using Microsoft.AspNetCore.Mvc;

namespace DuAn_DoAnNhanh.Manage.Areas.Store.Controllers
{
    public class HomeController : Controller
    {
        [Area("ManageStore")]
        public IActionResult Index()
        {
            return View();
        }
    }
}
