using DuAn_DoAnNhanh.Data.ViewModel;
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
