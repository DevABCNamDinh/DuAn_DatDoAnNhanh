using DuAn_DoAnNhanh.Application.Implements.Service;
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
        public StoreController(MyDBContext dBContext, IAddressService addressService)
        {
            _dbContext = dBContext;
            _addressService = addressService;
        }
        public IActionResult Index()
        {
            var storeList = _dbContext.Stores.Include(x=>x.Address)
                .Include(x=>x.Users)
                .Where(x=>x.Status==Status.Activity)
                .OrderByDescending(x=>x.CreateDate)
                .ToList();
            return View(storeList);
        }
        public IActionResult CreateStore(StoreViewModel storeViewModel)
        {
            Store store = new Store()
            {
                StoreID=new Guid(),
                StoreName = storeViewModel.StoreName,
                Status=Status.Activity,
                CreateDate=DateTime.Now,
            };
            _dbContext.Stores.Add(store);
            _dbContext.SaveChanges();
            var fullAddress = $"{storeViewModel.SpecificAddress}, {storeViewModel.Ward}, {storeViewModel.District}, {storeViewModel.Province}";
            var coordinates = _addressService.GetCoordinates(fullAddress);
            Address address = new Address()
            {
                AddressID = new Guid(),
                StoreID = store.StoreID,
                UserID = null,
                FullName = store.StoreName,
                NumberPhone = storeViewModel.NumberPhone,
                Province = storeViewModel.Province,
                District = storeViewModel.District,
                Ward = storeViewModel.Ward,
                SpecificAddress = storeViewModel.SpecificAddress,
                FullAddress = fullAddress,
                Latitude=coordinates.Latitude,
                Longitude=coordinates.Longitude,
                AddressType=AddressType.Default,
                CreateDate=DateTime.Now,
                status=Status.Activity,
            };
            _dbContext.addresses.Add(address);
            _dbContext.SaveChanges();
            User user = new User()
            {
                UserID = new Guid(),
                StoreID=store.StoreID,
                FullName=storeViewModel.ManagerName,
                Password="1",
                Email=storeViewModel.Email,
                CreateDate= DateTime.Now,
                Role=Role.Manager,
                Status=Status.Activity,
            };
            _dbContext.Users.Add(user);
            _dbContext.SaveChanges();
            return RedirectToAction("Index");
        }
        
    }
}
