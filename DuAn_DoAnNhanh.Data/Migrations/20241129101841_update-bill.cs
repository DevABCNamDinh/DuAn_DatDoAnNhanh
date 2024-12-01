using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DuAn_DoAnNhanh.Data.Migrations
{
    /// <inheritdoc />
    public partial class updatebill : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "PriceEndow",
                table: "BillDetails",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "TotalAmountEndow",
                table: "Bill",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PriceEndow",
                table: "BillDetails");

            migrationBuilder.DropColumn(
                name: "TotalAmountEndow",
                table: "Bill");
        }
    }
}
