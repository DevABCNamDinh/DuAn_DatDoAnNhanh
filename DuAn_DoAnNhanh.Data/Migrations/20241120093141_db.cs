using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DuAn_DoAnNhanh.Data.Migrations
{
    /// <inheritdoc />
    public partial class db : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderDetails_Combos_ComboID",
                table: "OrderDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderDetails_Orders_OrderID",
                table: "OrderDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderDetails_Products_ProductID",
                table: "OrderDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Users_UserID",
                table: "Orders");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Orders",
                table: "Orders");

            migrationBuilder.DropPrimaryKey(
                name: "PK_OrderDetails",
                table: "OrderDetails");

            migrationBuilder.DropIndex(
                name: "IX_OrderDetails_ComboID",
                table: "OrderDetails");

            migrationBuilder.DropIndex(
                name: "IX_OrderDetails_OrderID",
                table: "OrderDetails");

            migrationBuilder.DropColumn(
                name: "OrderDetailsID",
                table: "OrderDetails");

            migrationBuilder.DropColumn(
                name: "ComboID",
                table: "OrderDetails");

            migrationBuilder.RenameTable(
                name: "Orders",
                newName: "Bill");

            migrationBuilder.RenameTable(
                name: "OrderDetails",
                newName: "BillDetails");

            migrationBuilder.RenameColumn(
                name: "OrderDate",
                table: "Bill",
                newName: "BillDate");

            migrationBuilder.RenameColumn(
                name: "OrderID",
                table: "Bill",
                newName: "BillID");

            migrationBuilder.RenameIndex(
                name: "IX_Orders_UserID",
                table: "Bill",
                newName: "IX_Bill_UserID");

            migrationBuilder.RenameColumn(
                name: "ProductID",
                table: "BillDetails",
                newName: "BillID");

            migrationBuilder.RenameColumn(
                name: "OrderID",
                table: "BillDetails",
                newName: "BillDetailsID");

            migrationBuilder.RenameIndex(
                name: "IX_OrderDetails_ProductID",
                table: "BillDetails",
                newName: "IX_BillDetails_BillID");

            migrationBuilder.AlterColumn<string>(
                name: "ImageUrl",
                table: "Products",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<decimal>(
                name: "TotalAmount",
                table: "Bill",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<string>(
                name: "Image",
                table: "BillDetails",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "PCName",
                table: "BillDetails",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Bill",
                table: "Bill",
                column: "BillID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BillDetails",
                table: "BillDetails",
                column: "BillDetailsID");

            migrationBuilder.AddForeignKey(
                name: "FK_Bill_Users_UserID",
                table: "Bill",
                column: "UserID",
                principalTable: "Users",
                principalColumn: "UserID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BillDetails_Bill_BillID",
                table: "BillDetails",
                column: "BillID",
                principalTable: "Bill",
                principalColumn: "BillID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bill_Users_UserID",
                table: "Bill");

            migrationBuilder.DropForeignKey(
                name: "FK_BillDetails_Bill_BillID",
                table: "BillDetails");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BillDetails",
                table: "BillDetails");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Bill",
                table: "Bill");

            migrationBuilder.DropColumn(
                name: "Image",
                table: "BillDetails");

            migrationBuilder.DropColumn(
                name: "PCName",
                table: "BillDetails");

            migrationBuilder.DropColumn(
                name: "TotalAmount",
                table: "Bill");

            migrationBuilder.RenameTable(
                name: "BillDetails",
                newName: "OrderDetails");

            migrationBuilder.RenameTable(
                name: "Bill",
                newName: "Orders");

            migrationBuilder.RenameColumn(
                name: "BillID",
                table: "OrderDetails",
                newName: "ProductID");

            migrationBuilder.RenameColumn(
                name: "BillDetailsID",
                table: "OrderDetails",
                newName: "OrderID");

            migrationBuilder.RenameIndex(
                name: "IX_BillDetails_BillID",
                table: "OrderDetails",
                newName: "IX_OrderDetails_ProductID");

            migrationBuilder.RenameColumn(
                name: "BillDate",
                table: "Orders",
                newName: "OrderDate");

            migrationBuilder.RenameColumn(
                name: "BillID",
                table: "Orders",
                newName: "OrderID");

            migrationBuilder.RenameIndex(
                name: "IX_Bill_UserID",
                table: "Orders",
                newName: "IX_Orders_UserID");

            migrationBuilder.AlterColumn<string>(
                name: "ImageUrl",
                table: "Products",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "OrderDetailsID",
                table: "OrderDetails",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "ComboID",
                table: "OrderDetails",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddPrimaryKey(
                name: "PK_OrderDetails",
                table: "OrderDetails",
                column: "OrderDetailsID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Orders",
                table: "Orders",
                column: "OrderID");

            migrationBuilder.CreateIndex(
                name: "IX_OrderDetails_ComboID",
                table: "OrderDetails",
                column: "ComboID");

            migrationBuilder.CreateIndex(
                name: "IX_OrderDetails_OrderID",
                table: "OrderDetails",
                column: "OrderID");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderDetails_Combos_ComboID",
                table: "OrderDetails",
                column: "ComboID",
                principalTable: "Combos",
                principalColumn: "ComboID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderDetails_Orders_OrderID",
                table: "OrderDetails",
                column: "OrderID",
                principalTable: "Orders",
                principalColumn: "OrderID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderDetails_Products_ProductID",
                table: "OrderDetails",
                column: "ProductID",
                principalTable: "Products",
                principalColumn: "ProductID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Users_UserID",
                table: "Orders",
                column: "UserID",
                principalTable: "Users",
                principalColumn: "UserID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
