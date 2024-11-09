using Microsoft.AspNetCore.Mvc;

namespace DuAn_DoAnNhanh.Client.Controllers
{
    public class ProductController : Controller
    {

        public IActionResult Index()
        {
            return View();
        }
    }
}
