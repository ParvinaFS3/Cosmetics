using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Cosmetics.Migrations
{
    public partial class ModelServiceChanged : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "FilePath",
                table: "Services",
                newName: "Icon");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Icon",
                table: "Services",
                newName: "FilePath");
        }
    }
}
