using DuAn_DoAnNhanh.Application.Interfaces.Service;
using DuAn_DoAnNhanh.Data.EF;
using DuAn_DoAnNhanh.Data.Entities;
using DuAn_DoAnNhanh.Data.Enum;
using DuAn_DoAnNhanh.Data.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Net.Http;

namespace DuAn_DoAnNhanh.Client.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserService _userService;
        private readonly IAddressService _addressService;
        private readonly MyDBContext _dbContext;
        public UserController(IUserService userService,IAddressService addressService,MyDBContext dbContext)
        {
            _userService = userService;
            _addressService = addressService;
            _dbContext = dbContext;

        }
     

        [HttpPost]
        public IActionResult Login(LoginRegisterViewModel loginViewModel)
        {

            if (loginViewModel.Login.Email==null||loginViewModel.Login.Password==null)
            {
                TempData["ActiveTab"] = "login";
                TempData["LoginRegisterModel"] =JsonConvert.SerializeObject(loginViewModel);
                TempData["OpenSignInModal"] = true;              
                return RedirectToAction("Index", "Home");

            }

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

       


        [HttpPost]
	
		public IActionResult Register(LoginRegisterViewModel registerViewModel)
        {
            //// Kiểm tra dữ liệu model hợp lệ chưa
            if (registerViewModel.Register.Email==null|| registerViewModel.Register.Password == null 
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
                var bill = _dbContext.Bill.Where(x => x.UserID == userId).Include(x => x.BillDetails).OrderByDescending(x=>x.BillDate).ToList();
                if(status!=null)
                { 
                    bill=bill.Where(x=>x.Status== status).ToList();              
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
            var address = _dbContext.addresses.Where(x => x.UserID == userId);
            var defaultAddresses = address.Where(x => x.AddressType == AddressType.Default).ToList();
            var normalAddresses = address.Where(x => x.AddressType != AddressType.Default)
                                 .OrderByDescending(x => x.CreateDate)
                                 .ToList();
            var result = defaultAddresses.Concat(normalAddresses).ToList();
            return View(result);
        }
        [HttpPost]
        public IActionResult SaveAddress(string province, string district, string ward, string specificAddress,string fullName,string numberPhone)
        {
            var userIdString = HttpContext.Session.GetString("UserId");
            if (string.IsNullOrEmpty(userIdString) || !Guid.TryParse(userIdString, out var userId))
            {
                return RedirectToAction("Index", "Home");
            }
            var fullAddress = $"{specificAddress}, {ward}, {district}, {province}";

            // Gọi API để lấy kinh độ và vĩ độ
            var coordinates = _addressService.GetCoordinates(fullAddress);
            if (coordinates == (0.0,0.0))
            {
                return BadRequest("Không thể lấy tọa độ từ địa chỉ.");
            }

            // Lưu vào cơ sở dữ liệu
            var address = new Address
            {
                Province = province,
                District = district,
                Ward = ward,
                SpecificAddress = specificAddress,
                Latitude = coordinates.Latitude,
                Longitude = coordinates.Longitude,
                FullAddress = fullAddress,
                FullName = fullName,
                NumberPhone = numberPhone,
                UserID = userId,
                AddressType=AddressType.Default,
                CreateDate = DateTime.Now,
            };

            // Giả sử bạn đang sử dụng DbContext để lưu vào cơ sở dữ liệu
            _dbContext.addresses.Add(address);
            _dbContext.SaveChangesAsync();

            return RedirectToAction("Address");
        }

        [HttpPost]
        public IActionResult UpdateAddress(Address address)
        {
            if (address==null)
            {

            }
            else
            {
                var coordinates = _addressService.GetCoordinates(address.FullAddress);
                var addressUpdate = _dbContext.addresses.Find(address.AddressID);
                addressUpdate.FullAddress = address.FullAddress;
                addressUpdate.FullName = address.FullName;
                addressUpdate.NumberPhone = address.NumberPhone;
                addressUpdate.Province = address.Province;
                addressUpdate.District = address.District;
                addressUpdate.Ward = address.Ward;
                addressUpdate.SpecificAddress = address.SpecificAddress;
                addressUpdate.AddressType = address.AddressType;
                addressUpdate.Latitude = coordinates.Latitude;
                addressUpdate.Longitude = coordinates.Longitude;
                _dbContext.Update(addressUpdate);
                _dbContext.SaveChanges();
            }
            return RedirectToAction("Address");
        }
        [HttpPost]
        public IActionResult UpdateAddressType(Address address) 
        {
            if (address==null) 
            { 
            }
            else 
            {
                var listAddress = _dbContext.addresses.ToList();
                foreach (var item in listAddress)
                {
                    var listAddressUpdate = _dbContext.addresses.Find(item.AddressID);
                    listAddressUpdate.AddressType=AddressType.Normal;
                    _dbContext.Update(listAddressUpdate);
                }
                var addressUpdate = _dbContext.addresses.Find(address.AddressID);
                addressUpdate.AddressType=AddressType.Default;
                _dbContext.Update(addressUpdate);
                _dbContext.SaveChanges();
            }
            return RedirectToAction("Address");
        }
        //private (double Latitude, double Longitude) GetCoordinates(string address)
        //{
        //    string url = $"https://geocode.search.hereapi.com/v1/geocode?q={address}&apiKey={_apiKey}";

        //    var response = _httpClient.GetAsync(url).Result; // Chuyển thành đồng bộ
        //    if (!response.IsSuccessStatusCode)
        //        return (0.0, 0.0);

        //    var result = response.Content.ReadAsStringAsync().Result; // Chuyển thành đồng bộ

        //    // Parse kết quả JSON trả về từ Here API để lấy tọa độ
        //    // Giả sử kết quả trả về có dạng:
        //    // {"items":[{"position":{"lat":21.0285,"lng":105.8542}}]}
        //    var coordinates = JsonConvert.DeserializeObject<dynamic>(result);
        //    if (coordinates.items != null && coordinates.items.Count > 0)
        //    {
        //        double latitude = coordinates.items[0].position.lat;
        //        double longitude = coordinates.items[0].position.lng;
        //        return (latitude, longitude);
        //    }

        //    return (0.0, 0.0);
        //}

    }
}
