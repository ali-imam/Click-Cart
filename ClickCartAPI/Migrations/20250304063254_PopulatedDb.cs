using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ClickCartAPI.Migrations
{
    /// <inheritdoc />
    public partial class PopulatedDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "CategoryId", "CategoryName" },
                values: new object[] { 1, "Fashion" });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserId", "Email", "Password", "Phone", "Role", "UserName" },
                values: new object[,]
                {
                    { 1, "mohddwasi@gmail.com", "wasi#Passorwd90", "8692051488", "User", "Wasi" },
                    { 2, "aliimam@gmail.com", "ali#Passorwd91", "8692051488", "Admin", "Ali" }
                });

            migrationBuilder.InsertData(
                table: "Addresses",
                columns: new[] { "AddressId", "City", "Country", "State", "Street", "UserId", "ZipCode" },
                values: new object[] { 1, "Mumbai", "India", "Maharashtra", "Colaba", 1, 400614 });

            migrationBuilder.InsertData(
                table: "Orders",
                columns: new[] { "OrderId", "OrderDate", "Status", "TotalAmount", "UserId" },
                values: new object[] { 1, null, "Shipped", "4400", 1 });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "ProductId", "CategoryId", "Description", "Price", "ProductName", "StockQuantity" },
                values: new object[] { 1, 1, "ifhiehfoiwehf", "2200", "watch", 20 });

            migrationBuilder.InsertData(
                table: "OrderItems",
                columns: new[] { "OrderItemId", "OrderDate", "OrderId", "ProductId", "Quantity", "SubTotal" },
                values: new object[] { 1, null, 1, 1, 2, "4400" });

            migrationBuilder.InsertData(
                table: "Payments",
                columns: new[] { "PaymentId", "OrderId", "PaymentMethod", "PaymentStatus" },
                values: new object[] { 1, 1, "UPI", "Pending" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Addresses",
                keyColumn: "AddressId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "OrderItems",
                keyColumn: "OrderItemId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Payments",
                keyColumn: "PaymentId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Orders",
                keyColumn: "OrderId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 1);
        }
    }
}
