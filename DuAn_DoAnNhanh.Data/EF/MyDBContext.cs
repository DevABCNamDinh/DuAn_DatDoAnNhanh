using Microsoft.EntityFrameworkCore;
using DuAn_DoAnNhanh.Data.Configurations;
using DuAn_DoAnNhanh.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DuAn_DoAnNhanh.Data.Enum;

namespace DuAn_DoAnNhanh.Data.EF
{
    public class MyDBContext : DbContext
    {
        public MyDBContext(DbContextOptions options) : base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Configure using Fluent API
            modelBuilder.ApplyConfiguration(new CartConfigurations());
            modelBuilder.ApplyConfiguration(new CartItemConfigurations());
            modelBuilder.ApplyConfiguration(new OrderConfigurations());
            modelBuilder.ApplyConfiguration(new OrderDetailsConfigurations());
            modelBuilder.ApplyConfiguration(new ComboConfigurations());
            modelBuilder.ApplyConfiguration(new ProductComboConfigurations());
            modelBuilder.ApplyConfiguration(new ProductConfigurations());
            modelBuilder.ApplyConfiguration(new AddressConfigurations());
            modelBuilder.ApplyConfiguration(new UserConfigurations());

            modelBuilder.Entity<Product>().HasData(
                new Product
                {
                    ProductID = Guid.Parse("f4a7b7e8-63b6-4c90-a38a-74c5c8d9d7b1"),
                    ProductName = "Burger Siêu Cay",
                    Description = "Mô tả",
                    Quantity = 0,
                    Price = 35000,
                    CreteDate = DateTime.UtcNow,
                    Status = StatusProduct.Activity,
                    ImageUrl = "https://www.lotteria.vn/media/catalog/product/cache/400x400/m/e/menu_burger_2.jpg.webp"
                },
                new Product
                {
                    ProductID = Guid.Parse("9e41d162-3f6a-42a1-b9a6-28f6efbc7f5c"),
                    ProductName = "Burger Bulgogi",
                    Description = "Mô tả",
                    Quantity = 0,
                    Price = 35000,
                    CreteDate = DateTime.UtcNow,
                    Status = StatusProduct.Activity,
                    ImageUrl = "https://www.lotteria.vn/media/catalog/product/cache/400x400/b/u/burger_bulgogi_4.png.webp"
                },
                new Product
                {
                    ProductID = Guid.Parse("2d8f7e1a-5cbb-4ff1-bcbc-f82b07dcb4ad"),
                    ProductName = "Burger Tôm",
                    Description = "Mô tả",
                    Quantity = 0,
                    Price = 35000,
                    CreteDate = DateTime.UtcNow,
                    Status = StatusProduct.Activity,
                    ImageUrl = "https://www.lotteria.vn/media/catalog/product/cache/400x400/b/u/burger_shrimp_1_.png.webp"
                },
                new Product
                {
                    ProductID = Guid.Parse("85c5e5a3-9a3d-4d9a-a09c-74647eb07bfc"),
                    ProductName = "Pepsi Zero (M)",
                    Description = "Mô tả",
                    Quantity = 0,
                    Price = 15000,
                    CreteDate = DateTime.UtcNow,
                    Status = StatusProduct.Activity,
                    ImageUrl = "https://www.lotteria.vn/media/catalog/product/cache/400x400/d/r/drink_pepsi_zero_m_l__2.png.webp"
                },
                new Product
                {
                    ProductID = Guid.Parse("e1bb1ea5-94b2-45c7-98a2-b1fa0f4e3e6d"),
                    ProductName = "Khoai Tây Chiên (M)",
                    Description = "Mô tả",
                    Quantity = 0,
                    Price = 25000,
                    CreteDate = DateTime.UtcNow,
                    Status = StatusProduct.Activity,
                    ImageUrl = "https://www.lotteria.vn/media/catalog/product/cache/400x400/d/e/dessert_french_fries_m_i.png.webp"
                },
                new Product
                {
                    ProductID = Guid.Parse("b487da52-d738-4376-a1e3-c4a4d2fc7ef1"),
                    ProductName = "Khoai lắc tuyết xanh",
                    Description = "Mô tả",
                    Quantity = 0,
                    Price = 25000,
                    CreteDate = DateTime.UtcNow,
                    Status = StatusProduct.Activity,
                    ImageUrl = "https://www.lotteria.vn/media/catalog/product/cache/400x400/d/e/dessert_shake_potato_tuy_t_xanh_.png.webp"
                },
                new Product
                {
                    ProductID = Guid.Parse("f6a71ac8-78f3-4194-88c9-c2aa9467f93e"),
                    ProductName = "Gà Sốt HS (1 miếng)",
                    Description = "Mô tả",
                    Quantity = 0,
                    Price = 41000,
                    CreteDate = DateTime.UtcNow,
                    Status = StatusProduct.Activity,
                    ImageUrl = "https://www.lotteria.vn/media/catalog/product/cache/400x400/l/c/lc0003_1.png.webp"
                }, 
                new Product
                {
                    ProductID = Guid.Parse("dbc17836-d6f4-46cb-bb9a-77b9c54e7b13"),
                    ProductName = "Gà Nướng (1 miếng)",
                    Description = "Mô tả",
                    Quantity = 0,
                    Price = 40000,
                    CreteDate = DateTime.UtcNow,
                    Status = StatusProduct.Activity,
                    ImageUrl = "https://www.lotteria.vn/media/catalog/product/cache/400x400/2/2/227436_2.png.webp"
                },
                new Product
                {
                    ProductID = Guid.Parse("7b17b539-8168-42c5-8b9f-1c1c783bd423"),
                    ProductName = "Gà Rán (1 miếng)",
                    Description = "Mô tả",
                    Quantity = 0,
                    Price = 40000,
                    CreteDate = DateTime.UtcNow,
                    Status = StatusProduct.Activity,
                    ImageUrl = "https://www.lotteria.vn/media/catalog/product/cache/400x400/l/c/lc0001_4.png.webp"
                },
                new Product
                {
                    ProductID = Guid.Parse("c1a8f0ee-73c9-4c2f-b10f-fc3d6561d275"),
                    ProductName = "Gà Sốt Bơ Tỏi (1 miếng)",
                    Description = "Mô tả",
                    Quantity = 0,
                    Price = 41000,
                    CreteDate = DateTime.UtcNow,
                    Status = StatusProduct.Activity,
                    ImageUrl = "https://www.lotteria.vn/media/catalog/product/cache/400x400/m/e/menu_menu_5_.jpg.webp"
                }
                );
            modelBuilder.Entity<Combo>().HasData(
                new Combo
                {
                    ComboID = Guid.Parse("a1b2c3d4-e5f6-4789-abcd-1234567890ab"),
                    ComboName = "Combo Burger Siêu Cay",
                    Image = "https://www.lotteria.vn/media/catalog/product/cache/400x400/m/e/menu_burger_2.jpg.webp",
                    Price = 65000,
                    SetupPrice = 60000,
                    Status = StatusCombo.Activity,
                    Description = "Burger Siêu Cay kèm Khoai Tây Chiên (M) và Pepsi Zero (M)",
                    CreteDate = DateTime.UtcNow
                },
                new Combo
                {
                    ComboID = Guid.Parse("b2c3d4e5-f6a7-4890-abcd-2345678901bc"),
                    ComboName = "Combo Gà Rán",
                    Image = "https://www.lotteria.vn/media/catalog/product/cache/400x400/2/2/222281_4.png.webp",
                    Price = 85000,
                    SetupPrice = null,
                    Status = StatusCombo.Activity,
                    Description = "Gà Rán (1 miếng) kèm Khoai Tây Chiên (M) và Pepsi Zero (M)",
                    CreteDate = DateTime.UtcNow
                },
                new Combo
                {
                    ComboID = Guid.Parse("c3d4e5f6-a7b8-4901-bcde-3456789012cd"),
                    ComboName = "Combo Gà Nướng",
                    Image = "https://www.lotteria.vn/media/catalog/product/cache/400x400/2/2/228380.png.webp",
                    Price = 87000,
                    SetupPrice = 80000,
                    Status = StatusCombo.Activity,
                    Description = "Gà Nướng (1 miếng) kèm Khoai Tây Chiên (M) và Pepsi Zero (M)",
                    CreteDate = DateTime.UtcNow
                }
                );
            modelBuilder.Entity<ProductCombo>().HasData(
                new ProductCombo
                {
                    ProductComboID = Guid.NewGuid(),
                    ProductID = Guid.Parse("f4a7b7e8-63b6-4c90-a38a-74c5c8d9d7b1"), // Burger Siêu Cay
                    ComboID = Guid.Parse("a1b2c3d4-e5f6-4789-abcd-1234567890ab"),
                    Quantity = 1,
                    Status = StatusCombo.Activity
                },
                new ProductCombo
                {
                    ProductComboID = Guid.NewGuid(),
                    ProductID = Guid.Parse("e1bb1ea5-94b2-45c7-98a2-b1fa0f4e3e6d"), // Khoai Tây Chiên (M)
                    ComboID = Guid.Parse("a1b2c3d4-e5f6-4789-abcd-1234567890ab"),
                    Quantity = 1,
                    Status = StatusCombo.Activity
                },
                new ProductCombo
                {
                    ProductComboID = Guid.NewGuid(),
                    ProductID = Guid.Parse("85c5e5a3-9a3d-4d9a-a09c-74647eb07bfc"), // Pepsi Zero (M)
                    ComboID = Guid.Parse("a1b2c3d4-e5f6-4789-abcd-1234567890ab"),
                    Quantity = 1,
                    Status = StatusCombo.Activity
                },

                // Combo Gà Rán
                new ProductCombo
                {
                    ProductComboID = Guid.NewGuid(),
                    ProductID = Guid.Parse("7b17b539-8168-42c5-8b9f-1c1c783bd423"), // Gà Rán (1 miếng)
                    ComboID = Guid.Parse("b2c3d4e5-f6a7-4890-abcd-2345678901bc"),
                    Quantity = 1,
                    Status = StatusCombo.Activity
                },
                new ProductCombo
                {
                    ProductComboID = Guid.NewGuid(),
                    ProductID = Guid.Parse("e1bb1ea5-94b2-45c7-98a2-b1fa0f4e3e6d"), // Khoai Tây Chiên (M)
                    ComboID = Guid.Parse("b2c3d4e5-f6a7-4890-abcd-2345678901bc"),
                    Quantity = 1,
                    Status = StatusCombo.Activity
                },
                new ProductCombo
                {
                    ProductComboID = Guid.NewGuid(),
                    ProductID = Guid.Parse("85c5e5a3-9a3d-4d9a-a09c-74647eb07bfc"), // Pepsi Zero (M)
                    ComboID = Guid.Parse("b2c3d4e5-f6a7-4890-abcd-2345678901bc"),
                    Quantity = 1,
                    Status = StatusCombo.Activity
                },

                // Combo Gà Nướng
                new ProductCombo
                {
                    ProductComboID = Guid.NewGuid(),
                    ProductID = Guid.Parse("dbc17836-d6f4-46cb-bb9a-77b9c54e7b13"), // Gà Nướng (1 miếng)
                    ComboID = Guid.Parse("c3d4e5f6-a7b8-4901-bcde-3456789012cd"),
                    Quantity = 1,
                    Status = StatusCombo.Activity
                },
                new ProductCombo
                {
                    ProductComboID = Guid.NewGuid(),
                    ProductID = Guid.Parse("e1bb1ea5-94b2-45c7-98a2-b1fa0f4e3e6d"), // Khoai Tây Chiên (M)
                    ComboID = Guid.Parse("c3d4e5f6-a7b8-4901-bcde-3456789012cd"),
                    Quantity = 1,
                    Status = StatusCombo.Activity
                },
                new ProductCombo
                {
                    ProductComboID = Guid.NewGuid(),
                    ProductID = Guid.Parse("85c5e5a3-9a3d-4d9a-a09c-74647eb07bfc"), // Pepsi Zero (M)
                    ComboID = Guid.Parse("c3d4e5f6-a7b8-4901-bcde-3456789012cd"),
                    Quantity = 1,
                    Status = StatusCombo.Activity
                }
                );
            modelBuilder.Entity<Store>().HasData(
                new Store
                {
                    StoreID = Guid.Parse("8a2e5d21-5f6b-4a7c-9d5e-3f6c8b2a1d0e"),
                    StoreName="BB Chicken-Hàm Nghi",
                    Status=Status.Activity
                }, 
                new Store
                {
                    StoreID = Guid.Parse("931F07E5-46D8-4449-B77E-533BF4F33AA3"),
                    StoreName = "BB Chicken-Lê Đức Thọ",
                    Status = Status.Activity
                }
                );
            modelBuilder.Entity<Address>().HasData(
                new Address
                {
                    AddressID = Guid.NewGuid(),
                    StoreID = Guid.Parse("8a2e5d21-5f6b-4a7c-9d5e-3f6c8b2a1d0e"),
                    UserID=null,
                    FullName= "BB Chicken-Hàm Nghi",
                    NumberPhone="055931234",
                    Province= "Thành phố Hà Nội",
                    District= "Quận Nam Từ Liêm",
                    Ward="Phường Cầu Diễn",   
                    SpecificAddress="Số 36 Hàm Nghi",
                    FullAddress= "Số 36 Hàm Nghi, Phường Cầu Diễn, Quận Nam Từ Liêm, Thành phố Hà Nội",
                    Latitude= 21.02983,
                    Longitude= 105.76913,
                    AddressType=AddressType.Store,
                    CreateDate=DateTime.Now,
                    Status = Status.Activity
                }, 
                new Address
                {
                    AddressID = Guid.NewGuid(),
                    StoreID = Guid.Parse("931F07E5-46D8-4449-B77E-533BF4F33AA3"),
                    UserID = null,
                    FullName = "BB Chicken-Lê Đức Thọ",
                    NumberPhone = "0828277707",
                    Province = "Thành phố Hà Nội",
                    District = "Quận Nam Từ Liêm",
                    Ward = "Phường Mỹ Đình 2",
                    SpecificAddress = "Số 7 Lê Đức Thọ",
                    FullAddress = "Số 7 Lê Đức Thọ, Phường Mỹ Đình 2, Quận Nam Từ Liêm, Thành phố Hà Nội",
                    Latitude = 21.02983,
                    Longitude = 105.76913,
                    AddressType = AddressType.Store,
                    CreateDate = DateTime.Now,
                    Status = Status.Activity
                }
                );

