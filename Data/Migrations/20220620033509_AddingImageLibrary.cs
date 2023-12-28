using Microsoft.EntityFrameworkCore.Migrations;

namespace LibraryManagementSystem.Data.Migrations
{
    public partial class AddingImageLibrary : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FileName",
                table: "Book",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FileName",
                table: "Book");
        }
    }
}
