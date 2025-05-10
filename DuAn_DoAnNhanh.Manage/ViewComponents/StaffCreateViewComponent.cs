using DuAn_DoAnNhanh.Data.ViewModel;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

public class StaffCreateViewComponent : ViewComponent
{
    public async Task<IViewComponentResult> InvokeAsync(StaffViewModel model)
    {
        return View(model);
    }
}
