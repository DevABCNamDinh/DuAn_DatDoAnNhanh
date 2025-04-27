using DuAn_DoAnNhanh.Data.EF;
using DuAn_DoAnNhanh.Data.Implements.Repository;
using DuAn_DoAnNhanh.Data.Interface.Repositories;
using DuAn_DoAnNhanh.Data.Interface.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DuAn_DoAnNhanh.Data.Implements.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly MyDBContext _context;
        public IProductRepository ProductRepo { get; private set; }

        public IComboRepository ComboRepo { get; private set; }

        public IUserRepository UserRepo { get; private set; }

        public IBillRepository BillRepo { get; private set; }

        public ICartRepository CartRepo { get; private set; }

        public IProductComboRepository ProductComboRepo { get; private set; }

        public IBillDetailsRepository BillDetailsRepo { get; private set; }

        public ICartItemRepository CartItemRepo { get; private set; }

        public IAddressRepository AddressRepo { get; private set; }

        public IStoresRepository StoresRepo { get; private set; }

        public UnitOfWork(MyDBContext context)
        {
            _context = context;
            ProductRepo = new ProductRepository(_context);
            ComboRepo = new ComboRepository(_context);
            CartRepo = new CartRepository(_context);
            UserRepo = new UserRepository(_context);
            BillRepo = new BillRepository(_context);
            ProductComboRepo = new ProductComboRepository(_context);
            CartItemRepo = new CartItemRepository(_context);
            BillDetailsRepo = new BillDetailsRepository(_context);
            AddressRepo = new AddressRepository(_context);
            StoresRepo = new StoresRepository(_context);
        }
        public int Complete()
        {
            return _context.SaveChanges();
        }
        public async Task<int> CompleteAsync()
        {
            return await _context.SaveChangesAsync();
        }
        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
