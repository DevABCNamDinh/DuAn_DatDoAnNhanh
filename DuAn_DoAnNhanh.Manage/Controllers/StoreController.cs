using DuAn_DoAnNhanh.Application.Interfaces.Service;
using DuAn_DoAnNhanh.Data.EF;
using DuAn_DoAnNhanh.Data.Entities;
using DuAn_DoAnNhanh.Data.Enum;
using DuAn_DoAnNhanh.Data.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DuAn_DoAnNhanh.Manage.Controllers
{
    public class StoreController : Controller
    {
        private readonly MyDBContext _dbContext;
        private readonly IAddressService _addressService;
        private readonly IEmailService _emailService;

        public StoreController(MyDBContext dBContext, IAddressService addressService, IEmailService emailService)
        {
            _dbContext = dBContext;
            _addressService = addressService;
            _emailService = emailService;
        }

        public IActionResult Index()
        {
            var storeList = _dbContext.Stores.Include(x => x.Address)
                .Include(x => x.Users)
                .Where(x => x.Status == Status.Activity)
                .OrderByDescending(x => x.CreateDate)
                .ToList();
            return View(storeList);
        }

        public async Task<IActionResult> CreateStore(StoreViewModel storeViewModel)
        {
            Store store = new Store()
            {
                StoreID = Guid.NewGuid(),
                StoreName = storeViewModel.StoreName,
                Status = Status.Activity,
                CreateDate = DateTime.Now,
            };

            _dbContext.Stores.Add(store);
            _dbContext.SaveChanges();

            var fullAddress = $"{storeViewModel.SpecificAddress}, {storeViewModel.Ward}, {storeViewModel.District}, {storeViewModel.Province}";
            var coordinates = _addressService.GetCoordinates(fullAddress);

            Address address = new Address()
            {
                AddressID = Guid.NewGuid(),
                StoreID = store.StoreID,
                UserID = null,
                FullName = store.StoreName,
                NumberPhone = storeViewModel.NumberPhone,
                Province = storeViewModel.Province,
                District = storeViewModel.District,
                Ward = storeViewModel.Ward,
                SpecificAddress = storeViewModel.SpecificAddress,
                FullAddress = fullAddress,
                Latitude = coordinates.Latitude,
                Longitude = coordinates.Longitude,
                AddressType = AddressType.Default,
                CreateDate = DateTime.Now,
                Status = Status.Activity,
            };

            _dbContext.addresses.Add(address);
            _dbContext.SaveChanges();

            // Tạo mật khẩu ngẫu nhiên
            string randomPassword = GenerateRandomPassword();

            User user = new User()
            {
                UserID = Guid.NewGuid(),
                StoreID = store.StoreID,
                FullName = storeViewModel.ManagerName,
                Password = randomPassword,
                Email = storeViewModel.Email,
                CreateDate = DateTime.Now,
                Role = Role.Manager,
                Status = Status.Activity,
            };

            _dbContext.Users.Add(user);
            _dbContext.SaveChanges();

            string subject = "Thông tin tài khoản quản lý cửa hàng";

            string body = $@"
                <p>Xin chào <strong>{storeViewModel.ManagerName}</strong>,</p>
                <p>Bạn đã được đăng ký làm <strong>quản lý cửa hàng</strong> thành công. Dưới đây là thông tin chi tiết:</p>

                <h4>🛍️ Thông tin cửa hàng:</h4>
                <ul>
                    <li><strong>Tên cửa hàng:</strong> {storeViewModel.StoreName}</li>
                    <li><strong>Địa chỉ:</strong> {fullAddress}</li>
                    <li><strong>Số điện thoại:</strong> {storeViewModel.NumberPhone}</li>
                </ul>

                <h4>🔐 Thông tin đăng nhập:</h4>
                <ul>
                    <li><strong>Email:</strong> {storeViewModel.Email}</li>
                    <li><strong>Mật khẩu:</strong> {randomPassword}</li>
                </ul>

                <p style='color: red;'><b>Lưu ý:</b> Vui lòng đổi mật khẩu ngay sau khi đăng nhập để đảm bảo an toàn.</p>

                <p>Trân trọng,<br>Hệ thống quản lý cửa hàng</p>
            ";


            await _emailService.SendEmailAsync(storeViewModel.Email, subject, body);

            return RedirectToAction("Index");
        }


        // Hàm tạo mật khẩu ngẫu nhiên
        private string GenerateRandomPassword(int length = 8)
        {
            const string validChars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890!@#$%^&*";
            Random random = new Random();
            return new string(Enumerable.Repeat(validChars, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }

    }
}
