using DuAn_DoAnNhanh.Application.Interfaces.Repositories;
using DuAn_DoAnNhanh.Application.Interfaces.Service;
using DuAn_DoAnNhanh.Data.Entities;
using DuAn_DoAnNhanh.Data.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DuAn_DoAnNhanh.Application.Implements.Service
{
    public class ProductService : IProductService
    {
        private readonly IGenericRepository<Product> _ProductRepository;
        public ProductService(IGenericRepository<Product> ProductRepository)
        {
            _ProductRepository = ProductRepository;
        }

        public void AddProduct(Product product)
        {
            _ProductRepository.insert(product);
            _ProductRepository.save();
        }

        public void DeleteProduct(Guid id)
        {
            var obj = _ProductRepository.GetById(id);

            obj.Status = Data.Enum.StatusProduct.InActivity;
            _ProductRepository.update(obj);
            _ProductRepository.save();
        }

        public List<Product> GetAllProduct()
        {
            return _ProductRepository.GetAll().Where(x=>x.Status==StatusProduct.Activity).ToList();
        }

        public Product GetProductById(Guid id)
        {
            return _ProductRepository.GetById(id);
        }

        public void UpdateProduct(Product product)
        {
            var obj = _ProductRepository.GetById(product.ProductID);

            obj.ProductName = product.ProductName;
            obj.ImageUrl = product.ImageUrl;
            obj.Description = product.Description;
            obj.Quantity = product.Quantity;
            obj.Price = product.Price;
            obj.CreteDate = product.CreteDate;
            obj.Status = product.Status;
             _ProductRepository.update(obj);
            _ProductRepository.save();
        }
    }
}
