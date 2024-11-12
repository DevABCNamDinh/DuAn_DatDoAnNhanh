using DuAn_DoAnNhanh.Application.Implements.Repository;
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
    public class ComboSevice : IComboService
    {
        private readonly IGenericRepository<Combo> _genericRepository; 
        public ComboSevice(IGenericRepository<Combo> genericRepository)
        {
            _genericRepository = genericRepository;
        }
        public void AddCombo(Combo combo)
        {
            combo.Status = StatusCombo.Activity;
            combo.CreteDate= DateTime.Now;
           _genericRepository.insert(combo);
            _genericRepository.save();
        }

        public void DeleteCombo(Guid id)
        {
            throw new NotImplementedException();
        }

        public List<Combo> GetAllCombo()
        {
            return _genericRepository.GetAll();
        }

        public Combo GetComboById(Guid id)
        {
            return _genericRepository.GetById(id);
        }

        public void UpdateCombo(Combo combo)
        {
            _genericRepository.update(combo);
            _genericRepository.save();
        }
    }
}
