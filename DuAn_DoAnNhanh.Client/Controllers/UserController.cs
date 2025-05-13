using DuAn_DoAnNhanh.Application.Interfaces.Service;
using DuAn_DoAnNhanh.Data.EF;
using DuAn_DoAnNhanh.Data.Entities;
using DuAn_DoAnNhanh.Data.Enum;
using DuAn_DoAnNhanh.Data.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net.Http;

namespace DuAn_DoAnNhanh.Client.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserService _userService;
        private readonly IAddressService _addressService;
        private readonly MyDBContext _dbContext;
        public UserController(IUserService userService, IAddressService addressService, MyDBContext dbContext)
        {
            _userService = userService;
            _addressService = addressService;
            _dbContext = dbContext;

        }


        [HttpPost]
        public IActionResult Login(LoginRegisterViewModel loginViewModel)
        {

            if (loginViewModel.Login.Email == null || loginViewModel.Login.Password == null)
            {
                TempData["ActiveTab"] = "login";
                TempData["LoginRegisterModel"] = JsonConvert.SerializeObject(loginViewModel);
                TempData["OpenSignInModal"] = true;
                return RedirectToAction("Index", "Home");

            }
            try
            {
                var user = _userService.Login(loginViewModel.Login);

                if (user == null)
                {
                    TempData["LoginError"] = "Email hoặc mật khẩu không đúng.";
                    TempData["OpenSignInModal"] = true;
                    TempData["ActiveTab"] = "login";
                    TempData["LoginRegisterModel"] = JsonConvert.SerializeObject(loginViewModel);
                    return RedirectToAction("Index", "Home");

                }

                HttpContext.Session.SetString("UserId", user.UserID.ToString());
                HttpContext.Session.SetString("UserName", user.FullName);
                TempData["Message"] = "Đăng nhập thành công!";

                return RedirectToAction("Index", "Home");
            }
            catch (Exception ex)
            {

                TempData["LoginError"] = ex.Message;
                TempData["OpenSignInModal"] = true;
                TempData["ActiveTab"] = "login";
                TempData["LoginRegisterModel"] = JsonConvert.SerializeObject(loginViewModel);
                return RedirectToAction("Index", "Home");
            }



        }




        [HttpPost]

        public IActionResult Register(LoginRegisterViewModel registerViewModel)
        {
            //// Kiểm tra dữ liệu model hợp lệ chưa
            if (registerViewModel.Register.Email == null || registerViewModel.Register.Password == null
                || registerViewModel.Register.ConfirmPassword == null || registerViewModel.Register.FullName == null)
            {
                //TempData["RegisterError"] = "Thông tin đăng ký không hợp lệ.";
                TempData["OpenSignInModal"] = true;
                TempData["ShowRegisterTab"] = true;
                TempData["ActiveTab"] = "register";
                TempData["LoginRegisterModel"] = JsonConvert.SerializeObject(registerViewModel);
                return RedirectToAction("Index", "Home");
            }

            try
            {
                // Tạo user mới từ view model (mã hóa, role, status sẽ xử lý bên service)
                var user = new User()
                {
                    FullName = registerViewModel.Register.FullName,
                    Email = registerViewModel.Register.Email,
                    Password = registerViewModel.Register.Password
                };

                _userService.Register(user);

                TempData["Message"] = "Đăng ký thành công! Mời bạn đăng nhập.";
                TempData["OpenSignInModal"] = true;
                return RedirectToAction("Index", "Home");

            }
            catch (Exception ex)
            {
                TempData["RegisterError"] = ex.Message;
                TempData["OpenSignInModal"] = true;
                TempData["ActiveTab"] = "register";
                TempData["LoginRegisterModel"] = JsonConvert.SerializeObject(registerViewModel);
                return RedirectToAction("Index", "Home");
            }


        }





        public IActionResult Logout()
        {
            HttpContext.Session.Remove("UserId");
            HttpContext.Session.Remove("UserName");
            return RedirectToAction("Index", "Home");
        }
        public IActionResult History(StatusOrder? status)
        {
            var userIdString = HttpContext.Session.GetString("UserId");
            if (string.IsNullOrEmpty(userIdString) || !Guid.TryParse(userIdString, out var userId))
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                var bill = _dbContext.Bill.Where(x => x.UserID == userId).Include(x => x.BillDetails).OrderByDescending(x => x.BillDate).ToList();
                if (status != null)
                {
                    bill = bill.Where(x => x.Status == status).ToList();
                }
                ViewBag.ShowSidebar = true;
                return View(bill);
            }

        }
        public IActionResult Information()
        {
            ViewBag.ShowSidebar = true;
            return View();
        }
        public IActionResult Address()
        {
            ViewBag.ShowSidebar = true;
            var userIdString = HttpContext.Session.GetString("UserId");
            if (string.IsNullOrEmpty(userIdString) || !Guid.TryParse(userIdString, out var userId))
            {
                return RedirectToAction("Index", "Home");
            }
            var address = _dbContext.addresses.Where(x => x.UserID == userId && x.AddressType != AddressType.Inactive);
            var defaultAddresses = address.Where(x => x.AddressType == AddressType.Default).ToList();
            var normalAddresses = address.Where(x => x.AddressType != AddressType.Default)
                                 .OrderByDescending(x => x.CreateDate)
                                 .ToList();
            var result = defaultAddresses.Concat(normalAddresses).ToList();
            return View(result);
        }






        [HttpPost]
        public IActionResult SaveAddress(
            Guid? AddressID,
            string province,
            string district,
            string ward,
            string specificAddress,
            string fullName,
            string numberPhone,
            string? Description,
            bool SetAsDefault)
        {
            var userIdString = HttpContext.Session.GetString("UserId");
            if (string.IsNullOrEmpty(userIdString) || !Guid.TryParse(userIdString, out var userId))
            {
                return RedirectToAction("Index", "Home");
            }

            var fullAddress = $"{specificAddress}, {ward}, {district}, {province}";

            // Lấy tọa độ từ address service
            var coordinates = _addressService.GetCoordinates(fullAddress);
            if (coordinates == (0.0, 0.0))
            {
                return BadRequest("Không thể lấy tọa độ từ địa chỉ.");
            }

            if (AddressID.HasValue)
            {
                // Cập nhật địa chỉ hiện có
                var existingAddress = _dbContext.addresses.FirstOrDefault(a => a.AddressID == AddressID.Value);
                if (existingAddress == null)
                {
                    return NotFound("Không tìm thấy địa chỉ.");
                }

                existingAddress.Province = province;
                existingAddress.District = district;
                existingAddress.Ward = ward;
                existingAddress.SpecificAddress = specificAddress;
                existingAddress.FullAddress = fullAddress;
                existingAddress.FullName = fullName;
                existingAddress.NumberPhone = numberPhone;
                existingAddress.Latitude = coordinates.Latitude;
                existingAddress.Longitude = coordinates.Longitude;
                existingAddress.Description = Description ?? "";

                if (SetAsDefault)
                {
                    var otherAddresses = _dbContext.addresses
                        .Where(a => a.UserID == userId && a.AddressID != existingAddress.AddressID && a.AddressType == AddressType.Default)
                        .ToList();

                    foreach (var addr in otherAddresses)
                    {
                        addr.AddressType = AddressType.Normal;
                    }

                    existingAddress.AddressType = AddressType.Default;
                }

                _dbContext.SaveChanges();
            }
            else
            {
                // Tạo địa chỉ mới
                var newAddress = new Address
                {
                    AddressID = Guid.NewGuid(),
                    Province = province,
                    District = district,
                    Ward = ward,
                    SpecificAddress = specificAddress,
                    FullAddress = fullAddress,
                    FullName = fullName,
                    NumberPhone = numberPhone,
                    Latitude = coordinates.Latitude,
                    Longitude = coordinates.Longitude,
                    UserID = userId,
                    CreateDate = DateTime.Now,
                    Description = Description ?? "",
                    AddressType = AddressType.Normal, // tạm set là Normal
                    Status = Status.Activity
                };

                _dbContext.addresses.Add(newAddress);
                _dbContext.SaveChanges(); // Lưu trước để lấy ID

                if (SetAsDefault)
                {
                    // Reset địa chỉ mặc định khác
                    var otherAddresses = _dbContext.addresses
                        .Where(a => a.UserID == userId && a.AddressID != newAddress.AddressID && a.AddressType == AddressType.Default)
                        .ToList();

                    foreach (var addr in otherAddresses)
                    {
                        addr.AddressType = AddressType.Normal;
                    }

                    newAddress.AddressType = AddressType.Default;
                    _dbContext.SaveChanges();
                }
            }

            return RedirectToAction("Address");
        }



        [HttpGet]
        public IActionResult EditAddress(Guid id)
        {

            var address = _dbContext.addresses.Find(id);

            if (address == null)
            {
                return NotFound();
            }

            return View("EditAddress", address);
        }




        [HttpPost]
        public IActionResult UpdateAddress(Address address, bool SetAsDefault)
        {
            if (address == null)
            {
                return BadRequest("Invalid address data.");
            }

            var addressUpdate = _dbContext.addresses.Find(address.AddressID);
            if (addressUpdate == null)
            {
                return NotFound("Address not found.");
            }

            // Lấy tọa độ của địa chỉ
            var coordinates = _addressService.GetCoordinates(address.FullAddress);

            // Cập nhật các trường thông tin địa chỉ
            addressUpdate.FullAddress = address.FullAddress;
            addressUpdate.FullName = address.FullName;
            addressUpdate.NumberPhone = address.NumberPhone;
            addressUpdate.Province = address.Province;
            addressUpdate.District = address.District;
            addressUpdate.Ward = address.Ward;
            addressUpdate.SpecificAddress = address.SpecificAddress;
            addressUpdate.Latitude = coordinates.Latitude;
            addressUpdate.Longitude = coordinates.Longitude;
            addressUpdate.Description = address.Description;

            if (SetAsDefault)
            {
                // Đặt địa chỉ hiện tại làm mặc định
                addressUpdate.AddressType = AddressType.Default;

                // Bỏ mặc định các địa chỉ khác của user
                //var otherAddresses = _dbContext.addresses
                //    .Where(a => a.UserID == addressUpdate.UserID && a.AddressID != address.AddressID && a.AddressType == AddressType.Default)
                //    .ToList();

                var otherAddresses = _dbContext.addresses
                .Where(a => a.UserID == addressUpdate.UserID && a.AddressID != address.AddressID)
                 .ToList();


                foreach (var item in otherAddresses)
                {
                    item.AddressType = AddressType.Normal;
                }
            }
            else
            {
                // Nếu không tick thì vẫn để lại giá trị hiện tại
                addressUpdate.AddressType = AddressType.Normal;
                // addressUpdate.AddressType = addressUpdate.AddressType == AddressType.Default ? AddressType.Default : AddressType.Normal;
            }

            // Lưu thay đổi vào database
            _dbContext.Update(addressUpdate);
            _dbContext.SaveChanges();

            return RedirectToAction("Address");
        }




        [HttpPost]
        public IActionResult UpdateAddressType(Address address)
        {
            if (address == null)
            {
            }
            else
            {
                var listAddress = _dbContext.addresses.ToList();
                foreach (var item in listAddress)
                {
                    var listAddressUpdate = _dbContext.addresses.Find(item.AddressID);
                    listAddressUpdate.AddressType = AddressType.Normal;
                    _dbContext.Update(listAddressUpdate);
                }
                var addressUpdate = _dbContext.addresses.Find(address.AddressID);
                addressUpdate.AddressType = AddressType.Default;
                _dbContext.Update(addressUpdate);
                _dbContext.SaveChanges();
            }
            return RedirectToAction("Address");
        }



        [HttpPost]
        //[ActionName("ConfirmDelete")]  // Xác nhận rằng đây là POST cho action ji
        public IActionResult MarkAddressAsInActive(Guid id)
        {
            var address = _dbContext.addresses.FirstOrDefault(a => a.AddressID == id); // Lấy địa chỉ theo Guid
            if (address != null)
            {
                address.AddressType = (AddressType)Enum.Parse(typeof(AddressType), "Inactive");
                _dbContext.Update(address);  // Cập nhật địa chỉ vào cơ sở dữ liệu
                _dbContext.SaveChanges();    // Lưu thay đổi
            }

            return RedirectToAction("Address");  // Sau khi xóa, quay lại trang quản lý địa chỉ
        }



    }
}
