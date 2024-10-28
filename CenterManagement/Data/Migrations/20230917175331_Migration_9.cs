using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CenterManagement.Data.Migrations
{
    public partial class Migration_9 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "Discount",
                table: "Courses",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "NetPrice",
                table: "Courses",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "price",
                table: "Courses",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Discount",
                table: "Courses");

            migrationBuilder.DropColumn(
                name: "NetPrice",
                table: "Courses");

            migrationBuilder.DropColumn(
                name: "price",
                table: "Courses");
        }
    }
}
