using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Cosmetics.Migrations
{
    public partial class dbContextModelUpdateTestimonalImage : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FeaturedProduct_About_AboutId",
                table: "FeaturedProduct");

            migrationBuilder.DropForeignKey(
                name: "FK_TestimonalImages_TestimonalImages_TestimonalImageId",
                table: "TestimonalImages");

            migrationBuilder.DropForeignKey(
                name: "FK_Testimonals_Products_ProductId",
                table: "Testimonals");

            migrationBuilder.DropIndex(
                name: "IX_Testimonals_ProductId",
                table: "Testimonals");

            migrationBuilder.DropIndex(
                name: "IX_TestimonalImages_TestimonalImageId",
                table: "TestimonalImages");

            migrationBuilder.DropPrimaryKey(
                name: "PK_FeaturedProduct",
                table: "FeaturedProduct");

            migrationBuilder.DropColumn(
                name: "ProductId",
                table: "Testimonals");

            migrationBuilder.DropColumn(
                name: "ProfileImage",
                table: "Testimonals");

            migrationBuilder.DropColumn(
                name: "TestimonalImageId",
                table: "TestimonalImages");

            migrationBuilder.DropColumn(
                name: "Icon",
                table: "Services");

            migrationBuilder.RenameTable(
                name: "FeaturedProduct",
                newName: "FeaturedProducts");

            migrationBuilder.RenameIndex(
                name: "IX_FeaturedProduct_AboutId",
                table: "FeaturedProducts",
                newName: "IX_FeaturedProducts_AboutId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_FeaturedProducts",
                table: "FeaturedProducts",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_FeaturedProducts_About_AboutId",
                table: "FeaturedProducts",
                column: "AboutId",
                principalTable: "About",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FeaturedProducts_About_AboutId",
                table: "FeaturedProducts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_FeaturedProducts",
                table: "FeaturedProducts");

            migrationBuilder.RenameTable(
                name: "FeaturedProducts",
                newName: "FeaturedProduct");

            migrationBuilder.RenameIndex(
                name: "IX_FeaturedProducts_AboutId",
                table: "FeaturedProduct",
                newName: "IX_FeaturedProduct_AboutId");

            migrationBuilder.AddColumn<int>(
                name: "ProductId",
                table: "Testimonals",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "ProfileImage",
                table: "Testimonals",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "TestimonalImageId",
                table: "TestimonalImages",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Icon",
                table: "Services",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_FeaturedProduct",
                table: "FeaturedProduct",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Testimonals_ProductId",
                table: "Testimonals",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_TestimonalImages_TestimonalImageId",
                table: "TestimonalImages",
                column: "TestimonalImageId");

            migrationBuilder.AddForeignKey(
                name: "FK_FeaturedProduct_About_AboutId",
                table: "FeaturedProduct",
                column: "AboutId",
                principalTable: "About",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TestimonalImages_TestimonalImages_TestimonalImageId",
                table: "TestimonalImages",
                column: "TestimonalImageId",
                principalTable: "TestimonalImages",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Testimonals_Products_ProductId",
                table: "Testimonals",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
