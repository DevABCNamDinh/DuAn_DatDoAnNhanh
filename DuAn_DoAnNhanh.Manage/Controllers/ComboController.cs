using DuAn_DoAnNhanh.Application.Interfaces.Service;
using DuAn_DoAnNhanh.Data.EF;
using DuAn_DoAnNhanh.Data.Entities;
using DuAn_DoAnNhanh.Data.Enum;
using DuAn_DoAnNhanh.Data.ViewModel;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace DuAn_DoAnNhanh.Manage.Controllers
{
    public class ComboController : Controller
    {
        private readonly MyDBContext _dbContext;
        private readonly IComboDetailsService _comboDetailsService;
        private readonly IComboService _comboService;
        private readonly IProductService _productService;

        public ComboController(IComboDetailsService comboDetailsService, IComboService comboService, MyDBContext dbContext, IProductService productService)
        {
            _comboService = comboService;
            _dbContext = dbContext;
            _productService = productService;
            _comboDetailsService = comboDetailsService;
        }

        // Hành động GET để lấy danh sách combo với phân trang
        public IActionResult GetAll(int page = 1, string searchTerm = null)
        {
            const int pageSize = 6; // Số combo trên mỗi trang

            // Lấy danh sách combo từ service
            var comboList = _comboService.GetAllCombo();

            // Nếu có từ khóa tìm kiếm, lọc danh sách combo
            if (!string.IsNullOrEmpty(searchTerm))
            {
                comboList = comboList.Where(combo => combo.ComboName.Contains(searchTerm, StringComparison.OrdinalIgnoreCase)).ToList();
            }

            // Tính toán phân trang
            int totalCombos = comboList.Count;
            int totalPages = (int)Math.Ceiling((double)totalCombos / pageSize);
            var paginatedCombos = comboList.Skip((page - 1) * pageSize).Take(pageSize).ToList();

            // Tạo danh sách ViewModel chứa combo và sản phẩm
            var comboWithProductsList = paginatedCombos.Select(combo => new ComboWithProductsViewModel
            {
                Combo = combo,
                Products = _comboDetailsService.listProductInCombo(combo.ComboID).ToList()
            }).ToList();

            // Truyền dữ liệu phân trang vào ViewBag
            ViewBag.CurrentPage = page;
            ViewBag.TotalPages = totalPages;
            ViewBag.SearchTerm = searchTerm;

            return View(comboWithProductsList);
        }

        // Hành động GET cho trang tạo combo
        [HttpGet]
        public IActionResult Create()
        {
            var model = new ComboCreateViewModel
            {
                AvailableProducts = _productService.GetAllProduct()
            };
            return View("~/Views/Shared/Components/ComboCreate/Default.cshtml", model);
        }

        // Hành động POST để tạo combo mới
        [HttpPost]
        public async Task<IActionResult> Create(ComboCreateViewModel model, IFormFile ImageFile)
        {
            // Kiểm tra nếu không chọn ít nhất 2 sản phẩm
            if (model.SelectedProducts == null || model.SelectedProducts.Count < 2)
            {
                ModelState.AddModelError("", "Vui lòng chọn ít nhất 2 sản phẩm để tạo combo.");
                model.AvailableProducts = _productService.GetAllProduct();
               return View("~/Views/Shared/Components/ComboCreate/Default.cshtml", model);
            }

            // Xử lý upload ảnh
            string imageUrl = null;
            if (ImageFile != null && ImageFile.Length > 0)
            {
                imageUrl = await SaveImageAsync(ImageFile);
            }

            // Tạo đối tượng Combo
            var combo = new Combo
            {
                ComboName = model.ComboName,
                Description = model.Description,
                Image = imageUrl,
                Price = 0, // Giá sẽ được tính tự động trong service
                Status = StatusCombo.Activity,
                CreteDate = DateTime.Now
            };

            // Tạo danh sách ProductCombo
            var productCombos = model.SelectedProducts.Select(sp => new ProductCombo
            {
                ProductID = sp.ProductID,
                Quantity = sp.Quantity,
                Status = StatusCombo.Activity
            }).ToList();

            // Gọi service để tạo combo
            await _comboService.CreateComboAsync(combo, productCombos);

            return RedirectToAction("GetAll", "Combo");
        }

        // Phương thức lưu ảnh
        private async Task<string> SaveImageAsync(IFormFile imageFile)
        {
            if (imageFile == null || imageFile.Length == 0) return null;

            var uploadPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images");
            if (!Directory.Exists(uploadPath))
            {
                Directory.CreateDirectory(uploadPath);
            }

            var fileName = Guid.NewGuid().ToString() + Path.GetExtension(imageFile.FileName);
            var filePath = Path.Combine(uploadPath, fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await imageFile.CopyToAsync(stream);
            }

            return $"/images/{fileName}";
        }

        // Hành động xem chi tiết combo
        public IActionResult Details(Guid id)
        {
            var combo = _comboService.GetComboById(id);
            if (combo == null) return NotFound();

            var products = _comboDetailsService.listProductInCombo(id).ToList();
            var comboDetail = new ComboWithProductsViewModel
            {
                Combo = combo,
                Products = products
            };

            return View(comboDetail);
        }

        // Hành động chỉnh sửa combo
        [HttpPost]
        public async Task<IActionResult> Edit(IFormFile ImageFile, Combo combo)
        {
            var existingCombo = _comboService.GetComboById(combo.ComboID);
            if (existingCombo == null) return NotFound();

            if (ImageFile != null && ImageFile.Length > 0)
            {
                var imageUrl = await SaveImageAsync(ImageFile);
                if (!string.IsNullOrEmpty(imageUrl))
                {
                    combo.Image = imageUrl;
                }
            }
            else
            {
                combo.Image = existingCombo.Image; // Giữ ảnh cũ nếu không có ảnh mới
            }

            existingCombo.ComboName = combo.ComboName;
            existingCombo.Description = combo.Description;
            existingCombo.Image = combo.Image;
            existingCombo.SetupPrice = combo.SetupPrice;

            _comboService.UpdateCombo(existingCombo);
            return RedirectToAction("Details", new { id = combo.ComboID });
        }

        // Hành động xóa combo
        public IActionResult Delete(Guid id)
        {
            _comboService.DeleteCombo(id);
            return RedirectToAction("GetAll");
        }
    }
}