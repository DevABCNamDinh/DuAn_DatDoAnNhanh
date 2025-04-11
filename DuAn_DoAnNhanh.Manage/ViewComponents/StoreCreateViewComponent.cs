using DuAn_DoAnNhanh.Data.ViewModel;
using Microsoft.AspNetCore.Mvc;

public class StoreCreateViewComponent : ViewComponent
{
    public async Task<IViewComponentResult> InvokeAsync(StoreViewModel model)
    {
        return View(model);
    }
}
