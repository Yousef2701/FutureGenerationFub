using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CenterManagement.Data.Migrations
{
    public partial class Migration_20 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ChapterTitle",
                table: "Lessons",
                newName: "Month");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Month",
                table: "Lessons",
                newName: "ChapterTitle");
        }
    }
}
