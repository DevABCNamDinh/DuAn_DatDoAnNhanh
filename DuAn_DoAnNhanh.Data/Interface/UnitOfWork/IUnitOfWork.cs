﻿using DuAn_DoAnNhanh.Data.Interface.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DuAn_DoAnNhanh.Data.Interface.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        IProductRepository ProductRepo { get; }
        IComboRepository ComboRepo { get; }
        IUserRepository UserRepo { get; }
        IBillRepository BillRepo { get; }
        IBillDetailsRepository BillDetailsRepo { get; }
        ICartRepository CartRepo { get; }
        ICartItemRepository CartItemRepo { get; }
        IProductComboRepository ProductComboRepo { get; }
        IAddressRepository AddressRepo { get; }
        IStoresRepository StoresRepo { get; }
        IComboItemsArchiveRepository ComboItemsArchiveRepo { get; }
        IBillPaymentRepository BillPaymentRepo { get; }
        IBillNotesRepository BillNotesRepo { get; }
        IBillDeliveryRepository BillDeliveryRepo { get; }
        Task<int> CompleteAsync();
        int Complete();
        void BeginTransaction();
        void Commit();
        Task CommitAsync();
        void Rollback();
        Task RollbackAsync();
    }
}
