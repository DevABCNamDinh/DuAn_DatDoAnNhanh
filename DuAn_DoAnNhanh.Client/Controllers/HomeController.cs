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
            return View();
        }

   
    }
}
