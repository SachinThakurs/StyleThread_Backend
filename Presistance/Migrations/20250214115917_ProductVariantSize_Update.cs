using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Presistance.Migrations
{
    /// <inheritdoc />
    public partial class ProductVariantSize_Update : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductVariantSizes_Sizes_SizeId",
                table: "ProductVariantSizes");

            migrationBuilder.DropIndex(
                name: "IX_ProductVariantSizes_SizeId",
                table: "ProductVariantSizes");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_ProductVariantSizes_SizeId",
                table: "ProductVariantSizes",
                column: "SizeId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductVariantSizes_Sizes_SizeId",
                table: "ProductVariantSizes",
                column: "SizeId",
                principalTable: "Sizes",
                principalColumn: "SizeId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
