using DuAn_DoAnNhanh.Data.ViewModel;
using System.Threading.Tasks;

namespace DuAn_DoAnNhanh.Application.Interfaces.Service
{
    public interface IStaffService
    {
        Task CreateStaffAsync(StaffViewModel staffViewModel);
        Task UpdateStaffAsync(StaffViewModel staffViewModel);
    }
}
