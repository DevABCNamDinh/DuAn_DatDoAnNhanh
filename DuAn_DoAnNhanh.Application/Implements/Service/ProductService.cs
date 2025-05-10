using DuAn_DoAnNhanh.Data.Interfaces.Repositories;
using DuAn_DoAnNhanh.Application.Interfaces.Service;
using DuAn_DoAnNhanh.Data.Entities;
using DuAn_DoAnNhanh.Data.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Http;
using System.IO;
using DuAn_DoAnNhanh.Data.Interface.UnitOfWork;

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
            if (product != null)
            {
                if (ImageFile != null && ImageFile.Length > 0)
                {
                    var imageUrl = SaveImage(ImageFile);
                    if (!string.IsNullOrEmpty(imageUrl))
                    {
                        product.ImageUrl = imageUrl;
                    }
                }

                Product productCreate = new Product
                {
                    ProductName = product.ProductName,
                    Description = product.Description,
                    Price = product.Price,
                    Quantity = 0,
                    Status = product.Status,
                    ImageUrl = product.ImageUrl,
                    CreteDate = DateTime.Now
                };
                _unitOfWork.ProductRepo.Add(productCreate);
                _unitOfWork.ProductRepo.SaveChanges();
            }
        }

        public void DeleteProduct(Guid id)
        {
            var obj = _unitOfWork.ProductRepo.GetById(id);
            if (obj != null)
            {
                obj.Status = StatusProduct.InActivity;
                _unitOfWork.ProductRepo.Update(obj);
                _unitOfWork.ProductRepo.SaveChanges();
            }
        }

        public List<Product> GetAllProduct()
        {
            return _unitOfWork.ProductRepo.GetAll()
                .Where(x => x.Status == StatusProduct.Activity)
                .ToList();
        }

        public List<Product> GetAllProductIncludeInactive()
        {
            return _unitOfWork.ProductRepo.GetAll().ToList();
        }

        public Product GetProductById(Guid id)
        {
            return _unitOfWork.ProductRepo.GetById(id);
        }

        public void UpdateProduct(Product product, IFormFile ImageFile)
        {
            if (ImageFile != null && ImageFile.Length > 0)
            {
                var imageUrl = SaveImage(ImageFile);
                if (!string.IsNullOrEmpty(imageUrl))
                {
                    product.ImageUrl = imageUrl;
                }
            }
            else
            {
                var existingProduct = GetProductById(product.ProductID);
                product.ImageUrl = existingProduct.ImageUrl;
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

        //public bool ChangeStatus(Guid id)
        //{
        //    var product = _unitOfWork.ProductRepo.GetById(id);
        //    if (product == null) return false;

        //    product.Status = product.Status == StatusProduct.Activity
        //        ? StatusProduct.InActivity
        //        : StatusProduct.Activity;

        //    _unitOfWork.ProductRepo.Update(product);
        //    _unitOfWork.ProductRepo.SaveChanges();
        //    return true;
        //}

        public bool ChangeStatus(Guid id)
        {
            var product = _unitOfWork.ProductRepo.GetById(id);
            if (product == null) return false;

            // Nếu đang từ Đang bán -> Ngừng bán thì phải kiểm tra
            if (product.Status == StatusProduct.Activity)
            {
                var isInCombo = _unitOfWork.ProductComboRepo.GetAll().Any(cd => cd.ProductID == id);
                if (isInCombo)
                {
                    // Không cho phép chuyển nếu sản phẩm đang nằm trong combo
                    throw new InvalidOperationException("Sản phẩm đang nằm trong combo.");
                }
            }

            product.Status = product.Status == StatusProduct.Activity
                ? StatusProduct.InActivity
                : StatusProduct.Activity;

            _unitOfWork.ProductRepo.Update(product);
            _unitOfWork.ProductRepo.SaveChanges();
            return true;
        }


        private string SaveImage(IFormFile imageFile)
        {
            if (imageFile != null && imageFile.Length > 0)
            {
                var dataProjectPath = Path.Combine(Directory.GetCurrentDirectory(), "..", "DuAn_DoAnNhanh.Application", "Images");

                if (!Directory.Exists(dataProjectPath))
                {
                    Directory.CreateDirectory(dataProjectPath);
                }

                var fileName = Path.GetFileName(imageFile.FileName);
                var filePath = Path.Combine(dataProjectPath, fileName);

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    imageFile.CopyTo(fileStream);
                }

                return $"/images/{fileName}";
            }
            return null;
        }
    }
}