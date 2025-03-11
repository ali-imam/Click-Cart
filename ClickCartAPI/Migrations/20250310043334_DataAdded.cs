using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ClickCartAPI.Migrations
{
    /// <inheritdoc />
    public partial class DataAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Carts",
                columns: new[] { "CartId", "Price", "ProductId", "ProductImg", "ProductName", "Quantity", "UserId" },
                values: new object[] { 1, "2200", 1, "/images/products/first.webp", "Watch", 2, 2 });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Carts",
                keyColumn: "CartId",
                keyValue: 1);
        }
    }
}
