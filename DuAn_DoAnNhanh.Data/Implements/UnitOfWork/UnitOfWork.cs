using DuAn_DoAnNhanh.Data.EF;
using DuAn_DoAnNhanh.Data.Implements.Repository;
using DuAn_DoAnNhanh.Data.Interface.Repositories;
using DuAn_DoAnNhanh.Data.Interface.UnitOfWork;
using Microsoft.EntityFrameworkCore.Storage;
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
        private IDbContextTransaction _transaction;
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

        public IComboItemsArchiveRepository ComboItemsArchiveRepo { get; private set; }

        public IBillPaymentRepository BillPaymentRepo { get; private set; }

        public IBillNotesRepository BillNotesRepo { get; private set; }

        public IBillDeliveryRepository BillDeliveryRepo { get; private set; }

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
            ComboItemsArchiveRepo = new ComboItemsArchiveRepository(_context);
            BillPaymentRepo = new BillPaymentRepository(_context);
            BillNotesRepo = new BillNotesRepository(_context);
            BillDeliveryRepo = new BillDeliveryRepository(_context);
        }
        public void BeginTransaction()
        {
            _transaction = _context.Database.BeginTransaction();
        }
        public void Commit()
        {
            try
            {
                _context.SaveChanges();
                _transaction?.Commit();
            }
            catch
            {
                Rollback();
                throw;
            }
            finally
            {
                _transaction?.Dispose();
                _transaction = null;
            }
        }

        public async Task CommitAsync()
        {
            try
            {
                await _context.SaveChangesAsync();
                _transaction?.Commit();
            }
            catch
            {
                await RollbackAsync();
                throw;
            }
            finally
            {
                _transaction?.Dispose();
                _transaction = null;
            }
        }

        public void Rollback()
        {
            _transaction?.Rollback();
            _transaction?.Dispose();
            _transaction = null;
        }

        public async Task RollbackAsync()
        {
            if (_transaction != null)
            {
                await _transaction.RollbackAsync();
                await _transaction.DisposeAsync();
                _transaction = null;
            }
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
            _transaction?.Dispose();
            _context.Dispose();
        }
    }
}
