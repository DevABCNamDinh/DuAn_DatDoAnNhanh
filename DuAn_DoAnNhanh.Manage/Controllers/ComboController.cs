using DuAn_DoAnNhanh.Application.Interfaces.Service;
using DuAn_DoAnNhanh.Data.EF;
using DuAn_DoAnNhanh.Data.Entities;
using DuAn_DoAnNhanh.Data.Enum;
using DuAn_DoAnNhanh.Data.ViewModel;
using Microsoft.AspNetCore.Mvc;

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


        public IActionResult GetAll(string searchTerm = null)
        {
            // Lấy danh sách combo
            var comboList = _comboService.GetAllCombo();

            // Nếu có từ khóa tìm kiếm, lọc danh sách combo
            if (!string.IsNullOrEmpty(searchTerm))
            {
                comboList = comboList.Where(combo => combo.ComboName.Contains(searchTerm, StringComparison.OrdinalIgnoreCase)).ToList();
            }

            // Tạo danh sách ViewModel để chứa combo và sản phẩm của mỗi combo
            var comboWithProductsList = comboList.Select(combo => new ComboWithProductsViewModel
            {
                Combo = combo,
                Products = _comboDetailsService.listProductInCombo(combo.ComboID).ToList()
            }).ToList();

            return View(comboWithProductsList);
        }

        public IActionResult Create() { 
            return ViewComponent("ComboCreate");
        }
        [HttpPost]
        public async Task<IActionResult> Create(IFormFile ImageFile, Combo combo)
        {
            if (ImageFile != null && ImageFile.Length > 0)
            {
                var imageUrl = await SaveImageAsync2(ImageFile); // Gọi phương thức lưu ảnh
                if (!string.IsNullOrEmpty(imageUrl))
                {
                    combo.Image = imageUrl; // Gán đường dẫn ảnh vào combo
                }
            }
            // Tạo product mới và lưu vào cơ sở dữ liệu
            Combo comboCreate = new Combo
            {
                ComboName = combo.ComboName,
                Description = combo.Description,
                Price = combo.Price,                          
                Image = combo.Image, // Địa chỉ ảnh đã tạo
            };

            _comboService.AddCombo(comboCreate);

            return RedirectToAction("GetAll", "Combo");
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
            _comboService.UpdateCombo(comboEdit);
            return RedirectToAction("Details", new {id =combo.ComboID});
            //return ViewComponent("ComboDetails", new { comboID =combo.ComboID});

        }

        public IActionResult Delete(Guid id)
        {
            _comboService.DeleteCombo(id);
            return RedirectToAction("GetAll");
        }
       
    }
}
