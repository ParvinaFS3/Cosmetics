using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Cosmetics.Migrations
{
    public partial class dbContextCategorypropCount : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Count",
                table: "Categories",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Count",
                table: "Categories");
        }
    }
}
