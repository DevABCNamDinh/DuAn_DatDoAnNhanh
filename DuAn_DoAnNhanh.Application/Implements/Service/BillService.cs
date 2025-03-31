using DuAn_DoAnNhanh.Data.Interfaces.Repositories;
using DuAn_DoAnNhanh.Application.Interfaces.Service;
using DuAn_DoAnNhanh.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DuAn_DoAnNhanh.Data.Implements.UnitOfWork;
using DuAn_DoAnNhanh.Data.Interface.UnitOfWork;

namespace DuAn_DoAnNhanh.Application.Implements.Service
{
    public class BillService : IBillService
    {

        private readonly IUnitOfWork _unitOfWork;
        public BillService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
    
        public void AddBill(Bill bill)
        {
           _unitOfWork.BillRepo.Add(bill);
            _unitOfWork.Complete();
        }

        public IEnumerable<Bill> GetAllBill()
        {
            return _unitOfWork.BillRepo.GetAll();
        }

        public Bill GetBillById(Guid id)
        {
            return _unitOfWork.BillRepo.GetById(id);
        }

        public void UpdateBill(Bill bill)
        {
           _unitOfWork.BillRepo.Update(bill);
            _unitOfWork.Complete();
        }
    }
}
