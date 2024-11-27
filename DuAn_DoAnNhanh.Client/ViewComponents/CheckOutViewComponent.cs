using DuAn_DoAnNhanh.Client.Models.ViewModel;
using DuAn_DoAnNhanh.Data.Entities;
using Microsoft.AspNetCore.Mvc;

namespace DuAn_DoAnNhanh.Client.ViewComponents
{
    public class CheckOutViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
           
            return View(); 
        }
    }
}
