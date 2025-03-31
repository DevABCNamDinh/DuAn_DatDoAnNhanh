using DuAn_DoAnNhanh.Data.Interfaces.Repositories;
using DuAn_DoAnNhanh.Application.Interfaces.Service;
using DuAn_DoAnNhanh.Data.Entities;
using DuAn_DoAnNhanh.Data.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DuAn_DoAnNhanh.Data.Interface.UnitOfWork;
using Microsoft.AspNetCore.Http;

namespace DuAn_DoAnNhanh.Application.Implements.Service
{
    public class ProductService : IProductService
    {
        private readonly IUnitOfWork _unitOfWork;
        
        public ProductService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void AddProduct(Product product, IFormFile ImageFile)
        {
            if (product!=null)
            {
                if (ImageFile != null && ImageFile.Length > 0)
                {
                    var imageUrl = SaveImage(ImageFile); // Gọi phương thức lưu ảnh
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
                _unitOfWork.ProductRepo.Add(productCreate);
                _unitOfWork.ProductRepo.SaveChanges();
            }
           
        }

        public void DeleteProduct(Guid id)
        {
            var obj = _unitOfWork.ProductRepo.GetById(id);
            if (obj!=null)
            {
                obj.Status = StatusProduct.InActivity;
                _unitOfWork.ProductRepo.Update(obj);
                _unitOfWork.ProductRepo.SaveChanges();
            }
           
        }

        public List<Product> GetAllProduct()
        {
            return _unitOfWork.ProductRepo.GetAll().Where(x=>x.Status==StatusProduct.Activity).ToList();
        }

        public Product GetProductById(Guid id)
        {
            return _unitOfWork.ProductRepo.GetById(id);
        }

        public void UpdateProduct(Product product, IFormFile ImageFile)
        {
            if (ImageFile != null && ImageFile.Length > 0)
            {
                var imageUrl =  SaveImage(ImageFile); // Gọi phương thức lưu ảnh
                if (!string.IsNullOrEmpty(imageUrl))
                {
                    product.ImageUrl = imageUrl; // Gán đường dẫn ảnh vào combo
                }
            }
            else
            {
                // Nếu không có ảnh mới, giữ nguyên giá trị ảnh cũ
                var comboEditX = GetProductById(product.ProductID);
                product.ImageUrl = comboEditX.ImageUrl;  // Giữ lại ảnh cũ nếu không có ảnh mới
            }

            var productUpdate = GetProductById(product.ProductID);

            productUpdate.ProductName = product.ProductName;
            productUpdate.Price = product.Price;
            productUpdate.Description = product.Description;
            productUpdate.ImageUrl = product.ImageUrl;
            productUpdate.Status = product.Status;
            _unitOfWork.ProductRepo.Update(productUpdate);
            _unitOfWork.ProductRepo.SaveChanges();
        }

        private string SaveImage(IFormFile imageFile)
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
                    imageFile.CopyTo(fileStream);
                }

                // Trả về đường dẫn tương đối đến ảnh (tùy thuộc vào cách bạn muốn sử dụng đường dẫn này)
                return $"/images/{fileName}";
            }
            return null;
        }
    }
}
