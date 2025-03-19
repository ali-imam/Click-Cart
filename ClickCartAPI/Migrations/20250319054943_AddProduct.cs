using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ClickCartAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddProduct : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Carts",
                keyColumn: "CartId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Orders",
                keyColumn: "OrderId",
                keyValue: 1);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Carts",
                columns: new[] { "CartId", "ProductId", "Quantity", "UserId" },
                values: new object[] { 1, 1, 2, 2 });

            migrationBuilder.InsertData(
                table: "Orders",
                columns: new[] { "OrderId", "OrderCode", "OrderDate", "ProductId", "Quantity", "Status", "UserId" },
                values: new object[] { 1, "ASDFFFF", null, 1, 2, "Shipped", 1 });
        }
    }
}
