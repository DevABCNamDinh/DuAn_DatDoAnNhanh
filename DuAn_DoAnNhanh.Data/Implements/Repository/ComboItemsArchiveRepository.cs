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
    public class ComboItemsArchiveRepository : GenericRepository<ComboItemsArchive>, IComboItemsArchiveRepository
    {
        private readonly MyDBContext _dbContext;
        public ComboItemsArchiveRepository(MyDBContext context) : base(context)
        {
            _dbContext = context;
        }
    }
}
