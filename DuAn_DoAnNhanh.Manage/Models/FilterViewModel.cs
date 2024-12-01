using DuAn_DoAnNhanh.Data.Entities;

namespace DuAn_DoAnNhanh.Manage.Models
{
    public class FilterViewModel
    {
        public string Keyword { get; set; }
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 5;
        public List<Product> Products { get; set; }
    }
}
