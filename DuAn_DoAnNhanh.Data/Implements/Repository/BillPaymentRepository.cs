using DuAn_DoAnNhanh.Data.EF;
using DuAn_DoAnNhanh.Data.Entities;
using DuAn_DoAnNhanh.Data.Interface.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DuAn_DoAnNhanh.Data.Implements.Repository
{
    public class BillPaymentRepository : GenericRepository<BillPayment>, IBillPaymentRepository
    {
        private readonly MyDBContext _dbContext;
        public BillPaymentRepository(MyDBContext context) : base(context)
        {
            _dbContext = context;
        }
    }
}
