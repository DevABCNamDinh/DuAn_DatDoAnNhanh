using DuAn_DoAnNhanh.Data.Interfaces.Repositories;
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
using DuAn_DoAnNhanh.Data.Interface.UnitOfWork;

namespace DuAn_DoAnNhanh.Application.Implements.Service
{
    public class ComboDetailsService : IComboDetailsService
    {

        private readonly IUnitOfWork _unitOfWork;
        
        public ComboDetailsService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public void AddProductCombo(ProductCombo productCombo)
        {
            var productComboAdd = new ProductCombo();
            productComboAdd.ProductID = productCombo.ProductID;
            productComboAdd.ComboID = productCombo.ComboID;
            productComboAdd.Status = StatusCombo.Activity;
            productComboAdd.Quantity = productCombo.Quantity;
            _unitOfWork.ProductComboRepo.Add(productCombo);
            _unitOfWork.ProductComboRepo.SaveChanges();
            var combo = _unitOfWork.ComboRepo.GetById(productCombo.ComboID);
            if (combo != null)
            {
                // Tính toán lại giá cho Combo
                combo.Price = totalAmount(productCombo.ComboID);
                combo.SetupPrice = null;
                _unitOfWork.ComboRepo.Update(combo);
                _unitOfWork.Complete();
            }
        }
        public void DeleteComboDetailsByproductIDcomboID(Guid productID, Guid comboID)
        {
            var productComboDelete = _unitOfWork.ProductComboRepo.Find(x => x.ProductID == productID && x.ComboID == comboID && x.Status == StatusCombo.Activity);

            _unitOfWork.ProductComboRepo.Delete(productComboDelete);
            _unitOfWork.Complete();
            var combo = _unitOfWork.ComboRepo.GetById(comboID);
            if (combo != null)
            {
                // Tính toán lại giá cho Combo
                combo.Price = totalAmount(comboID);
                combo.SetupPrice= null;
                _unitOfWork.ComboRepo.Update(combo);
                _unitOfWork.Complete();
            }
        }

        public Combo GetProductComboByIdComboIdProduct(Guid comboID, Guid productID)
        {
            throw new NotImplementedException();
        }

        public List<Product> listProductInCombo(Guid id)
        {
            // Loại bỏ điều kiện lọc trạng thái để lấy tất cả ProductCombo
            var listProductInCombo = _unitOfWork.ProductComboRepo.FindAll(x => x.ComboID == id);

            var products = new List<Product>();
            foreach (var product in listProductInCombo)
            {
                var pr = _unitOfWork.ProductRepo.GetById(product.ProductID);
                pr.Quantity = product.Quantity;
                products.Add(pr);
            }

            return products;
        }

        public void UpdateProductCombo(ProductCombo productCombo)
        {
            var productComboEdit =_unitOfWork.ProductComboRepo.Find(x => x.ProductID == productCombo.ProductID && x.ComboID == productCombo.ComboID && x.Status == StatusCombo.Activity);
            productComboEdit.Quantity = productCombo.Quantity;
            _unitOfWork.ProductComboRepo.Update(productComboEdit);
            _unitOfWork.Complete();
            
            var combo =_unitOfWork.ComboRepo.GetById(productCombo.ComboID);

            if (combo != null)
            {
                // Tính toán lại giá cho Combo
                combo.Price = totalAmount(productCombo.ComboID);
                combo.SetupPrice = null;

                _unitOfWork.ComboRepo.Update(combo);
                _unitOfWork.Complete();
            }
        }
        private decimal totalAmount(Guid comboID)
        {
            var combo = _unitOfWork.ComboRepo.GetComboWithDetails(comboID);

            if (combo != null)
                // Tính toán lại giá cho Combo
                return combo.Price = combo.ProductComboes.Where(x => x.Status == StatusCombo.Activity).Sum(pc => pc.Quantity * pc.Product.Price);

            else
                return 0;

        }
    }
}
