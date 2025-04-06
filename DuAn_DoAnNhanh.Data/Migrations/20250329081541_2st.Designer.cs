﻿// <auto-generated />
using System;
using DuAn_DoAnNhanh.Data.EF;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace DuAn_DoAnNhanh.Data.Migrations
{
    [DbContext(typeof(MyDBContext))]
    [Migration("20250329081541_2st")]
    partial class _2st
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.12")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("DuAn_DoAnNhanh.Data.Entities.Address", b =>
                {
                    b.Property<Guid>("AddressID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int?>("AddressType")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreateDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("District")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FullAddress")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FullName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("Latitude")
                        .HasColumnType("float");

                    b.Property<double>("Longitude")
                        .HasColumnType("float");

                    b.Property<string>("NumberPhone")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Province")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SpecificAddress")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<Guid?>("StoreID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("UserID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Ward")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("AddressID");

                    b.HasIndex("StoreID")
                        .IsUnique()
                        .HasFilter("[StoreID] IS NOT NULL");

                    b.HasIndex("UserID");

                    b.ToTable("Address", (string)null);

                    b.HasData(
                        new
                        {
                            AddressID = new Guid("a137e13f-5b4b-4a98-b229-0e7341846b28"),
                            AddressType = 2,
                            CreateDate = new DateTime(2025, 3, 29, 15, 15, 40, 296, DateTimeKind.Local).AddTicks(8400),
                            District = "Quận Nam Từ Liêm",
                            FullAddress = "Số 36 Hàm Nghi, Phường Cầu Diễn, Quận Nam Từ Liêm, Thành phố Hà Nội",
                            FullName = "BB Chicken-Hàm Nghi",
                            Latitude = 21.02983,
                            Longitude = 105.76913,
                            NumberPhone = "055931234",
                            Province = "Thành phố Hà Nội",
                            SpecificAddress = "Số 36 Hàm Nghi",
                            Status = 0,
                            StoreID = new Guid("8a2e5d21-5f6b-4a7c-9d5e-3f6c8b2a1d0e"),
                            Ward = "Phường Cầu Diễn"
                        },
                        new
                        {
                            AddressID = new Guid("8c8554dd-c872-4847-a811-fc29416ac68e"),
                            AddressType = 2,
                            CreateDate = new DateTime(2025, 3, 29, 15, 15, 40, 296, DateTimeKind.Local).AddTicks(8424),
                            District = "Quận Nam Từ Liêm",
                            FullAddress = "Số 7 Lê Đức Thọ, Phường Mỹ Đình 2, Quận Nam Từ Liêm, Thành phố Hà Nội",
                            FullName = "BB Chicken-Lê Đức Thọ",
                            Latitude = 21.02983,
                            Longitude = 105.76913,
                            NumberPhone = "0828277707",
                            Province = "Thành phố Hà Nội",
                            SpecificAddress = "Số 7 Lê Đức Thọ",
                            Status = 0,
                            StoreID = new Guid("931f07e5-46d8-4449-b77e-533bf4f33aa3"),
                            Ward = "Phường Mỹ Đình 2"
                        });
                });

            modelBuilder.Entity("DuAn_DoAnNhanh.Data.Entities.Bill", b =>
                {
                    b.Property<Guid>("BillID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("AddressID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("BillDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("PaymentType")
                        .HasColumnType("int");

                    b.Property<int>("ReceivingType")
                        .HasColumnType("int");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<Guid?>("StoreID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<decimal>("TotalAmount")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("TotalAmountEndow")
                        .HasColumnType("decimal(18,2)");

                    b.Property<Guid>("UserID")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("BillID");

                    b.HasIndex("AddressID");

                    b.HasIndex("StoreID");

                    b.HasIndex("UserID");

                    b.ToTable("Bill", (string)null);
                });

            modelBuilder.Entity("DuAn_DoAnNhanh.Data.Entities.BillDetails", b =>
                {
                    b.Property<Guid>("BillDetailsID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("BillID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Image")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PCName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("PriceEndow")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.HasKey("BillDetailsID");

                    b.HasIndex("BillID");

                    b.ToTable("BillDetails", (string)null);
                });

            modelBuilder.Entity("DuAn_DoAnNhanh.Data.Entities.Cart", b =>
                {
                    b.Property<Guid>("CartID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("UserID")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("CartID");

                    b.HasIndex("UserID")
                        .IsUnique();

                    b.ToTable("Carts", (string)null);
                });

            modelBuilder.Entity("DuAn_DoAnNhanh.Data.Entities.CartItem", b =>
                {
                    b.Property<Guid>("CartItemID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("CartID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("ComboID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("ProductID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.HasKey("CartItemID");

                    b.HasIndex("CartID");

                    b.HasIndex("ComboID");

                    b.HasIndex("ProductID");

                    b.ToTable("CartItems", (string)null);
                });

            modelBuilder.Entity("DuAn_DoAnNhanh.Data.Entities.Combo", b =>
                {
                    b.Property<Guid>("ComboID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("ComboName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreteDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Image")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal?>("SetupPrice")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.HasKey("ComboID");

                    b.ToTable("Combos", (string)null);

                    b.HasData(
                        new
                        {
                            ComboID = new Guid("a1b2c3d4-e5f6-4789-abcd-1234567890ab"),
                            ComboName = "Combo Burger Siêu Cay",
                            CreteDate = new DateTime(2025, 3, 29, 8, 15, 40, 296, DateTimeKind.Utc).AddTicks(8072),
                            Description = "Burger Siêu Cay kèm Khoai Tây Chiên (M) và Pepsi Zero (M)",
                            Image = "https://www.lotteria.vn/media/catalog/product/cache/400x400/m/e/menu_burger_2.jpg.webp",
                            Price = 65000m,
                            SetupPrice = 60000m,
                            Status = 0
                        },
                        new
                        {
                            ComboID = new Guid("b2c3d4e5-f6a7-4890-abcd-2345678901bc"),
                            ComboName = "Combo Gà Rán",
                            CreteDate = new DateTime(2025, 3, 29, 8, 15, 40, 296, DateTimeKind.Utc).AddTicks(8080),
                            Description = "Gà Rán (1 miếng) kèm Khoai Tây Chiên (M) và Pepsi Zero (M)",
                            Image = "https://www.lotteria.vn/media/catalog/product/cache/400x400/2/2/222281_4.png.webp",
                            Price = 85000m,
                            Status = 0
                        },
                        new
                        {
                            ComboID = new Guid("c3d4e5f6-a7b8-4901-bcde-3456789012cd"),
                            ComboName = "Combo Gà Nướng",
                            CreteDate = new DateTime(2025, 3, 29, 8, 15, 40, 296, DateTimeKind.Utc).AddTicks(8087),
                            Description = "Gà Nướng (1 miếng) kèm Khoai Tây Chiên (M) và Pepsi Zero (M)",
                            Image = "https://www.lotteria.vn/media/catalog/product/cache/400x400/2/2/228380.png.webp",
                            Price = 87000m,
                            SetupPrice = 80000m,
                            Status = 0
                        });
                });

            modelBuilder.Entity("DuAn_DoAnNhanh.Data.Entities.Product", b =>
                {
                    b.Property<Guid>("ProductID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreteDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ImageUrl")
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("ProductName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.HasKey("ProductID");

                    b.ToTable("Products", (string)null);

                    b.HasData(
                        new
                        {
                            ProductID = new Guid("f4a7b7e8-63b6-4c90-a38a-74c5c8d9d7b1"),
                            CreteDate = new DateTime(2025, 3, 29, 8, 15, 40, 296, DateTimeKind.Utc).AddTicks(7488),
                            Description = "Mô tả",
                            ImageUrl = "https://www.lotteria.vn/media/catalog/product/cache/400x400/m/e/menu_burger_2.jpg.webp",
                            Price = 35000m,
                            ProductName = "Burger Siêu Cay",
                            Quantity = 0,
                            Status = 0
                        },
                        new
                        {
                            ProductID = new Guid("9e41d162-3f6a-42a1-b9a6-28f6efbc7f5c"),
                            CreteDate = new DateTime(2025, 3, 29, 8, 15, 40, 296, DateTimeKind.Utc).AddTicks(7498),
                            Description = "Mô tả",
                            ImageUrl = "https://www.lotteria.vn/media/catalog/product/cache/400x400/b/u/burger_bulgogi_4.png.webp",
                            Price = 35000m,
                            ProductName = "Burger Bulgogi",
                            Quantity = 0,
                            Status = 0
                        },
                        new
                        {
                            ProductID = new Guid("2d8f7e1a-5cbb-4ff1-bcbc-f82b07dcb4ad"),
                            CreteDate = new DateTime(2025, 3, 29, 8, 15, 40, 296, DateTimeKind.Utc).AddTicks(7504),
                            Description = "Mô tả",
                            ImageUrl = "https://www.lotteria.vn/media/catalog/product/cache/400x400/b/u/burger_shrimp_1_.png.webp",
                            Price = 35000m,
                            ProductName = "Burger Tôm",
                            Quantity = 0,
                            Status = 0
                        },
                        new
                        {
                            ProductID = new Guid("85c5e5a3-9a3d-4d9a-a09c-74647eb07bfc"),
                            CreteDate = new DateTime(2025, 3, 29, 8, 15, 40, 296, DateTimeKind.Utc).AddTicks(7510),
                            Description = "Mô tả",
                            ImageUrl = "https://www.lotteria.vn/media/catalog/product/cache/400x400/d/r/drink_pepsi_zero_m_l__2.png.webp",
                            Price = 15000m,
                            ProductName = "Pepsi Zero (M)",
                            Quantity = 0,
                            Status = 0
                        },
                        new
                        {
                            ProductID = new Guid("e1bb1ea5-94b2-45c7-98a2-b1fa0f4e3e6d"),
                            CreteDate = new DateTime(2025, 3, 29, 8, 15, 40, 296, DateTimeKind.Utc).AddTicks(7515),
                            Description = "Mô tả",
                            ImageUrl = "https://www.lotteria.vn/media/catalog/product/cache/400x400/d/e/dessert_french_fries_m_i.png.webp",
                            Price = 25000m,
                            ProductName = "Khoai Tây Chiên (M)",
                            Quantity = 0,
                            Status = 0
                        },
                        new
                        {
                            ProductID = new Guid("b487da52-d738-4376-a1e3-c4a4d2fc7ef1"),
                            CreteDate = new DateTime(2025, 3, 29, 8, 15, 40, 296, DateTimeKind.Utc).AddTicks(7522),
                            Description = "Mô tả",
                            ImageUrl = "https://www.lotteria.vn/media/catalog/product/cache/400x400/d/e/dessert_shake_potato_tuy_t_xanh_.png.webp",
                            Price = 25000m,
                            ProductName = "Khoai lắc tuyết xanh",
                            Quantity = 0,
                            Status = 0
                        },
                        new
                        {
                            ProductID = new Guid("f6a71ac8-78f3-4194-88c9-c2aa9467f93e"),
                            CreteDate = new DateTime(2025, 3, 29, 8, 15, 40, 296, DateTimeKind.Utc).AddTicks(7527),
                            Description = "Mô tả",
                            ImageUrl = "https://www.lotteria.vn/media/catalog/product/cache/400x400/l/c/lc0003_1.png.webp",
                            Price = 41000m,
                            ProductName = "Gà Sốt HS (1 miếng)",
                            Quantity = 0,
                            Status = 0
                        },
                        new
                        {
                            ProductID = new Guid("dbc17836-d6f4-46cb-bb9a-77b9c54e7b13"),
                            CreteDate = new DateTime(2025, 3, 29, 8, 15, 40, 296, DateTimeKind.Utc).AddTicks(7533),
                            Description = "Mô tả",
                            ImageUrl = "https://www.lotteria.vn/media/catalog/product/cache/400x400/2/2/227436_2.png.webp",
                            Price = 40000m,
                            ProductName = "Gà Nướng (1 miếng)",
                            Quantity = 0,
                            Status = 0
                        },
                        new
                        {
                            ProductID = new Guid("7b17b539-8168-42c5-8b9f-1c1c783bd423"),
                            CreteDate = new DateTime(2025, 3, 29, 8, 15, 40, 296, DateTimeKind.Utc).AddTicks(7538),
                            Description = "Mô tả",
                            ImageUrl = "https://www.lotteria.vn/media/catalog/product/cache/400x400/l/c/lc0001_4.png.webp",
                            Price = 40000m,
                            ProductName = "Gà Rán (1 miếng)",
                            Quantity = 0,
                            Status = 0
                        },
                        new
                        {
                            ProductID = new Guid("c1a8f0ee-73c9-4c2f-b10f-fc3d6561d275"),
                            CreteDate = new DateTime(2025, 3, 29, 8, 15, 40, 296, DateTimeKind.Utc).AddTicks(7543),
                            Description = "Mô tả",
                            ImageUrl = "https://www.lotteria.vn/media/catalog/product/cache/400x400/m/e/menu_menu_5_.jpg.webp",
                            Price = 41000m,
                            ProductName = "Gà Sốt Bơ Tỏi (1 miếng)",
                            Quantity = 0,
                            Status = 0
                        });
                });

            modelBuilder.Entity("DuAn_DoAnNhanh.Data.Entities.ProductCombo", b =>
                {
                    b.Property<Guid>("ProductComboID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("ComboID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("ProductID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.HasKey("ProductComboID");

                    b.HasIndex("ComboID");

                    b.HasIndex("ProductID");

                    b.ToTable("ProductCombos", (string)null);

                    b.HasData(
                        new
                        {
                            ProductComboID = new Guid("30169240-1ef4-4cab-8c82-9feb995b52dc"),
                            ComboID = new Guid("a1b2c3d4-e5f6-4789-abcd-1234567890ab"),
                            ProductID = new Guid("f4a7b7e8-63b6-4c90-a38a-74c5c8d9d7b1"),
                            Quantity = 1,
                            Status = 0
                        },
                        new
                        {
                            ProductComboID = new Guid("74c74a7b-2a74-4524-a48b-7493de78a8cb"),
                            ComboID = new Guid("a1b2c3d4-e5f6-4789-abcd-1234567890ab"),
                            ProductID = new Guid("e1bb1ea5-94b2-45c7-98a2-b1fa0f4e3e6d"),
                            Quantity = 1,
                            Status = 0
                        },
                        new
                        {
                            ProductComboID = new Guid("3ed69b72-c2de-4d09-806c-e865adfdf2b9"),
                            ComboID = new Guid("a1b2c3d4-e5f6-4789-abcd-1234567890ab"),
                            ProductID = new Guid("85c5e5a3-9a3d-4d9a-a09c-74647eb07bfc"),
                            Quantity = 1,
                            Status = 0
                        },
                        new
                        {
                            ProductComboID = new Guid("cf88ce7e-80b7-47a2-ae29-42df7b2773dc"),
                            ComboID = new Guid("b2c3d4e5-f6a7-4890-abcd-2345678901bc"),
                            ProductID = new Guid("7b17b539-8168-42c5-8b9f-1c1c783bd423"),
                            Quantity = 1,
                            Status = 0
                        },
                        new
                        {
                            ProductComboID = new Guid("9041c541-891b-47a7-b065-ecd76084da6f"),
                            ComboID = new Guid("b2c3d4e5-f6a7-4890-abcd-2345678901bc"),
                            ProductID = new Guid("e1bb1ea5-94b2-45c7-98a2-b1fa0f4e3e6d"),
                            Quantity = 1,
                            Status = 0
                        },
                        new
                        {
                            ProductComboID = new Guid("27ddb993-2752-4359-bc02-c18ae57df6ae"),
                            ComboID = new Guid("b2c3d4e5-f6a7-4890-abcd-2345678901bc"),
                            ProductID = new Guid("85c5e5a3-9a3d-4d9a-a09c-74647eb07bfc"),
                            Quantity = 1,
                            Status = 0
                        },
                        new
                        {
                            ProductComboID = new Guid("434ce794-1ca0-44b4-b234-020b4da58a22"),
                            ComboID = new Guid("c3d4e5f6-a7b8-4901-bcde-3456789012cd"),
                            ProductID = new Guid("dbc17836-d6f4-46cb-bb9a-77b9c54e7b13"),
                            Quantity = 1,
                            Status = 0
                        },
                        new
                        {
                            ProductComboID = new Guid("7765f52f-08b8-437f-b16e-c36fa0145f3d"),
                            ComboID = new Guid("c3d4e5f6-a7b8-4901-bcde-3456789012cd"),
                            ProductID = new Guid("e1bb1ea5-94b2-45c7-98a2-b1fa0f4e3e6d"),
                            Quantity = 1,
                            Status = 0
                        },
                        new
                        {
                            ProductComboID = new Guid("9cccfa2d-2eda-46d7-b162-20f8e2ef4a17"),
                            ComboID = new Guid("c3d4e5f6-a7b8-4901-bcde-3456789012cd"),
                            ProductID = new Guid("85c5e5a3-9a3d-4d9a-a09c-74647eb07bfc"),
                            Quantity = 1,
                            Status = 0
                        });
                });

            modelBuilder.Entity("DuAn_DoAnNhanh.Data.Entities.Store", b =>
                {
                    b.Property<Guid>("StoreID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreateDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<string>("StoreName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("StoreID");

                    b.ToTable("Stores");

                    b.HasData(
                        new
                        {
                            StoreID = new Guid("8a2e5d21-5f6b-4a7c-9d5e-3f6c8b2a1d0e"),
                            CreateDate = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Status = 0,
                            StoreName = "BB Chicken-Hàm Nghi"
                        },
                        new
                        {
                            StoreID = new Guid("931f07e5-46d8-4449-b77e-533bf4f33aa3"),
                            CreateDate = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Status = 0,
                            StoreName = "BB Chicken-Lê Đức Thọ"
                        });
                });

            modelBuilder.Entity("DuAn_DoAnNhanh.Data.Entities.User", b =>
                {
                    b.Property<Guid>("UserID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreateDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FullName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Role")
                        .HasColumnType("int");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<Guid?>("StoreID")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("UserID");

                    b.HasIndex("StoreID");

                    b.ToTable("Users", (string)null);

                    b.HasData(
                        new
                        {
                            UserID = new Guid("10bcba6c-46b1-4233-89d3-877c120a471f"),
                            CreateDate = new DateTime(2025, 3, 29, 15, 15, 40, 296, DateTimeKind.Local).AddTicks(8480),
                            Email = "admin@gmail.com",
                            FullName = "Admin",
                            Password = "admin",
                            Role = 0,
                            Status = 0
                        },
                        new
                        {
                            UserID = new Guid("ac5b3bbf-8d67-4143-af3b-b03b9db4de14"),
                            CreateDate = new DateTime(2025, 3, 29, 15, 15, 40, 296, DateTimeKind.Local).AddTicks(8489),
                            Email = "manhdb123@gmail.com",
                            FullName = "Phạm Viết Manh",
                            Password = "manhdb123",
                            Role = 1,
                            Status = 0,
                            StoreID = new Guid("931f07e5-46d8-4449-b77e-533bf4f33aa3")
                        },
                        new
                        {
                            UserID = new Guid("411f4d61-d4a7-4eec-b584-308a4f71e9e7"),
                            CreateDate = new DateTime(2025, 3, 29, 15, 15, 40, 296, DateTimeKind.Local).AddTicks(8497),
                            Email = "linhdb123@gmail.com",
                            FullName = "Nguyễn Phương Linh",
                            Password = "linhdb123",
                            Role = 1,
                            Status = 0,
                            StoreID = new Guid("8a2e5d21-5f6b-4a7c-9d5e-3f6c8b2a1d0e")
                        });
                });

            modelBuilder.Entity("DuAn_DoAnNhanh.Data.Entities.Address", b =>
                {
                    b.HasOne("DuAn_DoAnNhanh.Data.Entities.Store", "Store")
                        .WithOne("Address")
                        .HasForeignKey("DuAn_DoAnNhanh.Data.Entities.Address", "StoreID");

                    b.HasOne("DuAn_DoAnNhanh.Data.Entities.User", "User")
                        .WithMany("Addresses")
                        .HasForeignKey("UserID");

                    b.Navigation("Store");

                    b.Navigation("User");
                });

            modelBuilder.Entity("DuAn_DoAnNhanh.Data.Entities.Bill", b =>
                {
                    b.HasOne("DuAn_DoAnNhanh.Data.Entities.Address", "Address")
                        .WithMany("Bills")
                        .HasForeignKey("AddressID");

                    b.HasOne("DuAn_DoAnNhanh.Data.Entities.Store", "Store")
                        .WithMany("Bills")
                        .HasForeignKey("StoreID");

                    b.HasOne("DuAn_DoAnNhanh.Data.Entities.User", "User")
                        .WithMany("Orderes")
                        .HasForeignKey("UserID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Address");

                    b.Navigation("Store");

                    b.Navigation("User");
                });

            modelBuilder.Entity("DuAn_DoAnNhanh.Data.Entities.BillDetails", b =>
                {
                    b.HasOne("DuAn_DoAnNhanh.Data.Entities.Bill", "Order")
                        .WithMany("BillDetails")
                        .HasForeignKey("BillID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Order");
                });

            modelBuilder.Entity("DuAn_DoAnNhanh.Data.Entities.Cart", b =>
                {
                    b.HasOne("DuAn_DoAnNhanh.Data.Entities.User", "User")
                        .WithOne("Cart")
                        .HasForeignKey("DuAn_DoAnNhanh.Data.Entities.Cart", "UserID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("DuAn_DoAnNhanh.Data.Entities.CartItem", b =>
                {
                    b.HasOne("DuAn_DoAnNhanh.Data.Entities.Cart", "Cart")
                        .WithMany("CartItems")
                        .HasForeignKey("CartID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DuAn_DoAnNhanh.Data.Entities.Combo", "Combo")
                        .WithMany("CartItemes")
                        .HasForeignKey("ComboID");

                    b.HasOne("DuAn_DoAnNhanh.Data.Entities.Product", "Product")
                        .WithMany("CartItemes")
                        .HasForeignKey("ProductID");

                    b.Navigation("Cart");

                    b.Navigation("Combo");

                    b.Navigation("Product");
                });

            modelBuilder.Entity("DuAn_DoAnNhanh.Data.Entities.ProductCombo", b =>
                {
                    b.HasOne("DuAn_DoAnNhanh.Data.Entities.Combo", "Combo")
                        .WithMany("ProductComboes")
                        .HasForeignKey("ComboID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DuAn_DoAnNhanh.Data.Entities.Product", "Product")
                        .WithMany("ProductComboes")
                        .HasForeignKey("ProductID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Combo");

                    b.Navigation("Product");
                });

            modelBuilder.Entity("DuAn_DoAnNhanh.Data.Entities.User", b =>
                {
                    b.HasOne("DuAn_DoAnNhanh.Data.Entities.Store", "Store")
                        .WithMany("Users")
                        .HasForeignKey("StoreID");

                    b.Navigation("Store");
                });

            modelBuilder.Entity("DuAn_DoAnNhanh.Data.Entities.Address", b =>
                {
                    b.Navigation("Bills");
                });

            modelBuilder.Entity("DuAn_DoAnNhanh.Data.Entities.Bill", b =>
                {
                    b.Navigation("BillDetails");
                });

            modelBuilder.Entity("DuAn_DoAnNhanh.Data.Entities.Cart", b =>
                {
                    b.Navigation("CartItems");
                });

            modelBuilder.Entity("DuAn_DoAnNhanh.Data.Entities.Combo", b =>
                {
                    b.Navigation("CartItemes");

                    b.Navigation("ProductComboes");
                });

            modelBuilder.Entity("DuAn_DoAnNhanh.Data.Entities.Product", b =>
                {
                    b.Navigation("CartItemes");

                    b.Navigation("ProductComboes");
                });

            modelBuilder.Entity("DuAn_DoAnNhanh.Data.Entities.Store", b =>
                {
                    b.Navigation("Address")
                        .IsRequired();

                    b.Navigation("Bills");

                    b.Navigation("Users");
                });

            modelBuilder.Entity("DuAn_DoAnNhanh.Data.Entities.User", b =>
                {
                    b.Navigation("Addresses");

                    b.Navigation("Cart")
                        .IsRequired();

                    b.Navigation("Orderes");
                });
#pragma warning restore 612, 618
        }
    }
}
