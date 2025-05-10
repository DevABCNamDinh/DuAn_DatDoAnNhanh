using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DuAn_DoAnNhanh.Data.Migrations
{
    /// <inheritdoc />
    public partial class database : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Combos",
                columns: table => new
                {
                    ComboID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ComboName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Image = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    SetupPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreteDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Combos", x => x.ComboID);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    ProductID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProductName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CreteDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.ProductID);
                });

            migrationBuilder.CreateTable(
                name: "ProductCombos",
                columns: table => new
                {
                    ProductComboID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProductID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ComboID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductCombos", x => x.ProductComboID);
                    table.ForeignKey(
                        name: "FK_ProductCombos_Combos_ComboID",
                        column: x => x.ComboID,
                        principalTable: "Combos",
                        principalColumn: "ComboID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductCombos_Products_ProductID",
                        column: x => x.ProductID,
                        principalTable: "Products",
                        principalColumn: "ProductID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Address",
                columns: table => new
                {
                    AddressID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    FullName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    NumberPhone = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Province = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    District = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Ward = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SpecificAddress = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Latitude = table.Column<double>(type: "float", nullable: false),
                    Longitude = table.Column<double>(type: "float", nullable: false),
                    FullAddress = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AddressType = table.Column<int>(type: "int", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Address", x => x.AddressID);
                });

            migrationBuilder.CreateTable(
                name: "Stores",
                columns: table => new
                {
                    StoreID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StoreName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AddressID = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Stores", x => x.StoreID);
                    table.ForeignKey(
                        name: "FK_Stores_Address_AddressID",
                        column: x => x.AddressID,
                        principalTable: "Address",
                        principalColumn: "AddressID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StoreID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    FullName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    Role = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserID);
                    table.ForeignKey(
                        name: "FK_Users_Stores_StoreID",
                        column: x => x.StoreID,
                        principalTable: "Stores",
                        principalColumn: "StoreID");
                });

            migrationBuilder.CreateTable(
                name: "Bill",
                columns: table => new
                {
                    BillID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AddressID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    StoreID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    BillDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    TotalAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TotalAmountEndow = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ReceivingType = table.Column<int>(type: "int", nullable: false),
                    PaymentType = table.Column<int>(type: "int", nullable: false),
                    ReceiverName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ReceiverPhone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bill", x => x.BillID);
                    table.ForeignKey(
                        name: "FK_Bill_Address_AddressID",
                        column: x => x.AddressID,
                        principalTable: "Address",
                        principalColumn: "AddressID");
                    table.ForeignKey(
                        name: "FK_Bill_Stores_StoreID",
                        column: x => x.StoreID,
                        principalTable: "Stores",
                        principalColumn: "StoreID");
                    table.ForeignKey(
                        name: "FK_Bill_Users_UserID",
                        column: x => x.UserID,
                        principalTable: "Users",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Carts",
                columns: table => new
                {
                    CartID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserID = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Carts", x => x.CartID);
                    table.ForeignKey(
                        name: "FK_Carts_Users_UserID",
                        column: x => x.UserID,
                        principalTable: "Users",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BillDetails",
                columns: table => new
                {
                    BillDetailsID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BillID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PCName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Image = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PriceEndow = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BillDetails", x => x.BillDetailsID);
                    table.ForeignKey(
                        name: "FK_BillDetails_Bill_BillID",
                        column: x => x.BillID,
                        principalTable: "Bill",
                        principalColumn: "BillID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CartItems",
                columns: table => new
                {
                    CartItemID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProductID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ComboID = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CartID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CartItems", x => x.CartItemID);
                    table.ForeignKey(
                        name: "FK_CartItems_Carts_CartID",
                        column: x => x.CartID,
                        principalTable: "Carts",
                        principalColumn: "CartID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CartItems_Combos_ComboID",
                        column: x => x.ComboID,
                        principalTable: "Combos",
                        principalColumn: "ComboID");
                    table.ForeignKey(
                        name: "FK_CartItems_Products_ProductID",
                        column: x => x.ProductID,
                        principalTable: "Products",
                        principalColumn: "ProductID");
                });

            migrationBuilder.InsertData(
                table: "Address",
                columns: new[] { "AddressID", "AddressType", "CreateDate", "Description", "District", "FullAddress", "FullName", "Latitude", "Longitude", "NumberPhone", "Province", "SpecificAddress", "Status", "UserID", "Ward" },
                values: new object[] { new Guid("931f07e5-46d8-4449-b77e-533bf4f33aa3"), 2, new DateTime(2025, 5, 10, 8, 46, 48, 869, DateTimeKind.Local).AddTicks(1868), null, "Quận Nam Từ Liêm", "Số 36 Hàm Nghi, Phường Cầu Diễn, Quận Nam Từ Liêm, Thành phố Hà Nội", "BB Chicken-Hàm Nghi", 21.02983, 105.76913, "055931234", "Thành phố Hà Nội", "Số 36 Hàm Nghi", 0, null, "Phường Cầu Diễn" });

            migrationBuilder.InsertData(
                table: "Combos",
                columns: new[] { "ComboID", "ComboName", "CreteDate", "Description", "Image", "Price", "SetupPrice", "Status" },
                values: new object[,]
                {
                    { new Guid("a1b2c3d4-e5f6-4789-abcd-1234567890ab"), "Combo Burger Siêu Cay", new DateTime(2025, 5, 10, 1, 46, 48, 869, DateTimeKind.Utc).AddTicks(1656), "Burger Siêu Cay kèm Khoai Tây Chiên (M) và Pepsi Zero (M)", "https://www.lotteria.vn/media/catalog/product/cache/400x400/m/e/menu_burger_2.jpg.webp", 65000m, 60000m, 0 },
                    { new Guid("b2c3d4e5-f6a7-4890-abcd-2345678901bc"), "Combo Gà Rán", new DateTime(2025, 5, 10, 1, 46, 48, 869, DateTimeKind.Utc).AddTicks(1661), "Gà Rán (1 miếng) kèm Khoai Tây Chiên (M) và Pepsi Zero (M)", "https://www.lotteria.vn/media/catalog/product/cache/400x400/2/2/222281_4.png.webp", 85000m, null, 0 },
                    { new Guid("c3d4e5f6-a7b8-4901-bcde-3456789012cd"), "Combo Gà Nướng", new DateTime(2025, 5, 10, 1, 46, 48, 869, DateTimeKind.Utc).AddTicks(1664), "Gà Nướng (1 miếng) kèm Khoai Tây Chiên (M) và Pepsi Zero (M)", "https://www.lotteria.vn/media/catalog/product/cache/400x400/2/2/228380.png.webp", 87000m, 80000m, 0 }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "ProductID", "CreteDate", "Description", "ImageUrl", "Price", "ProductName", "Quantity", "Status" },
                values: new object[,]
                {
                    { new Guid("2d8f7e1a-5cbb-4ff1-bcbc-f82b07dcb4ad"), new DateTime(2025, 5, 10, 1, 46, 48, 869, DateTimeKind.Utc).AddTicks(1413), "Mô tả", "https://www.lotteria.vn/media/catalog/product/cache/400x400/b/u/burger_shrimp_1_.png.webp", 35000m, "Burger Tôm", 0, 0 },
                    { new Guid("7b17b539-8168-42c5-8b9f-1c1c783bd423"), new DateTime(2025, 5, 10, 1, 46, 48, 869, DateTimeKind.Utc).AddTicks(1433), "Mô tả", "https://www.lotteria.vn/media/catalog/product/cache/400x400/l/c/lc0001_4.png.webp", 40000m, "Gà Rán (1 miếng)", 0, 0 },
                    { new Guid("85c5e5a3-9a3d-4d9a-a09c-74647eb07bfc"), new DateTime(2025, 5, 10, 1, 46, 48, 869, DateTimeKind.Utc).AddTicks(1416), "Mô tả", "https://www.lotteria.vn/media/catalog/product/cache/400x400/d/r/drink_pepsi_zero_m_l__2.png.webp", 15000m, "Pepsi Zero (M)", 0, 0 },
                    { new Guid("9e41d162-3f6a-42a1-b9a6-28f6efbc7f5c"), new DateTime(2025, 5, 10, 1, 46, 48, 869, DateTimeKind.Utc).AddTicks(1409), "Mô tả", "https://www.lotteria.vn/media/catalog/product/cache/400x400/b/u/burger_bulgogi_4.png.webp", 35000m, "Burger Bulgogi", 0, 0 },
                    { new Guid("b487da52-d738-4376-a1e3-c4a4d2fc7ef1"), new DateTime(2025, 5, 10, 1, 46, 48, 869, DateTimeKind.Utc).AddTicks(1424), "Mô tả", "https://www.lotteria.vn/media/catalog/product/cache/400x400/d/e/dessert_shake_potato_tuy_t_xanh_.png.webp", 25000m, "Khoai lắc tuyết xanh", 0, 0 },
                    { new Guid("c1a8f0ee-73c9-4c2f-b10f-fc3d6561d275"), new DateTime(2025, 5, 10, 1, 46, 48, 869, DateTimeKind.Utc).AddTicks(1435), "Mô tả", "https://www.lotteria.vn/media/catalog/product/cache/400x400/m/e/menu_menu_5_.jpg.webp", 41000m, "Gà Sốt Bơ Tỏi (1 miếng)", 0, 0 },
                    { new Guid("dbc17836-d6f4-46cb-bb9a-77b9c54e7b13"), new DateTime(2025, 5, 10, 1, 46, 48, 869, DateTimeKind.Utc).AddTicks(1428), "Mô tả", "https://www.lotteria.vn/media/catalog/product/cache/400x400/2/2/227436_2.png.webp", 40000m, "Gà Nướng (1 miếng)", 0, 0 },
                    { new Guid("e1bb1ea5-94b2-45c7-98a2-b1fa0f4e3e6d"), new DateTime(2025, 5, 10, 1, 46, 48, 869, DateTimeKind.Utc).AddTicks(1419), "Mô tả", "https://www.lotteria.vn/media/catalog/product/cache/400x400/d/e/dessert_french_fries_m_i.png.webp", 25000m, "Khoai Tây Chiên (M)", 0, 0 },
                    { new Guid("f4a7b7e8-63b6-4c90-a38a-74c5c8d9d7b1"), new DateTime(2025, 5, 10, 1, 46, 48, 869, DateTimeKind.Utc).AddTicks(1403), "Mô tả", "https://www.lotteria.vn/media/catalog/product/cache/400x400/m/e/menu_burger_2.jpg.webp", 35000m, "Burger Siêu Cay", 0, 0 },
                    { new Guid("f6a71ac8-78f3-4194-88c9-c2aa9467f93e"), new DateTime(2025, 5, 10, 1, 46, 48, 869, DateTimeKind.Utc).AddTicks(1426), "Mô tả", "https://www.lotteria.vn/media/catalog/product/cache/400x400/l/c/lc0003_1.png.webp", 41000m, "Gà Sốt HS (1 miếng)", 0, 0 }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserID", "CreateDate", "Email", "FullName", "Password", "Role", "Status", "StoreID" },
                values: new object[] { new Guid("a7a7f4ec-4600-4eda-bd80-3abcb375302d"), new DateTime(2025, 5, 10, 8, 46, 49, 38, DateTimeKind.Local).AddTicks(5983), "admin@gmail.com", "Admin", "$2a$11$rNMWwHnuYbqCboRhwDQsIeH2nAZfqYnH9quSoA5MCXk5InrfSmWBW", 0, 0, null });

            migrationBuilder.InsertData(
                table: "ProductCombos",
                columns: new[] { "ProductComboID", "ComboID", "ProductID", "Quantity", "Status" },
                values: new object[,]
                {
                    { new Guid("54005e70-3cf6-487b-b771-dde970397f3f"), new Guid("c3d4e5f6-a7b8-4901-bcde-3456789012cd"), new Guid("e1bb1ea5-94b2-45c7-98a2-b1fa0f4e3e6d"), 1, 0 },
                    { new Guid("96fc0609-2f1f-4dd5-8d7c-19767c57faac"), new Guid("c3d4e5f6-a7b8-4901-bcde-3456789012cd"), new Guid("dbc17836-d6f4-46cb-bb9a-77b9c54e7b13"), 1, 0 },
                    { new Guid("aa0427c1-c631-48a2-bf62-4a2a127ba2da"), new Guid("b2c3d4e5-f6a7-4890-abcd-2345678901bc"), new Guid("e1bb1ea5-94b2-45c7-98a2-b1fa0f4e3e6d"), 1, 0 },
                    { new Guid("af881b82-147b-4ad9-89df-7d4e12b83bbc"), new Guid("a1b2c3d4-e5f6-4789-abcd-1234567890ab"), new Guid("e1bb1ea5-94b2-45c7-98a2-b1fa0f4e3e6d"), 1, 0 },
                    { new Guid("bed1a4e1-9ee8-4423-81d6-6bc953432bb3"), new Guid("b2c3d4e5-f6a7-4890-abcd-2345678901bc"), new Guid("85c5e5a3-9a3d-4d9a-a09c-74647eb07bfc"), 1, 0 },
                    { new Guid("c47f2417-7edf-4342-847e-f1c3b4fa9792"), new Guid("c3d4e5f6-a7b8-4901-bcde-3456789012cd"), new Guid("85c5e5a3-9a3d-4d9a-a09c-74647eb07bfc"), 1, 0 },
                    { new Guid("f078478c-82ad-4f58-ab7f-036b27f9c092"), new Guid("a1b2c3d4-e5f6-4789-abcd-1234567890ab"), new Guid("85c5e5a3-9a3d-4d9a-a09c-74647eb07bfc"), 1, 0 },
                    { new Guid("f621562d-7e1b-499a-a53b-18d3fb7baf76"), new Guid("b2c3d4e5-f6a7-4890-abcd-2345678901bc"), new Guid("7b17b539-8168-42c5-8b9f-1c1c783bd423"), 1, 0 },
                    { new Guid("fa570ecb-33f1-48ba-a2b3-9fc884e8ed55"), new Guid("a1b2c3d4-e5f6-4789-abcd-1234567890ab"), new Guid("f4a7b7e8-63b6-4c90-a38a-74c5c8d9d7b1"), 1, 0 }
                });

            migrationBuilder.InsertData(
                table: "Stores",
                columns: new[] { "StoreID", "AddressID", "CreateDate", "Status", "StoreName" },
                values: new object[] { new Guid("8a2e5d21-5f6b-4a7c-9d5e-3f6c8b2a1d0e"), new Guid("931f07e5-46d8-4449-b77e-533bf4f33aa3"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "BB Chicken-Hàm Nghi" });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserID", "CreateDate", "Email", "FullName", "Password", "Role", "Status", "StoreID" },
                values: new object[] { new Guid("8ddadcd2-68bf-4099-af30-e70d1ac6c8c3"), new DateTime(2025, 5, 10, 8, 46, 49, 220, DateTimeKind.Local).AddTicks(4414), "manhdb123@gmail.com", "Phạm Viết Mạnh", "$2a$11$uqhwOfqIYrfPctRDpDtclOKDu2eZzZlU87DNnPrQZUztdyCUJUot.", 1, 0, new Guid("8a2e5d21-5f6b-4a7c-9d5e-3f6c8b2a1d0e") });

            migrationBuilder.CreateIndex(
                name: "IX_Address_UserID",
                table: "Address",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_Bill_AddressID",
                table: "Bill",
                column: "AddressID");

            migrationBuilder.CreateIndex(
                name: "IX_Bill_StoreID",
                table: "Bill",
                column: "StoreID");

            migrationBuilder.CreateIndex(
                name: "IX_Bill_UserID",
                table: "Bill",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_BillDetails_BillID",
                table: "BillDetails",
                column: "BillID");

            migrationBuilder.CreateIndex(
                name: "IX_CartItems_CartID",
                table: "CartItems",
                column: "CartID");

            migrationBuilder.CreateIndex(
                name: "IX_CartItems_ComboID",
                table: "CartItems",
                column: "ComboID");

            migrationBuilder.CreateIndex(
                name: "IX_CartItems_ProductID",
                table: "CartItems",
                column: "ProductID");

            migrationBuilder.CreateIndex(
                name: "IX_Carts_UserID",
                table: "Carts",
                column: "UserID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProductCombos_ComboID",
                table: "ProductCombos",
                column: "ComboID");

            migrationBuilder.CreateIndex(
                name: "IX_ProductCombos_ProductID",
                table: "ProductCombos",
                column: "ProductID");

            migrationBuilder.CreateIndex(
                name: "IX_Stores_AddressID",
                table: "Stores",
                column: "AddressID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_StoreID",
                table: "Users",
                column: "StoreID");

            migrationBuilder.AddForeignKey(
                name: "FK_Address_Users_UserID",
                table: "Address",
                column: "UserID",
                principalTable: "Users",
                principalColumn: "UserID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Address_Users_UserID",
                table: "Address");

            migrationBuilder.DropTable(
                name: "BillDetails");

            migrationBuilder.DropTable(
                name: "CartItems");

            migrationBuilder.DropTable(
                name: "ProductCombos");

            migrationBuilder.DropTable(
                name: "Bill");

            migrationBuilder.DropTable(
                name: "Carts");

            migrationBuilder.DropTable(
                name: "Combos");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Stores");

            migrationBuilder.DropTable(
                name: "Address");
        }
    }
}
