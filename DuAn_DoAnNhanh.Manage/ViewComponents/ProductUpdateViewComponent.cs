using Microsoft.AspNetCore.Mvc;
using DuAn_DoAnNhanh.Data.Entities;
using Microsoft.AspNetCore.Mvc;
using DuAn_DoAnNhanh.Application.Interfaces.Service;
using DuAn_DoAnNhanh.Data.EF;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.Blazor;
using DuAn_DoAnNhanh.Application.Implements.Service;

namespace DuAn_DoAnNhanh.Manage.ViewComponents
{
    public class ProductUpdateViewComponent : ViewComponent
    {
        private readonly IProductService _productService;

        public ProductUpdateViewComponent(IProductService productService)
        {
            _productService = productService;
        }

        public async Task<IViewComponentResult> InvokeAsync(Guid idProduct)
        {
            //var idPro = Guid.Parse(idProduct);
            var obj = _productService.GetProductById(idProduct);
            return View(obj);
        }



        //public IViewComponentResult Invoke(string imageUrl = "")
        //{
        //    // Truyền đường dẫn hình ảnh (nếu có) tới view
        //    return View((object)imageUrl);
        //}


    }
}
