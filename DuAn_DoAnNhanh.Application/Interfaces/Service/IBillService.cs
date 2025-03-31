using DuAn_DoAnNhanh.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DuAn_DoAnNhanh.Application.Interfaces.Service
{
    public interface IBillService
    {
        IEnumerable<Bill> GetAllBill();
        Bill GetBillById(Guid id);
        void AddBill(Bill bill);
        
        void UpdateBill(Bill bill);
    }
}
