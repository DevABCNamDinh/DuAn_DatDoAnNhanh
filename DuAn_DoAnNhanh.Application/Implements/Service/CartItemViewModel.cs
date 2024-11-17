
namespace DuAn_DoAnNhanh.Application.Implements.Service
{
    internal class CartItemViewModel
    {
        public Guid ProductId { get; set; }
        public string ProductName { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
    }
}