using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DuAn_DoAnNhanh.Data.Migrations
{
    /// <inheritdoc />
    public partial class _2st : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bill_Address_UserID",
                table: "Bill");

            migrationBuilder.RenameColumn(
                name: "status",
                table: "Address",
                newName: "Status");

            migrationBuilder.AddColumn<int>(
                name: "ReceivingType",
                table: "Bill",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Address",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.InsertData(
                table: "Combos",
                columns: new[] { "ComboID", "ComboName", "CreteDate", "Description", "Image", "Price", "SetupPrice", "Status" },
                values: new object[,]
                {
                    { new Guid("a1b2c3d4-e5f6-4789-abcd-1234567890ab"), "Combo Burger Siêu Cay", new DateTime(2025, 3, 29, 8, 15, 40, 296, DateTimeKind.Utc).AddTicks(8072), "Burger Siêu Cay kèm Khoai Tây Chiên (M) và Pepsi Zero (M)", "https://www.lotteria.vn/media/catalog/product/cache/400x400/m/e/menu_burger_2.jpg.webp", 65000m, 60000m, 0 },
                    { new Guid("b2c3d4e5-f6a7-4890-abcd-2345678901bc"), "Combo Gà Rán", new DateTime(2025, 3, 29, 8, 15, 40, 296, DateTimeKind.Utc).AddTicks(8080), "Gà Rán (1 miếng) kèm Khoai Tây Chiên (M) và Pepsi Zero (M)", "https://www.lotteria.vn/media/catalog/product/cache/400x400/2/2/222281_4.png.webp", 85000m, null, 0 },
                    { new Guid("c3d4e5f6-a7b8-4901-bcde-3456789012cd"), "Combo Gà Nướng", new DateTime(2025, 3, 29, 8, 15, 40, 296, DateTimeKind.Utc).AddTicks(8087), "Gà Nướng (1 miếng) kèm Khoai Tây Chiên (M) và Pepsi Zero (M)", "https://www.lotteria.vn/media/catalog/product/cache/400x400/2/2/228380.png.webp", 87000m, 80000m, 0 }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "ProductID", "CreteDate", "Description", "ImageUrl", "Price", "ProductName", "Quantity", "Status" },
                values: new object[,]
                {
                    { new Guid("2d8f7e1a-5cbb-4ff1-bcbc-f82b07dcb4ad"), new DateTime(2025, 3, 29, 8, 15, 40, 296, DateTimeKind.Utc).AddTicks(7504), "Mô tả", "https://www.lotteria.vn/media/catalog/product/cache/400x400/b/u/burger_shrimp_1_.png.webp", 35000m, "Burger Tôm", 0, 0 },
                    { new Guid("7b17b539-8168-42c5-8b9f-1c1c783bd423"), new DateTime(2025, 3, 29, 8, 15, 40, 296, DateTimeKind.Utc).AddTicks(7538), "Mô tả", "https://www.lotteria.vn/media/catalog/product/cache/400x400/l/c/lc0001_4.png.webp", 40000m, "Gà Rán (1 miếng)", 0, 0 },
                    { new Guid("85c5e5a3-9a3d-4d9a-a09c-74647eb07bfc"), new DateTime(2025, 3, 29, 8, 15, 40, 296, DateTimeKind.Utc).AddTicks(7510), "Mô tả", "https://www.lotteria.vn/media/catalog/product/cache/400x400/d/r/drink_pepsi_zero_m_l__2.png.webp", 15000m, "Pepsi Zero (M)", 0, 0 },
                    { new Guid("9e41d162-3f6a-42a1-b9a6-28f6efbc7f5c"), new DateTime(2025, 3, 29, 8, 15, 40, 296, DateTimeKind.Utc).AddTicks(7498), "Mô tả", "https://www.lotteria.vn/media/catalog/product/cache/400x400/b/u/burger_bulgogi_4.png.webp", 35000m, "Burger Bulgogi", 0, 0 },
                    { new Guid("b487da52-d738-4376-a1e3-c4a4d2fc7ef1"), new DateTime(2025, 3, 29, 8, 15, 40, 296, DateTimeKind.Utc).AddTicks(7522), "Mô tả", "https://www.lotteria.vn/media/catalog/product/cache/400x400/d/e/dessert_shake_potato_tuy_t_xanh_.png.webp", 25000m, "Khoai lắc tuyết xanh", 0, 0 },
                    { new Guid("c1a8f0ee-73c9-4c2f-b10f-fc3d6561d275"), new DateTime(2025, 3, 29, 8, 15, 40, 296, DateTimeKind.Utc).AddTicks(7543), "Mô tả", "https://www.lotteria.vn/media/catalog/product/cache/400x400/m/e/menu_menu_5_.jpg.webp", 41000m, "Gà Sốt Bơ Tỏi (1 miếng)", 0, 0 },
                    { new Guid("dbc17836-d6f4-46cb-bb9a-77b9c54e7b13"), new DateTime(2025, 3, 29, 8, 15, 40, 296, DateTimeKind.Utc).AddTicks(7533), "Mô tả", "https://www.lotteria.vn/media/catalog/product/cache/400x400/2/2/227436_2.png.webp", 40000m, "Gà Nướng (1 miếng)", 0, 0 },
                    { new Guid("e1bb1ea5-94b2-45c7-98a2-b1fa0f4e3e6d"), new DateTime(2025, 3, 29, 8, 15, 40, 296, DateTimeKind.Utc).AddTicks(7515), "Mô tả", "https://www.lotteria.vn/media/catalog/product/cache/400x400/d/e/dessert_french_fries_m_i.png.webp", 25000m, "Khoai Tây Chiên (M)", 0, 0 },
                    { new Guid("f4a7b7e8-63b6-4c90-a38a-74c5c8d9d7b1"), new DateTime(2025, 3, 29, 8, 15, 40, 296, DateTimeKind.Utc).AddTicks(7488), "Mô tả", "https://www.lotteria.vn/media/catalog/product/cache/400x400/m/e/menu_burger_2.jpg.webp", 35000m, "Burger Siêu Cay", 0, 0 },
                    { new Guid("f6a71ac8-78f3-4194-88c9-c2aa9467f93e"), new DateTime(2025, 3, 29, 8, 15, 40, 296, DateTimeKind.Utc).AddTicks(7527), "Mô tả", "https://www.lotteria.vn/media/catalog/product/cache/400x400/l/c/lc0003_1.png.webp", 41000m, "Gà Sốt HS (1 miếng)", 0, 0 }
                });

            migrationBuilder.InsertData(
                table: "Stores",
                columns: new[] { "StoreID", "CreateDate", "Status", "StoreName" },
                values: new object[,]
                {
                    { new Guid("8a2e5d21-5f6b-4a7c-9d5e-3f6c8b2a1d0e"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "BB Chicken-Hàm Nghi" },
                    { new Guid("931f07e5-46d8-4449-b77e-533bf4f33aa3"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "BB Chicken-Lê đức Thọ" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserID", "CreateDate", "Email", "FullName", "Password", "Role", "Status", "StoreID" },
                values: new object[] { new Guid("10bcba6c-46b1-4233-89d3-877c120a471f"), new DateTime(2025, 3, 29, 15, 15, 40, 296, DateTimeKind.Local).AddTicks(8480), "admin@gmail.com", "Admin", "admin", 0, 0, null });

            migrationBuilder.InsertData(
                table: "Address",
                columns: new[] { "AddressID", "AddressType", "CreateDate", "Description", "District", "FullAddress", "FullName", "Latitude", "Longitude", "NumberPhone", "Province", "SpecificAddress", "Status", "StoreID", "UserID", "Ward" },
                values: new object[,]
                {
                    { new Guid("8c8554dd-c872-4847-a811-fc29416ac68e"), 2, new DateTime(2025, 3, 29, 15, 15, 40, 296, DateTimeKind.Local).AddTicks(8424), null, "Quận Nam Từ Liêm", "Số 7 Lê đức Thọ, Phường Mỹ đình 2, Quận Nam Từ Liêm, Thành phố Hà Nội", "BB Chicken-Lê đức Thọ", 21.02983, 105.76913, "0828277707", "Thành phố Hà Nội", "Số 7 Lê đức Thọ", 0, new Guid("931f07e5-46d8-4449-b77e-533bf4f33aa3"), null, "Phường Mỹ đình 2" },
                    { new Guid("a137e13f-5b4b-4a98-b229-0e7341846b28"), 2, new DateTime(2025, 3, 29, 15, 15, 40, 296, DateTimeKind.Local).AddTicks(8400), null, "Quận Nam Từ Liêm", "Số 36 Hàm Nghi, Phường Cầu Diễn, Quận Nam Từ Liêm, Thành phố Hà Nội", "BB Chicken-Hàm Nghi", 21.02983, 105.76913, "055931234", "Thành phố Hà Nội", "Số 36 Hàm Nghi", 0, new Guid("8a2e5d21-5f6b-4a7c-9d5e-3f6c8b2a1d0e"), null, "Phường Cầu Diễn" }
                });

            migrationBuilder.InsertData(
                table: "ProductCombos",
                columns: new[] { "ProductComboID", "ComboID", "ProductID", "Quantity", "Status" },
                values: new object[,]
                {
                    { new Guid("27ddb993-2752-4359-bc02-c18ae57df6ae"), new Guid("b2c3d4e5-f6a7-4890-abcd-2345678901bc"), new Guid("85c5e5a3-9a3d-4d9a-a09c-74647eb07bfc"), 1, 0 },
                    { new Guid("30169240-1ef4-4cab-8c82-9feb995b52dc"), new Guid("a1b2c3d4-e5f6-4789-abcd-1234567890ab"), new Guid("f4a7b7e8-63b6-4c90-a38a-74c5c8d9d7b1"), 1, 0 },
                    { new Guid("3ed69b72-c2de-4d09-806c-e865adfdf2b9"), new Guid("a1b2c3d4-e5f6-4789-abcd-1234567890ab"), new Guid("85c5e5a3-9a3d-4d9a-a09c-74647eb07bfc"), 1, 0 },
                    { new Guid("434ce794-1ca0-44b4-b234-020b4da58a22"), new Guid("c3d4e5f6-a7b8-4901-bcde-3456789012cd"), new Guid("dbc17836-d6f4-46cb-bb9a-77b9c54e7b13"), 1, 0 },
                    { new Guid("74c74a7b-2a74-4524-a48b-7493de78a8cb"), new Guid("a1b2c3d4-e5f6-4789-abcd-1234567890ab"), new Guid("e1bb1ea5-94b2-45c7-98a2-b1fa0f4e3e6d"), 1, 0 },
                    { new Guid("7765f52f-08b8-437f-b16e-c36fa0145f3d"), new Guid("c3d4e5f6-a7b8-4901-bcde-3456789012cd"), new Guid("e1bb1ea5-94b2-45c7-98a2-b1fa0f4e3e6d"), 1, 0 },
                    { new Guid("9041c541-891b-47a7-b065-ecd76084da6f"), new Guid("b2c3d4e5-f6a7-4890-abcd-2345678901bc"), new Guid("e1bb1ea5-94b2-45c7-98a2-b1fa0f4e3e6d"), 1, 0 },
                    { new Guid("9cccfa2d-2eda-46d7-b162-20f8e2ef4a17"), new Guid("c3d4e5f6-a7b8-4901-bcde-3456789012cd"), new Guid("85c5e5a3-9a3d-4d9a-a09c-74647eb07bfc"), 1, 0 },
                    { new Guid("cf88ce7e-80b7-47a2-ae29-42df7b2773dc"), new Guid("b2c3d4e5-f6a7-4890-abcd-2345678901bc"), new Guid("7b17b539-8168-42c5-8b9f-1c1c783bd423"), 1, 0 }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserID", "CreateDate", "Email", "FullName", "Password", "Role", "Status", "StoreID" },
                values: new object[,]
                {
                    { new Guid("411f4d61-d4a7-4eec-b584-308a4f71e9e7"), new DateTime(2025, 3, 29, 15, 15, 40, 296, DateTimeKind.Local).AddTicks(8497), "linhdb123@gmail.com", "Nguyễn Phương Linh", "linhdb123", 1, 0, new Guid("8a2e5d21-5f6b-4a7c-9d5e-3f6c8b2a1d0e") },
                    { new Guid("ac5b3bbf-8d67-4143-af3b-b03b9db4de14"), new DateTime(2025, 3, 29, 15, 15, 40, 296, DateTimeKind.Local).AddTicks(8489), "manhdb123@gmail.com", "Phạm Viết Manh", "manhdb123", 1, 0, new Guid("931f07e5-46d8-4449-b77e-533bf4f33aa3") }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Bill_AddressID",
                table: "Bill",
                column: "AddressID");

            migrationBuilder.AddForeignKey(
                name: "FK_Bill_Address_AddressID",
                table: "Bill",
                column: "AddressID",
                principalTable: "Address",
                principalColumn: "AddressID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bill_Address_AddressID",
                table: "Bill");

            migrationBuilder.DropIndex(
                name: "IX_Bill_AddressID",
                table: "Bill");

            migrationBuilder.DeleteData(
                table: "Address",
                keyColumn: "AddressID",
                keyValue: new Guid("8c8554dd-c872-4847-a811-fc29416ac68e"));

            migrationBuilder.DeleteData(
                table: "Address",
                keyColumn: "AddressID",
                keyValue: new Guid("a137e13f-5b4b-4a98-b229-0e7341846b28"));

            migrationBuilder.DeleteData(
                table: "ProductCombos",
                keyColumn: "ProductComboID",
                keyValue: new Guid("27ddb993-2752-4359-bc02-c18ae57df6ae"));

            migrationBuilder.DeleteData(
                table: "ProductCombos",
                keyColumn: "ProductComboID",
                keyValue: new Guid("30169240-1ef4-4cab-8c82-9feb995b52dc"));

            migrationBuilder.DeleteData(
                table: "ProductCombos",
                keyColumn: "ProductComboID",
                keyValue: new Guid("3ed69b72-c2de-4d09-806c-e865adfdf2b9"));

            migrationBuilder.DeleteData(
                table: "ProductCombos",
                keyColumn: "ProductComboID",
                keyValue: new Guid("434ce794-1ca0-44b4-b234-020b4da58a22"));

            migrationBuilder.DeleteData(
                table: "ProductCombos",
                keyColumn: "ProductComboID",
                keyValue: new Guid("74c74a7b-2a74-4524-a48b-7493de78a8cb"));

            migrationBuilder.DeleteData(
                table: "ProductCombos",
                keyColumn: "ProductComboID",
                keyValue: new Guid("7765f52f-08b8-437f-b16e-c36fa0145f3d"));

            migrationBuilder.DeleteData(
                table: "ProductCombos",
                keyColumn: "ProductComboID",
                keyValue: new Guid("9041c541-891b-47a7-b065-ecd76084da6f"));

            migrationBuilder.DeleteData(
                table: "ProductCombos",
                keyColumn: "ProductComboID",
                keyValue: new Guid("9cccfa2d-2eda-46d7-b162-20f8e2ef4a17"));

            migrationBuilder.DeleteData(
                table: "ProductCombos",
                keyColumn: "ProductComboID",
                keyValue: new Guid("cf88ce7e-80b7-47a2-ae29-42df7b2773dc"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: new Guid("2d8f7e1a-5cbb-4ff1-bcbc-f82b07dcb4ad"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: new Guid("9e41d162-3f6a-42a1-b9a6-28f6efbc7f5c"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: new Guid("b487da52-d738-4376-a1e3-c4a4d2fc7ef1"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: new Guid("c1a8f0ee-73c9-4c2f-b10f-fc3d6561d275"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: new Guid("f6a71ac8-78f3-4194-88c9-c2aa9467f93e"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: new Guid("10bcba6c-46b1-4233-89d3-877c120a471f"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: new Guid("411f4d61-d4a7-4eec-b584-308a4f71e9e7"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: new Guid("ac5b3bbf-8d67-4143-af3b-b03b9db4de14"));

            migrationBuilder.DeleteData(
                table: "Combos",
                keyColumn: "ComboID",
                keyValue: new Guid("a1b2c3d4-e5f6-4789-abcd-1234567890ab"));

            migrationBuilder.DeleteData(
                table: "Combos",
                keyColumn: "ComboID",
                keyValue: new Guid("b2c3d4e5-f6a7-4890-abcd-2345678901bc"));

            migrationBuilder.DeleteData(
                table: "Combos",
                keyColumn: "ComboID",
                keyValue: new Guid("c3d4e5f6-a7b8-4901-bcde-3456789012cd"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: new Guid("7b17b539-8168-42c5-8b9f-1c1c783bd423"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: new Guid("85c5e5a3-9a3d-4d9a-a09c-74647eb07bfc"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: new Guid("dbc17836-d6f4-46cb-bb9a-77b9c54e7b13"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: new Guid("e1bb1ea5-94b2-45c7-98a2-b1fa0f4e3e6d"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: new Guid("f4a7b7e8-63b6-4c90-a38a-74c5c8d9d7b1"));

            migrationBuilder.DeleteData(
                table: "Stores",
                keyColumn: "StoreID",
                keyValue: new Guid("8a2e5d21-5f6b-4a7c-9d5e-3f6c8b2a1d0e"));

            migrationBuilder.DeleteData(
                table: "Stores",
                keyColumn: "StoreID",
                keyValue: new Guid("931f07e5-46d8-4449-b77e-533bf4f33aa3"));

            migrationBuilder.DropColumn(
                name: "ReceivingType",
                table: "Bill");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "Address");

            migrationBuilder.RenameColumn(
                name: "Status",
                table: "Address",
                newName: "status");

            migrationBuilder.AddForeignKey(
                name: "FK_Bill_Address_UserID",
                table: "Bill",
                column: "UserID",
                principalTable: "Address",
                principalColumn: "AddressID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
