using DuAn_DoAnNhanh.Data.EF;
using DuAn_DoAnNhanh.Data.Entities;
using DuAn_DoAnNhanh.Data.Interface.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DuAn_DoAnNhanh.Data.Implements.Repository
{
    public class BillRepository:GenericRepository<Bill>,IBillRepository
    {
        private readonly MyDBContext _context;
        public BillRepository(MyDBContext context) : base(context)
        {
            _context = context;
        }

        public IEnumerable<Bill> GetAllBillIncludeUser()
        {
            return _context.Bill
            .Include(x => x.User)
            .OrderByDescending(x => x.BillDate).ToList();
        }
    }
}
