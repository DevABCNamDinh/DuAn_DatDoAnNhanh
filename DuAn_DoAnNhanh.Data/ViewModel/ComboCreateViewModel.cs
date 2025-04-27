using DuAn_DoAnNhanh.Data.Entities;
using System;
using System.Collections.Generic;

namespace DuAn_DoAnNhanh.Data.ViewModel
{
    public class ComboCreateViewModel
    {
        public string ComboName { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public List<SelectedProduct> SelectedProducts { get; set; }
       
        public List<Product> AvailableProducts { get; set; } // Thuộc tính mới
       
        public ComboCreateViewModel()
        {
            AvailableProducts = new List<Product>(); // Khởi tạo danh sách để tránh lỗi null
        }
    }
}

    public class SelectedProduct
    {
        public Guid ProductID { get; set; }
        public int Quantity { get; set; }
    }
    
