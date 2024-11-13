using DuAn_DoAnNhanh.Application.Implements.Service;
using DuAn_DoAnNhanh.Application.Interfaces.Service;
using DuAn_DoAnNhanh.Data.EF;
using DuAn_DoAnNhanh.Data.Entities;
using DuAn_DoAnNhanh.Data.Enum;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DuAn_DoAnNhanh.Manage.Controllers
{
    public class ComboDetailsController : Controller
    {
       
        private readonly IComboDetailsService _comboDetailsService;


        public ComboDetailsController( IComboDetailsService comboDetailsService)
        {          
            _comboDetailsService = comboDetailsService;
        }
        
        public IActionResult Create(ProductCombo productCombo)
        {
           _comboDetailsService.AddProductCombo(productCombo);
            return RedirectToAction("Details", "Combo", new { id = productCombo.ComboID });

        }

        [HttpPost]
        public IActionResult Edit(ProductCombo productCombo)
        {
           _comboDetailsService.UpdateCombo(productCombo);   
            return RedirectToAction("Details", "Combo", new { id = productCombo.ComboID });

        }

        public IActionResult Delete(Guid productID,Guid comboID) {
           _comboDetailsService.DeleteComboDetailsByproductIDcomboID(productID, comboID);
            return RedirectToAction("Details", "Combo", new {id=comboID});
        }

       
    }
}
