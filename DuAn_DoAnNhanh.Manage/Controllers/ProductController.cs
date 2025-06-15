
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
            var product = _productService.GetAllProductIncludeInactive()
                .OrderByDescending(p => p.CreteDate)
                .ToList();
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

        [HttpPost]
        //public IActionResult ChangeStatus(Guid id)
        //{
        //    if (_productService.ChangeStatus(id))
        //    {
        //        return Ok();
        //    }

        //    return NotFound();
        //}

        [HttpPost]
        public IActionResult ChangeStatus(Guid id)
        {
            try
            {
                if (_productService.ChangeStatus(id))
                {
                    return Ok(new { success = true });
                }
                return NotFound();
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { success = false, message = ex.Message });
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
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
