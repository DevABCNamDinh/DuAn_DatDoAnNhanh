using DuAn_DoAnNhanh.Application.Interfaces.Repositories;
using DuAn_DoAnNhanh.Application.Interfaces.Service;
using DuAn_DoAnNhanh.Data.Entities;

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
            throw new NotImplementedException();
        }

        public List<Product> GetAllProduct()
        {
            return _ProductRepository.GetAll();
        }

        public Product GetProductById(Guid id)
        {
            return _ProductRepository.GetById(id);
        }

        public void UpdateProduct(Product product)
        {
             _ProductRepository.update(product);
            _ProductRepository.save();
        }
    }
}
