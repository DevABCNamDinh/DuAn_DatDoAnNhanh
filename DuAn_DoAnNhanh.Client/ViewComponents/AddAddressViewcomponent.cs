using DuAn_DoAnNhanh.Data.Entities;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;

namespace DuAn_DoAnNhanh.Client.ViewComponents
{
    public class AddAddressViewcomponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            var address = new Address();
            return View(address);
        }
    }
}
