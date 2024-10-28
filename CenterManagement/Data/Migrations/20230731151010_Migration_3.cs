using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CenterManagement.Data.Migrations
{
    public partial class Migration_3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_question",
                table: "question");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "question");

            migrationBuilder.AddColumn<int>(
                name: "Numbre",
                table: "question",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_question",
                table: "question",
                columns: new[] { "Numbre", "ExamId" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_question",
                table: "question");

            migrationBuilder.DropColumn(
                name: "Numbre",
                table: "question");

            migrationBuilder.AddColumn<string>(
                name: "Id",
                table: "question",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_question",
                table: "question",
                column: "Id");
        }
    }
}
