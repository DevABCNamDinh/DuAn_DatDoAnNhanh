using DuAn_DoAnNhanh.Data.Entities;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DuAn_DoAnNhanh.Application.Interfaces.Service
{
    public interface IProductService
    {
        List<Product> GetAllProduct();
        Product GetProductById(Guid id);
        void AddProduct(Product product, IFormFile ImageFile);
        void DeleteProduct(Guid id);
        void UpdateProduct(Product product, IFormFile ImageFile);
    }
}
