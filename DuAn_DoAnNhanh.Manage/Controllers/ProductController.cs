using DuAn_DoAnNhanh.Application.Implements.Service;
using DuAn_DoAnNhanh.Application.Interfaces.Service;
using DuAn_DoAnNhanh.Data.Entities;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Mvc;

namespace DuAn_DoAnNhanh.Manage.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService _productService;
        public ProductController(IProductService productService)
        {
            _productService = productService;
        }
        public IActionResult GetAll()
        {
            var product = _productService.GetAllProduct().OrderByDescending(p => p.CreteDate);
            return View(product);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return ViewComponent("ProductCreate");
        }

        [HttpPost]
        public async Task<IActionResult> Create(IFormFile ImageFile, Product product)
        {
            if (ImageFile != null && ImageFile.Length > 0)
            {
                // Đường dẫn tới thư mục lưu ảnh
                var uploads = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/product");
                if (!Directory.Exists(uploads))
                {
                    Directory.CreateDirectory(uploads);
                }

                var fileName = Path.GetFileName(ImageFile.FileName);
                var filePath = Path.Combine(uploads, fileName);

                // Lưu file vào thư mục
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await ImageFile.CopyToAsync(fileStream);
                }

                // Tạo URL cho hình ảnh
                product.ImageUrl = Url.Content($"~/images/product/{fileName}");
            }

            // Tạo product mới và lưu vào cơ sở dữ liệu
            Product productCreate = new Product
            {
                ProductName = product.ProductName,
                Description = product.Description,
                Price = product.Price,
                Quantity = product.Quantity,
                Status = product.Status,
                ImageUrl = product.ImageUrl, // Địa chỉ ảnh đã tạo
                CreteDate = DateTime.Now
            };

            _productService.AddProduct(productCreate);

            return RedirectToAction("GetAll", "Product");
        }


    }
}
