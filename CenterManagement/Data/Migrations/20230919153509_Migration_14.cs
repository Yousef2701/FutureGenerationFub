using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CenterManagement.Data.Migrations
{
    public partial class Migration_14 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Numbre",
                table: "Lessons",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Numbre",
                table: "Lessons");
        }
    }
}
