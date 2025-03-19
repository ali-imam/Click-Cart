using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ClickCartAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddAddress : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Addresses",
                columns: new[] { "AddressId", "City", "Country", "State", "Street", "UserId", "ZipCode" },
                values: new object[] { 1, "Mumbai", "India", "Maharashtra", "Colaba", 1, 400614 });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Addresses",
                keyColumn: "AddressId",
                keyValue: 1);
        }
    }
}
