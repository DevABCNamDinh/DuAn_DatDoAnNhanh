using DuAn_DoAnNhanh.Data.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace DuAn_DoAnNhanh.Client.ViewComponents
{
    public class SignUpSignInViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke(LoginRegisterViewModel model)
        {          
            return View(model); // Truyền đối tượng  vào view
        }
    }
}
