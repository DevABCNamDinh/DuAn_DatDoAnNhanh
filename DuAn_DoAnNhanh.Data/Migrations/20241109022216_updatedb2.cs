using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DuAn_DoAnNhanh.Data.Migrations
{
    /// <inheritdoc />
    public partial class updatedb2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreteBy",
                table: "Combos");

            migrationBuilder.AddColumn<decimal>(
                name: "Price",
                table: "OrderDetails",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<Guid>(
                name: "ComboID",
                table: "CartItems",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_CartItems_ComboID",
                table: "CartItems",
                column: "ComboID");

            migrationBuilder.AddForeignKey(
                name: "FK_CartItems_Combos_ComboID",
                table: "CartItems",
                column: "ComboID",
                principalTable: "Combos",
                principalColumn: "ComboID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CartItems_Combos_ComboID",
                table: "CartItems");

            migrationBuilder.DropIndex(
                name: "IX_CartItems_ComboID",
                table: "CartItems");

            migrationBuilder.DropColumn(
                name: "Price",
                table: "OrderDetails");

            migrationBuilder.DropColumn(
                name: "ComboID",
                table: "CartItems");

            migrationBuilder.AddColumn<Guid>(
                name: "CreteBy",
                table: "Combos",
                type: "uniqueidentifier",
                nullable: true);
        }
    }
}