            modelBuilder.Entity<User>().HasData(
                new User
                {
                    UserID = Guid.NewGuid(),
                    StoreID = null,
                    FullName="Admin",
                    Password="admin",
                    Email="admin@gmail.com",
                    CreateDate=DateTime.Now,
                    Role=Role.Admin,                 
                    Status = Status.Activity
                },
                new User
                {
                    UserID = Guid.NewGuid(),
                    StoreID = Guid.Parse("931F07E5-46D8-4449-B77E-533BF4F33AA3"),
                    FullName = "Phạm Viết Manh",
                    Password = "manhdb123",
                    Email = "manhdb123@gmail.com",
                    CreateDate = DateTime.Now,
                    Role = Role.Manager,
                    Status = Status.Activity
                },
                new User
                {
                    UserID = Guid.NewGuid(),
                    StoreID = Guid.Parse("8a2e5d21-5f6b-4a7c-9d5e-3f6c8b2a1d0e"),
                    FullName = "Nguyễn Phương Linh",
                    Password = "linhdb123",
                    Email = "linhdb123@gmail.com",
                    CreateDate = DateTime.Now,
                    Role = Role.Manager,
                    Status = Status.Activity
                }
                );
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer();
            }
        }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<CartItem> CartItems { get; set; }
        public DbSet<Bill> Bill { get; set; }
        public DbSet<BillDetails> BillDetailses { get; set; }
        public DbSet<Combo> Combos { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductCombo> productCombos { get; set; }
        public DbSet<Address> addresses { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Store> Stores { get; set; }



    }
}
