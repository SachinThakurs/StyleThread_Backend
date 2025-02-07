using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Presistance.Migrations
{
    /// <inheritdoc />
    public partial class oninit : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "051c4d69-c7a5-42bd-8cd3-6a20b03a10c6");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "07320367-a488-4b05-86b4-f50c019191e6");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "483a176e-65f9-4fbc-90f8-9807fe0898ab");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "f7ced334-d652-4319-9bde-81c21277019b");

            migrationBuilder.AddColumn<string>(
                name: "SizeName",
                table: "ProductVariantSizes",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "39c8199c-997a-4710-9546-133adc9b6109", null, "Seller", "SELLER" },
                    { "608bf850-c2b3-423b-94b2-20c3de545aed", null, "Administrator", "ADMINISTRATOR" },
                    { "67fc53a8-95cb-4818-b6df-f65bf2e66ae5", null, "SuperAdministrator", "SUPERADMINISTRATOR" },
                    { "d88dd2cf-699d-4569-aa31-18202fb26018", null, "Visitor", "VISITOR" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "39c8199c-997a-4710-9546-133adc9b6109");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "608bf850-c2b3-423b-94b2-20c3de545aed");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "67fc53a8-95cb-4818-b6df-f65bf2e66ae5");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "d88dd2cf-699d-4569-aa31-18202fb26018");

            migrationBuilder.DropColumn(
                name: "SizeName",
                table: "ProductVariantSizes");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "051c4d69-c7a5-42bd-8cd3-6a20b03a10c6", null, "Seller", "SELLER" },
                    { "07320367-a488-4b05-86b4-f50c019191e6", null, "Visitor", "VISITOR" },
                    { "483a176e-65f9-4fbc-90f8-9807fe0898ab", null, "Administrator", "ADMINISTRATOR" },
                    { "f7ced334-d652-4319-9bde-81c21277019b", null, "SuperAdministrator", "SUPERADMINISTRATOR" }
                });
        }
    }
}
