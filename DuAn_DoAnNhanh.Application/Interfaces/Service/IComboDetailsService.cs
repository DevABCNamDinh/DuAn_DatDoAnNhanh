using DuAn_DoAnNhanh.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DuAn_DoAnNhanh.Application.Interfaces.Service
{
    public interface IComboDetailsService
    {
       
        Combo GetProductComboByIdComboIdProduct(Guid comboID,Guid productID);
        void AddProductCombo(ProductCombo productCombo);
        void DeleteComboDetailsByproductIDcomboID(Guid productID, Guid comboID);
        void UpdateProductCombo(ProductCombo productCombo);
         List<Product> listProductInCombo(Guid id);
    }
}
