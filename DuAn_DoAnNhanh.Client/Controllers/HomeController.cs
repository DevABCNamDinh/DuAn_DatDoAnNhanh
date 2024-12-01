using DuAn_DoAnNhanh.Client.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace DuAn_DoAnNhanh.Client.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            var slides = new List<SlideModel>
            {
                new SlideModel { imgurl = "https://inan2h.vn/wp-content/uploads/2022/12/in-banner-quang-cao-do-an-10-1.jpg" },
                new SlideModel { imgurl = "https://inan2h.vn/wp-content/uploads/2022/12/in-banner-quang-cao-do-an-11-1.jpg" },
                new SlideModel { imgurl = "https://inan2h.vn/wp-content/uploads/2022/12/in-banner-quang-cao-do-an-7-1.jpg" }
            };

            return View(slides);
        }

        public IActionResult OrderHome()
        {
            return View();
        }
        public class SlideModel
        {
            public string imgurl { get; set; }
        }


    }
}
