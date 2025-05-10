using DuAn_DoAnNhanh.Application.Interfaces.Service;
using DuAn_DoAnNhanh.Data.EF;
using DuAn_DoAnNhanh.Data.Entities;
using DuAn_DoAnNhanh.Data.Enum;
using DuAn_DoAnNhanh.Data.ViewModel;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;

namespace DuAn_DoAnNhanh.Manage.Controllers
{
    public class ComboController : Controller
    {
        private readonly MyDBContext _dbContext;
        private readonly IComboDetailsService _comboDetailsService;
        private readonly IComboService _comboService;
        private readonly IProductService _productService;


        public ComboController(IComboDetailsService comboDetailsService,IComboService comboService, MyDBContext dbContext, IProductService productService)
        {
            _comboService = comboService;
            _dbContext = dbContext;
            _productService= productService;
            _comboDetailsService = comboDetailsService;
        }
        //public IActionResult GetAll()
        //{
        //    // Lấy danh sách combo
        //    var comboList = _comboService.GetAllCombo();

        //    // Tạo danh sách ViewModel để chứa combo và sản phẩm của mỗi combo
        //    var comboWithProductsList = comboList.Select(combo => new ComboWithProductsViewModel
        //    {

        //        Combo = combo,
        //        Products = _comboDetailsService.listProductInCombo(combo.ComboID).ToList() // Giả sử bạn có dịch vụ _productService để lấy sản phẩm theo ComboID
        //    }).ToList();

        //    return View(comboWithProductsList);
        //}


