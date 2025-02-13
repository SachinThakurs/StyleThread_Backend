using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Presistance.Migrations
{
    /// <inheritdoc />
    public partial class demo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "57ec5eff-ee3b-4264-b436-7b62dabff246");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "bc262523-4c85-4eb6-ae89-ec71f156bc7a");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "d6665056-7542-4976-98c7-3b9cc487eda6");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "e6d6e72f-c58e-4c0f-a750-b1372b0d2d67");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "57ec5eff-ee3b-4264-b436-7b62dabff246", null, "Administrator", "ADMINISTRATOR" },
                    { "bc262523-4c85-4eb6-ae89-ec71f156bc7a", null, "SuperAdministrator", "SUPERADMINISTRATOR" },
                    { "d6665056-7542-4976-98c7-3b9cc487eda6", null, "Seller", "SELLER" },
                    { "e6d6e72f-c58e-4c0f-a750-b1372b0d2d67", null, "Visitor", "VISITOR" }
                });
        }
    }
}
