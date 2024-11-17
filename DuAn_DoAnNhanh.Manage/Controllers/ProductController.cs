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

        public IActionResult Details(Guid id)
        {
            var book = _productService.GetProductById(id);
            if (book == null)
            {
                return NotFound();
            }
            return View(book);
        }

        public IActionResult Edit(Guid id)
        {
            var product = _productService.GetProductById(id);
            return View(product);
        }

        [HttpPost]
        public IActionResult Edit(Product product, IFormFile imageFile)
        {

            if (imageFile != null && imageFile.Length > 0)
            {
                // Xóa ảnh cũ nếu có
                if (!string.IsNullOrEmpty(product.ImageUrl))
                {
                    var oldImagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", product.ImageUrl.TrimStart('/'));
                    if (System.IO.File.Exists(oldImagePath))
                    {
                        System.IO.File.Delete(oldImagePath);
                    }
                }

                // Lưu ảnh mới
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images", imageFile.FileName);
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    imageFile.CopyTo(stream);
                }
                product.ImageUrl = $"/images/{imageFile.FileName}";
            }

            //Product productUpdate = new Product
            //{
            //    ProductName = product.ProductName,
            //    Description = product.Description,
            //    Price = product.Price,
            //    Quantity = product.Quantity,
            //    Status = product.Status,
            //    ImageUrl = product.ImageUrl, // Địa chỉ ảnh đã tạo
            //    CreteDate = DateTime.Now
            //};

            _productService.UpdateProduct(product);





            return RedirectToAction("GetAll");

        }

        public IActionResult Delete(Guid id)
        {
            var product = _productService.GetProductById(id);
            return View(product);
        }

        [HttpPost]  
        public IActionResult Delete(Product product)
        {

            _productService.DeleteProduct(product.ProductID);

            return RedirectToAction("GetAll");

        }


        //[HttpPost]
        //public IActionResult Edit(Product product, IFormFile imageFile)
        //{
        //    if (imageFile != null && imageFile.Length > 0)
        //    {
        //        var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images", imageFile.FileName);
        //        using (var stream = new FileStream(filePath, FileMode.Create))
        //        {
        //            imageFile.CopyTo(stream);
        //        }
        //        product.ImageUrl = $"/images/{imageFile.FileName}";
        //    }

        //    var existingProduct = _productService.FirstOrDefault(p => p.Id == product.Id).UpdateProduct;
        //    if (existingProduct != null)
        //    {
        //        existingProduct.Name = product.Name;
        //        existingProduct.Price = product.Price;
        //        existingProduct.Description = product.Description;
        //        existingProduct.ImagePath = product.ImagePath;
        //    }

        //    return RedirectToAction("Index");
        //}








        //[HttpPost]
        //public IActionResult UploadImage(IFormFile file)
        //{
        //    if (file != null && file.Length > 0)
        //    {
        //        var filePath = Path.Combine("wwwroot/images", file.FileName);
        //        using (var stream = new FileStream(filePath, FileMode.Create))
        //        {
        //            file.CopyTo(stream);
        //        }

        //        // Trả về đường dẫn ảnh (tương đối)
        //        return Json(new { imageUrl = $"/images/{file.FileName}" });
        //    }

        //    return BadRequest("No file uploaded.");
        //}





    }
}