        public IActionResult GetAll(string searchTerm = null, string status = null, int page = 1)
        {
            // Lấy danh sách combo
            var comboList = _comboService.GetAllCombo();
            int pageSize = 6;

            // Lọc theo trạng thái
            if (!string.IsNullOrEmpty(status))
            {
                if (Enum.TryParse<StatusCombo>(status, out var statusEnum))
                {
                    comboList = comboList.Where(combo => combo.Status == statusEnum).ToList();
                }
            }

            // Nếu có từ khóa tìm kiếm, lọc danh sách combo
            if (!string.IsNullOrEmpty(searchTerm))
            {
                comboList = comboList.Where(combo => combo.ComboName.Contains(searchTerm, StringComparison.OrdinalIgnoreCase)).ToList();
            }

            // Phân trang
            int totalCombos = comboList.Count;
            int totalPages = (int)Math.Ceiling((double)totalCombos / pageSize);
            var paginatedCombos = comboList.Skip((page - 1) * pageSize).Take(pageSize).ToList();

            // Tạo danh sách ViewModel để chứa combo và sản phẩm của mỗi combo
            var comboWithProductsList = paginatedCombos.Select(combo => new ComboWithProductsViewModel
            {
                Combo = combo,
                Products = _comboDetailsService.listProductInCombo(combo.ComboID).ToList()
            }).ToList();

            // Truyền các tham số cho view
            ViewBag.SearchTerm = searchTerm;
            ViewBag.Status = status;
            ViewBag.CurrentPage = page;
            ViewBag.TotalPages = totalPages;

            return View(comboWithProductsList);
        }
        // Hành động POST để tạo combo mới
        [HttpPost]
        public async Task<IActionResult> Create(ComboCreateViewModel model, IFormFile ImageFile)
        {
            // Kiểm tra nếu không chọn ít nhất 2 sản phẩm
            //if (model.SelectedProducts == null || model.SelectedProducts.Count < 2)
            //{
            //    ModelState.AddModelError("", "Vui lòng chọn ít nhất 2 sản phẩm để tạo combo.");
            //    model.AvailableProducts = _productService.GetAllProduct();
            //    return View("GetAll", model);
            //}
            model.ComboName = model.ComboName?.Trim(); // Cắt bỏ khoảng trắng thừa
            if (string.IsNullOrWhiteSpace(model.ComboName))
            {
                ModelState.AddModelError("ComboName", "Tên combo không thể chỉ chứa khoảng trắng.");
            }
            // Lọc các sản phẩm đã được chọn(dựa trên IsSelected)
            var selectedProducts = model.SelectedProducts
                .Where(sp => sp.IsSelected && sp.Quantity > 0)
                .ToList();

            //// refill danh sách để component hiển thị
            //model.AvailableProducts = _productService.GetAllProduct();

            // Kiểm tra số lượng sản phẩm đã chọn
            if (selectedProducts.Count < 2)
            {
                ModelState.AddModelError("SelectedProducts", "Vui lòng chọn ít nhất 2 sản phẩm để tạo combo.");
            }
            // Kiểm tra file ảnh
            if (ImageFile == null || ImageFile.Length == 0)
            {
                ModelState.AddModelError("ImageFile", "Vui lòng chọn hình ảnh cho combo.");
            }// Nếu có lỗi, trả lại view với dữ liệu cần thiết
            if (!ModelState.IsValid)
            {
                model.AvailableProducts = _productService.GetAllProduct();
                return ViewComponent("ComboCreate", model);
            }
            //if (!ModelState.IsValid)
            //{
            //    return ViewComponent("ComboCreate");
            //}

            // Xử lý upload ảnh
            string imageUrl = null;
            if (ImageFile != null && ImageFile.Length > 0)
            {
                imageUrl = await SaveImageAsync2(ImageFile);
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
            var productCombos = selectedProducts.Select(sp => new ProductCombo
            {
                ProductID = sp.ProductID,
                Quantity = sp.Quantity,
                Status = StatusCombo.Activity
            }).ToList();

            // Gọi service để tạo combo
            await _comboService.CreateComboAsync(combo, productCombos);

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

        public IActionResult Details(Guid id)
        {
            var comboDetail = new ComboWithProductsViewModel();
            var listProductCombo = _dbContext.productCombos.Where(x=>x.ComboID==id&&x.Status==StatusCombo.Activity).ToList();

           var combo = _comboService.GetComboById(id);
            var products = new List<Product>();
            foreach (var product in listProductCombo)
            {
                var pr = _productService.GetProductById(product.ProductID);
                pr.Quantity = product.Quantity;
                products.Add(pr);
            }
            comboDetail.Combo = combo;
            comboDetail.Products = products;
            return View(comboDetail);
        }
        public async Task<IActionResult> Edit(IFormFile ImageFile, Combo combo)
        {
            if (ImageFile != null && ImageFile.Length > 0)
            {
                var imageUrl = await SaveImageAsync2(ImageFile); // Gọi phương thức lưu ảnh
                if (!string.IsNullOrEmpty(imageUrl))
                {
                    combo.Image = imageUrl; // Gán đường dẫn ảnh vào combo
                }
            }
            else
            {
                // Nếu không có ảnh mới, giữ nguyên giá trị ảnh cũ
                var comboEditX = _comboService.GetComboById(combo.ComboID);
                combo.Image = comboEditX.Image;  // Giữ lại ảnh cũ nếu không có ảnh mới
            }

            var comboEdit = _comboService.GetComboById(combo.ComboID);    
            comboEdit.Image = combo.Image;   
            comboEdit.ComboName= combo.ComboName;
            comboEdit.SetupPrice = combo.SetupPrice;
            _comboService.UpdateCombo(comboEdit);
            return RedirectToAction("Details", new {id =combo.ComboID});
            //return ViewComponent("ComboDetails", new { comboID =combo.ComboID});

        }

        public IActionResult Delete(Guid id)
        {
            _comboService.DeleteCombo(id);
            return RedirectToAction("GetAll");
        }

        public IActionResult Deactivate(Guid id, string searchTerm, string status, int page)
        {
            var combo = _comboService.GetComboById(id);
            if (combo != null)
            {
                combo.Status = StatusCombo.InActivity;
                _comboService.UpdateCombo(combo);
            }
            return RedirectToAction("GetAll", new { searchTerm, status, page });
        }

        public IActionResult Activate(Guid id, string searchTerm, string status, int page)
        {
            var combo = _comboService.GetComboById(id);
            if (combo != null && combo.Status == StatusCombo.InActivity)
            {
                combo.Status = StatusCombo.Activity;
                _comboService.UpdateCombo(combo);
            }
            return RedirectToAction("GetAll", new { searchTerm, status, page });
        }

        //public IActionResult DeactivatedCombos(int page = 1)
        //{
        //    int pageSize = 6; // Số combo trên mỗi trang
        //    var deactivatedCombos = _comboService.GetDeactivatedCombos();
        //    int totalCombos = deactivatedCombos.Count;
        //    int totalPages = (int)Math.Ceiling((double)totalCombos / pageSize);
        //    var paginatedCombos = deactivatedCombos.Skip((page - 1) * pageSize).Take(pageSize).ToList();

        //    var viewModel = paginatedCombos.Select(combo => new ComboWithProductsViewModel
        //    {
        //        Combo = combo,
        //        Products = _comboDetailsService.listProductInCombo(combo.ComboID).ToList()
        //    }).ToList();

        //    ViewBag.CurrentPage = page;
        //    ViewBag.TotalPages = totalPages;

        //    return View("DeactivatedCombos", viewModel);
        //}

        //public IActionResult EditDeactivatedCombo(Guid id)
        //{
        //    var combo = _comboService.GetComboById(id);
        //    if (combo == null || combo.Status != StatusCombo.InActivity)
        //    {
        //        return NotFound();
        //    }
        //    var products = _comboDetailsService.listProductInCombo(id);
        //    var viewModel = new ComboWithProductsViewModel
        //    {
        //        Combo = combo,
        //        Products = products
        //    };
        //    return View("_DeactivatedComboDetails", viewModel);

        //}
    }
}
