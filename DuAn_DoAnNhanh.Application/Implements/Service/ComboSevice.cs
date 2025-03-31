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
        private readonly IUnitOfWork _unitOfWork;
        public ComboSevice(IUnitOfWork unitOfWork)
        {
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
    }
}
