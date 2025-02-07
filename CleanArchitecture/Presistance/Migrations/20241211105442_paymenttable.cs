using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Presistance.Migrations
{
    /// <inheritdoc />
    public partial class paymenttable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.CreateTable(
                name: "Payment",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrderId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PaymentId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PaymentSignature = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CustomerName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PaymentStatus = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PaymentDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CustomerEmail = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CustomerContact = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Payment", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "21951c71-e236-48b1-a298-5c3150c433da", null, "SuperAdministrator", "SUPERADMINISTRATOR" },
                    { "9801978b-5435-4b85-b2f4-fccd79f2756b", null, "Administrator", "ADMINISTRATOR" },
                    { "d652ffc3-0dc8-406e-bb6a-d6df8d6af8d8", null, "Visitor", "VISITOR" },
                    { "ea41c335-0897-4747-a9ea-3c89fce3f273", null, "Seller", "SELLER" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Payment");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "21951c71-e236-48b1-a298-5c3150c433da");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "9801978b-5435-4b85-b2f4-fccd79f2756b");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "d652ffc3-0dc8-406e-bb6a-d6df8d6af8d8");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "ea41c335-0897-4747-a9ea-3c89fce3f273");

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
    }
}
