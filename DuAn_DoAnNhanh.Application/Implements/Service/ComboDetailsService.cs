using DuAn_DoAnNhanh.Application.Interfaces.Repositories;
using DuAn_DoAnNhanh.Application.Interfaces.Service;
using DuAn_DoAnNhanh.Data.EF;
using DuAn_DoAnNhanh.Data.Entities;
using DuAn_DoAnNhanh.Data.Enum;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DuAn_DoAnNhanh.Application.Implements.Service
{
    public class ComboDetailsService : IComboDetailsService
    {
        private readonly IGenericRepository<ProductCombo> _genericRepository;
        private readonly IProductService _productService;

        private readonly MyDBContext _myDBContext;
        
        public ComboDetailsService(IGenericRepository<ProductCombo> genericRepository, MyDBContext myDBContext,IProductService productService)
        {
            _genericRepository = genericRepository;
            _productService = productService;
            _myDBContext = myDBContext;

        }
        public void AddProductCombo(ProductCombo productCombo)
        {
            var productComboAdd = new ProductCombo();
            productComboAdd.ProductID = productCombo.ProductID;
            productComboAdd.ComboID = productCombo.ComboID;
            productComboAdd.Status = StatusCombo.Activity;
            productComboAdd.Quantity = productCombo.Quantity;
            _genericRepository.insert(productCombo);
            _genericRepository.save();
            var combo = _myDBContext.Combos.Find(productCombo.ComboID);
            if (combo != null)
            {
                // Tính toán lại giá cho Combo
                combo.Price = totalAmount(productCombo.ComboID);
                _myDBContext.Combos.Update(combo);
                _myDBContext.SaveChanges();
            }
        }
        public void DeleteComboDetailsByproductIDcomboID(Guid productID, Guid comboID)
        {
            var productComboDelete = _myDBContext.productCombos.FirstOrDefault(x => x.ProductID == productID && x.ComboID == comboID && x.Status == StatusCombo.Activity);
            productComboDelete.Status = StatusCombo.InActivity;
            _myDBContext.Update(productComboDelete);
            _myDBContext.SaveChanges();
            var combo = _myDBContext.Combos.Find(comboID);
            if (combo != null)
            {
                // Tính toán lại giá cho Combo
                combo.Price = totalAmount(comboID);
                _myDBContext.Combos.Update(combo);
                _myDBContext.SaveChanges();
            }
        }

        public Combo GetProductComboByIdComboIdProduct(Guid comboID, Guid productID)
        {
            throw new NotImplementedException();
        }

        public List<Product> listProductInCombo(Guid id)
        {
            var listProductInCombo = _myDBContext.productCombos.Where(x => x.ComboID == id && x.Status == StatusCombo.Activity).ToList();


            var products = new List<Product>();
            foreach (var product in listProductInCombo)
            {
                var pr = _productService.GetProductById(product.ProductID);
                pr.Quantity = product.Quantity;
                products.Add(pr);
            }

            return products;
        }

        public void UpdateProductCombo(ProductCombo productCombo)
        {
            var productComboEdit = _myDBContext.productCombos.FirstOrDefault(x => x.ProductID == productCombo.ProductID && x.ComboID == productCombo.ComboID && x.Status == StatusCombo.Activity);
            productComboEdit.Quantity = productCombo.Quantity;
            _genericRepository.update(productComboEdit);
            _genericRepository.save();
            var combo = _myDBContext.Combos.Find(productCombo.ComboID);

            if (combo != null)
            {
                // Tính toán lại giá cho Combo
                combo.Price = totalAmount(productCombo.ComboID);

                _myDBContext.Combos.Update(combo);
                _myDBContext.SaveChanges();
            }
        }
        private decimal totalAmount(Guid comboID)
        {
            var combo = _myDBContext.Combos
             .Include(c => c.ProductComboes) // Tải ProductCombos liên quan
             .ThenInclude(pc => pc.Product) // Tải Product liên quan
             .FirstOrDefault(c => c.ComboID == comboID);


            // Tính toán lại giá cho Combo
            return combo.Price = combo.ProductComboes.Where(x => x.Status == StatusCombo.Activity).Sum(pc => pc.Quantity * pc.Product.Price);



        }
    }
}
