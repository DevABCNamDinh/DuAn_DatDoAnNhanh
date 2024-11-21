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
    public class BillService : IBillService
    {
        private readonly IGenericRepository<Bill> _genericRepository;
        public BillService(IGenericRepository<Bill> genericRepository)
        {
            _genericRepository = genericRepository;
        }
    
        public void AddBill(Bill bill)
        {
            _genericRepository.insert(bill);
            _genericRepository.save();
        }

        public List<Bill> GetAllBill()
        {
            return _genericRepository.GetAll();
        }

        public Bill GetBillById(Guid id)
        {
            return _genericRepository.GetById(id);
        }

        public void UpdateBill(Bill bill)
        {
           _genericRepository.update(bill);
            _genericRepository.save();
        }
    }
}
