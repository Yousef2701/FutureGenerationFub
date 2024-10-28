using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CenterManagement.Data.Migrations
{
    public partial class Migration_4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Degree",
                table: "exams");

            migrationBuilder.AddColumn<int>(
                name: "QuestionNumbre",
                table: "exams",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "QuestionNumbre",
                table: "exams");

            migrationBuilder.AddColumn<string>(
                name: "Degree",
                table: "exams",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
