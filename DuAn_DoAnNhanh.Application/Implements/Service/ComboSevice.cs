using DuAn_DoAnNhanh.Data.Implements.Repository;
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
using DuAn_DoAnNhanh.Data.Interface.Repositories;
using DuAn_DoAnNhanh.Data.Implements.UnitOfWork;

namespace DuAn_DoAnNhanh.Application.Implements.Service
{
    public class ComboSevice : IComboService
    {
        private readonly IUnitOfWork _unitOfWork;
        public ComboSevice(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public void AddCombo(Combo combo)
        {
            combo.Status = StatusCombo.Activity;
            combo.CreteDate = DateTime.Now;
            _unitOfWork.ComboRepo.Add(combo);
            _unitOfWork.ComboRepo.SaveChanges();
        }

        public void DeleteCombo(Guid id)
        {
            var comboDelete = _unitOfWork.ComboRepo.GetById(id);
            comboDelete.Status = StatusCombo.InActivity;
            UpdateCombo(comboDelete);

        }

        public List<Combo> GetAllCombo()
        {
            return _unitOfWork.ComboRepo.GetAll().OrderByDescending(p => p.CreteDate).ToList(); // Removed status filter
        }

        public Combo GetComboById(Guid id)
        {
            return _unitOfWork.ComboRepo.GetById(id);
        }

        public void UpdateCombo(Combo combo)
        {
            _unitOfWork.ComboRepo.Update(combo);
            _unitOfWork.ComboRepo.SaveChanges();
        }
        public async Task<Guid> CreateComboAsync(Combo combo, List<ProductCombo> productCombos)
        {
            // Thêm combo mới vào cơ sở dữ liệu
            _unitOfWork.ComboRepo.Add(combo);
            await _unitOfWork.CompleteAsync();
            decimal totalPrice = 0;
            // Thêm các sản phẩm vào combo
            foreach (var productCombo in productCombos)
            {
                productCombo.ComboID = combo.ComboID;// Liên kết ProductCombo với Combo vừa tạo
                await _unitOfWork.ProductComboRepo.AddAsync(productCombo);
                // Lấy giá sản phẩm và tính tổng
                var product = await _unitOfWork.ProductRepo.GetByIdAsync(productCombo.ProductID);
                if (product != null)
                {
                    totalPrice += product.Price * productCombo.Quantity;
                }
            }
            // Cập nhật giá cho combo
            combo.Price = totalPrice;
            _unitOfWork.ComboRepo.Update(combo);
            await _unitOfWork.CompleteAsync();

            // Trả về ID của combo vừa tạo
            return combo.ComboID;
        }
        public List<Combo> GetDeactivatedCombos()
        {
            return _unitOfWork.ComboRepo.GetAll().Where(x => x.Status == StatusCombo.InActivity).OrderByDescending(p => p.CreteDate).ToList();
        }
    }
}
