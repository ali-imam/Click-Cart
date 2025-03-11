using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ClickCartAPI.Migrations
{
    /// <inheritdoc />
    public partial class CartUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Price",
                table: "Carts");

            migrationBuilder.DropColumn(
                name: "ProductImg",
                table: "Carts");

            migrationBuilder.DropColumn(
                name: "ProductName",
                table: "Carts");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Price",
                table: "Carts",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ProductImg",
                table: "Carts",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ProductName",
                table: "Carts",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "Carts",
                keyColumn: "CartId",
                keyValue: 1,
                columns: new[] { "Price", "ProductImg", "ProductName" },
                values: new object[] { "2200", "/images/products/first.webp", "Watch" });
        }
    }
}
