
using DuAn_DoAnNhanh.Application.Implements.Service;
using DuAn_DoAnNhanh.Application.Interfaces.Service;
using DuAn_DoAnNhanh.Data.EF;
using DuAn_DoAnNhanh.Data.Entities;
using DuAn_DoAnNhanh.Data.Enum;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Mvc;

namespace DuAn_DoAnNhanh.Manage.Controllers
{

    [Authorize(Roles = nameof(Role.Admin))]
    public class ProductController : Controller
    {
        private readonly IProductService _productService;
        public ProductController(IProductService productService)
        {
            _productService = productService;
        }
        
        public IActionResult GetAll()
        {
            var product = _productService.GetAllProduct().OrderByDescending(p => p.CreteDate).ToList();
            return View(product);

        }

        [HttpGet]
        public IActionResult Create()
        {
            return ViewComponent("ProductCreate");
        }

        [HttpPost]
        public IActionResult Create(IFormFile ImageFile, Product product)
        {

            try
            {
                _productService.AddProduct(product, ImageFile);
                return RedirectToAction("GetAll", "Product");
            }
            catch (Exception ex)
            {
                TempData["Message"] = "Lỗi" + ex.Message;
                throw;
            }          
        }

        public IActionResult Details(Guid id)
        {
            var book = _productService.GetProductById(id);
            if (book == null)
            {
                return NotFound();
            }
            return View(book);
        }

        


        public IActionResult Edit(Product product, IFormFile ImageFile)
        {
            try
            {
                _productService.UpdateProduct(product, ImageFile);
                return RedirectToAction("GetAll");
            }
            catch (Exception ex)
            {
                TempData["Message"] = "Lỗi" + ex.Message;
                throw;
            }      
        }


        public IActionResult Delete(Guid id)
        {
            try
            {
                _productService.DeleteProduct(id);
                return RedirectToAction("GetAll");
            }
            catch (Exception ex)
            {
                TempData["Message"] = "Lỗi" + ex.Message;
                throw;
            }
        }

       

    }
}
