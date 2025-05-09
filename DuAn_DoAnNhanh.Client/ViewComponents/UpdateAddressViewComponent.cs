using DuAn_DoAnNhanh.Data.EF;
using Microsoft.AspNetCore.Mvc;

namespace DuAn_DoAnNhanh.Client.ViewComponents
{
    public class UpdateAddressViewComponent : ViewComponent
    {

        private readonly MyDBContext _dbContext;
        public UpdateAddressViewComponent(MyDBContext dbContext)
        {
            _dbContext = dbContext;
        }


        public IViewComponentResult Invoke(Guid id)
        {
            var address = _dbContext.addresses.FirstOrDefault(a => a.AddressID == id);
            if (address == null)
            {
                return View("NotFound");
            }

            return View(address);

        }
    }
}

