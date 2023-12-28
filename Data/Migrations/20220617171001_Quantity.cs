using Microsoft.EntityFrameworkCore.Migrations;

namespace LibraryManagementSystem.Data.Migrations
{
    public partial class Quantity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Available",
                table: "Book");

            migrationBuilder.AddColumn<int>(
                name: "Quantity",
                table: "Book",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Quantity",
                table: "Book");

            migrationBuilder.AddColumn<string>(
                name: "Available",
                table: "Book",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
