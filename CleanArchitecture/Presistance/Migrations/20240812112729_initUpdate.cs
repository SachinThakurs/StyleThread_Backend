using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Presistance.Migrations
{
    /// <inheritdoc />
    public partial class initUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductVariantSize_ProductVariants_ProductVariantId",
                table: "ProductVariantSize");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductVariantSize_Sizes_SizeId",
                table: "ProductVariantSize");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProductVariantSize",
                table: "ProductVariantSize");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "44de8dd5-0d96-42a7-a0a7-358954193b74");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "6185dd43-925d-4fbf-84d2-225d0883053b");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "c8b81d6a-6a17-4546-8a24-9b1d445997fc");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "d4e72011-e26a-4368-a305-637e0d380d51");

            migrationBuilder.RenameTable(
                name: "ProductVariantSize",
                newName: "ProductVariantSizes");

            migrationBuilder.RenameIndex(
                name: "IX_ProductVariantSize_SizeId",
                table: "ProductVariantSizes",
                newName: "IX_ProductVariantSizes_SizeId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductVariantSizes",
                table: "ProductVariantSizes",
                columns: new[] { "ProductVariantId", "SizeId" });

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

            migrationBuilder.AddForeignKey(
                name: "FK_ProductVariantSizes_ProductVariants_ProductVariantId",
                table: "ProductVariantSizes",
                column: "ProductVariantId",
                principalTable: "ProductVariants",
                principalColumn: "ProductVariantId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductVariantSizes_Sizes_SizeId",
                table: "ProductVariantSizes",
                column: "SizeId",
                principalTable: "Sizes",
                principalColumn: "SizeId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductVariantSizes_ProductVariants_ProductVariantId",
                table: "ProductVariantSizes");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductVariantSizes_Sizes_SizeId",
                table: "ProductVariantSizes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProductVariantSizes",
                table: "ProductVariantSizes");

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

            migrationBuilder.RenameTable(
                name: "ProductVariantSizes",
                newName: "ProductVariantSize");

            migrationBuilder.RenameIndex(
                name: "IX_ProductVariantSizes_SizeId",
                table: "ProductVariantSize",
                newName: "IX_ProductVariantSize_SizeId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductVariantSize",
                table: "ProductVariantSize",
                columns: new[] { "ProductVariantId", "SizeId" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "44de8dd5-0d96-42a7-a0a7-358954193b74", null, "Administrator", "ADMINISTRATOR" },
                    { "6185dd43-925d-4fbf-84d2-225d0883053b", null, "Visitor", "VISITOR" },
                    { "c8b81d6a-6a17-4546-8a24-9b1d445997fc", null, "Seller", "SELLER" },
                    { "d4e72011-e26a-4368-a305-637e0d380d51", null, "SuperAdministrator", "SUPERADMINISTRATOR" }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_ProductVariantSize_ProductVariants_ProductVariantId",
                table: "ProductVariantSize",
                column: "ProductVariantId",
                principalTable: "ProductVariants",
                principalColumn: "ProductVariantId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductVariantSize_Sizes_SizeId",
                table: "ProductVariantSize",
                column: "SizeId",
                principalTable: "Sizes",
                principalColumn: "SizeId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
