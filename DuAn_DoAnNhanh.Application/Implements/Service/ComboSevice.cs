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

namespace DuAn_DoAnNhanh.Application.Implements.Service
{
    public class ComboSevice : IComboService
    {

        private readonly IGenericRepository<Combo> _genericRepository;
        private readonly IGenericRepository<ProductCombo> _productComboRepository;
 private readonly IUnitOfWork _unitOfWork;
        public ComboSevice(IGenericRepository<Combo> comboRepository, IGenericRepository<ProductCombo> productComboRepository,IUnitOfWork unitOfWork)
        {
            _genericRepository = comboRepository;
            _productComboRepository = productComboRepository;
            _unitOfWork = unitOfWork;
        }
        public void AddCombo(Combo combo)
        {
            combo.Status = StatusCombo.Activity;
            combo.CreteDate= DateTime.Now;
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
            return _unitOfWork.ComboRepo.GetAll().Where(x=>x.Status==StatusCombo.Activity).OrderByDescending(p => p.CreteDate).ToList();
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
            _genericRepository.insert(combo);
            _genericRepository.save();

            // Thêm các sản phẩm vào combo
            foreach (var productCombo in productCombos)
            {
                productCombo.ComboID = combo.ComboID; // Liên kết ProductCombo với Combo vừa tạo
                _productComboRepository.insert(productCombo);
            }
            _productComboRepository.save();

            // Trả về ID của combo vừa tạo
            return combo.ComboID;
        }
    }
}
