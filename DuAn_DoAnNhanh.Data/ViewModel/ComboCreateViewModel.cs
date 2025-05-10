using DuAn_DoAnNhanh.Data.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DuAn_DoAnNhanh.Data.ViewModel
{
    public class ComboCreateViewModel
    {
        [Required(ErrorMessage = "Vui lòng nhập tên combo.")]
        [StringLength(100, ErrorMessage = "Tên combo không được vượt quá 100 ký tự.")]
        [RegularExpression(@"^\s*\S.*\S\s*$", ErrorMessage = "Tên combo không thể chỉ chứa khoảng trắng hoặc ký tự đặc biệt.")]
        public string ComboName { get; set; }

        public string? Description { get; set; }

        [MinLength(2, ErrorMessage = "Vui lòng chọn ít nhất 2 sản phẩm để tạo combo.")]
        public List<SelectedProduct> SelectedProducts { get; set; } = new List<SelectedProduct>();

        public List<Product> AvailableProducts { get; set; } = new List<Product>();

        [Required(ErrorMessage = "Vui lòng chọn hình ảnh cho combo.")]
        public string ImageFile { get; set; }
    }

    public class SelectedProduct
    {
        public Guid ProductID { get; set; }
        public int Quantity { get; set; }
        public bool IsSelected { get; set; }
    }
}