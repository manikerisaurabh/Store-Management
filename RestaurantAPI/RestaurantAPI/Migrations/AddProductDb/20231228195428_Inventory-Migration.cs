using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RestaurantAPI.Migrations.AddProductDb
{
    public partial class InventoryMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CategoryId",
                table: "Inventory");

            migrationBuilder.DropColumn(
                name: "ProductId",
                table: "Inventory");

            migrationBuilder.AlterColumn<decimal>(
                name: "Total",
                table: "Inventory",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "float");

            migrationBuilder.AlterColumn<decimal>(
                name: "ProductCost",
                table: "Inventory",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "float");

            migrationBuilder.AddColumn<string>(
                name: "CategoryName",
                table: "Inventory",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ProductName",
                table: "Inventory",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CategoryName",
                table: "Inventory");

            migrationBuilder.DropColumn(
                name: "ProductName",
                table: "Inventory");

            migrationBuilder.AlterColumn<double>(
                name: "Total",
                table: "Inventory",
                type: "float",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AlterColumn<double>(
                name: "ProductCost",
                table: "Inventory",
                type: "float",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AddColumn<int>(
                name: "CategoryId",
                table: "Inventory",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ProductId",
                table: "Inventory",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
