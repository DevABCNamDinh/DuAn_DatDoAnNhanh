
using DuAn_DoAnNhanh.Application.Implements.Service;
using DuAn_DoAnNhanh.Application.Interfaces.Service;
using DuAn_DoAnNhanh.Data.EF;
using DuAn_DoAnNhanh.Data.Entities;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Mvc;

namespace DuAn_DoAnNhanh.Manage.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService _productService;
        private readonly MyDBContext _myDbContext;
        public ProductController(IProductService productService, MyDBContext myDbContext)
        {
            _productService = productService;
            _myDbContext = myDbContext;
        }
        public IActionResult GetAll(string search)
        {
            var product = _productService.GetAllProduct().OrderByDescending(p => p.CreteDate).ToList();
            //return View(product);
            //var product = _myDbContext.Products.AsQueryable();
            if (!string.IsNullOrEmpty(search))
            {
                product = product.Where(p => p.ProductName.ToLower().Contains(search.ToLower())).ToList();
            }
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
                var imageUrl = await SaveImageAsync2(ImageFile); // Gọi phương thức lưu ảnh
                if (!string.IsNullOrEmpty(imageUrl))
                {
                    product.ImageUrl = imageUrl; // Gán đường dẫn ảnh vào combo
                }
            }

            // Tạo product mới và lưu vào cơ sở dữ liệu
            Product productCreate = new Product
            {
                ProductName = product.ProductName,
                Description = product.Description,
                Price = product.Price,
                Quantity = 0,
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

        


        public async Task<IActionResult> Edit(Product product, IFormFile ImageFile)
        {


            if (ImageFile != null && ImageFile.Length > 0)
            {
                var imageUrl = await SaveImageAsync2(ImageFile); // Gọi phương thức lưu ảnh
                if (!string.IsNullOrEmpty(imageUrl))
                {
                    product.ImageUrl = imageUrl; // Gán đường dẫn ảnh vào combo
                }
            }
            else
            {
                // Nếu không có ảnh mới, giữ nguyên giá trị ảnh cũ
                var comboEditX = _productService.GetProductById(product.ProductID);
                product.ImageUrl = comboEditX.ImageUrl;  // Giữ lại ảnh cũ nếu không có ảnh mới
            }

            var productUpdate = _productService.GetProductById(product.ProductID);

            productUpdate.ProductName = product.ProductName;
            productUpdate.Price = product.Price;
            productUpdate.Description = product.Description;
            productUpdate.ImageUrl = product.ImageUrl;



            _productService.UpdateProduct(productUpdate);





            return RedirectToAction("GetAll");

        }


        public IActionResult Delete(Guid id)
        {

            _productService.DeleteProduct(id);

            return RedirectToAction("GetAll");

        }

        private async Task<string> SaveImageAsync2(IFormFile imageFile)
        {
            if (imageFile != null && imageFile.Length > 0)
            {
                // Đường dẫn tới thư mục Images trong DuAn_DoAnNhanh.Data
                var dataProjectPath = Path.Combine(Directory.GetCurrentDirectory(), "..", "DuAn_DoAnNhanh.Application", "Images");

                // Kiểm tra nếu thư mục Images không tồn tại thì tạo mới
                if (!Directory.Exists(dataProjectPath))
                {
                    Directory.CreateDirectory(dataProjectPath);
                }

                // Lấy tên file
                var fileName = Path.GetFileName(imageFile.FileName);
                var filePath = Path.Combine(dataProjectPath, fileName);

                // Lưu ảnh vào thư mục Images
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await imageFile.CopyToAsync(fileStream);
                }

                // Trả về đường dẫn tương đối đến ảnh (tùy thuộc vào cách bạn muốn sử dụng đường dẫn này)
                return $"/images/{fileName}";
            }
            return null;
        }

    }
}
