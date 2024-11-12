using DuAn_DoAnNhanh.Data.EF;
using DuAn_DoAnNhanh.Data.Entities;
using DuAn_DoAnNhanh.Data.Enum;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DuAn_DoAnNhanh.Manage.Controllers
{
    public class ComboDetailsController : Controller
    {
        private readonly MyDBContext _dbContext;
        public ComboDetailsController(MyDBContext dbContext)
        {
            _dbContext = dbContext;
        }
        
        public IActionResult Create(Guid productID, Guid comboID,int quantity)
        {
            var productCombo = new ProductCombo();
            productCombo.ProductID = productID;
            productCombo.ComboID = comboID;
            productCombo.Status = StatusCombo.Activity;
            productCombo.Quantity = quantity;
            _dbContext.productCombos.Add(productCombo);
            _dbContext.SaveChanges();

          
            var combo = _dbContext.Combos
                .Include(c => c.ProductComboes) // Tải ProductCombos liên quan
                .ThenInclude(pc => pc.Product) // Tải Product liên quan
                .FirstOrDefault(c => c.ComboID == comboID);

            if (combo != null)
            {
                // Tính toán lại giá cho Combo
                combo.Price = combo.ProductComboes.Sum(pc => pc.Quantity * pc.Product.Price);
                _dbContext.Combos.Update(combo);
                _dbContext.SaveChanges();
            }

            return RedirectToAction("Details", "Combo", new { id = comboID });

        }

        [HttpPost]
        public IActionResult Edit(Guid productID, Guid comboID, int quantity)
        {
            var productCombo = _dbContext.productCombos.FirstOrDefault(x=>x.ProductID==productID&&x.ComboID==comboID);
            productCombo.Quantity=quantity;
            _dbContext.productCombos.Update(productCombo);
            _dbContext.SaveChanges();
            var combo = _dbContext.Combos
              .Include(c => c.ProductComboes) // Tải ProductCombos liên quan
              .ThenInclude(pc => pc.Product) // Tải Product liên quan
              .FirstOrDefault(c => c.ComboID == comboID);

            if (combo != null)
            {
                // Tính toán lại giá cho Combo
                combo.Price = combo.ProductComboes.Sum(pc => pc.Quantity * pc.Product.Price);
                _dbContext.Combos.Update(combo);
                _dbContext.SaveChanges();
            }
            return RedirectToAction("Details", "Combo", new { id = comboID });

        }
    }
}
