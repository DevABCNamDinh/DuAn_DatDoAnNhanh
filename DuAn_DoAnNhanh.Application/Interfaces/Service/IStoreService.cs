﻿using DuAn_DoAnNhanh.Data.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DuAn_DoAnNhanh.Application.Interfaces.Service
{
    public interface IStoreService
    {
        Task CreateStoreAsync(StoreViewModel storeViewModel);
    }
}
