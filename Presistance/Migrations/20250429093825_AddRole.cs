using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Presistance.Migrations
{
    /// <inheritdoc />
    public partial class AddRole : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "026d86c0-a803-4237-950d-7f197c6351b3", null, "Visitor", "VISITOR" },
                    { "22b6ec8b-dbc6-4f24-9444-0f201e323201", null, "Seller", "SELLER" },
                    { "51825ff7-52b2-40bb-975f-d501997fd4ca", null, "SuperAdministrator", "SUPERADMINISTRATOR" },
                    { "98f0d7f8-0313-4a26-8380-fa148658dc69", null, "Administrator", "ADMINISTRATOR" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "026d86c0-a803-4237-950d-7f197c6351b3");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "22b6ec8b-dbc6-4f24-9444-0f201e323201");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "51825ff7-52b2-40bb-975f-d501997fd4ca");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "98f0d7f8-0313-4a26-8380-fa148658dc69");
        }
    }
}
