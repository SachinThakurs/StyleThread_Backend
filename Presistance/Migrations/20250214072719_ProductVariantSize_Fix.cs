using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Presistance.Migrations
{
    /// <inheritdoc />
    public partial class ProductVariantSize_Fix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductVariantSizes_Sizes_SizeId",
                table: "ProductVariantSizes");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductVariantSizes_Sizes_SizeId",
                table: "ProductVariantSizes",
                column: "SizeId",
                principalTable: "Sizes",
                principalColumn: "SizeId",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductVariantSizes_Sizes_SizeId",
                table: "ProductVariantSizes");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductVariantSizes_Sizes_SizeId",
                table: "ProductVariantSizes",
                column: "SizeId",
                principalTable: "Sizes",
                principalColumn: "SizeId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
