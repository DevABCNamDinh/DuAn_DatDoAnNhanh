using DuAn_DoAnNhanh.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DuAn_DoAnNhanh.Application.Interfaces.Service
{
    public interface IComboService
    {
        List<Combo> GetAllCombo();
        Combo GetComboById(Guid id);
        void AddCombo(Combo combo);
        void DeleteCombo(Guid id);
        void UpdateCombo(Combo combo);
        Task<Guid> CreateComboAsync(Combo combo, List<ProductCombo> productCombos);
        public List<Combo> GetDeactivatedCombos();
    }
}
