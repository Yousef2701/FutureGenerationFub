using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CenterManagement.Data.Migrations
{
    public partial class Migration_15 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "question");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "question",
                columns: table => new
                {
                    Numbre = table.Column<int>(type: "int", nullable: false),
                    LessonId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CorrectChoose = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FourthChoose = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FristChoose = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Quest = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SecondChoose = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ThirdChoose = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_question", x => new { x.Numbre, x.LessonId });
                    table.ForeignKey(
                        name: "FK_question_Lessons_LessonId",
                        column: x => x.LessonId,
                        principalTable: "Lessons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_question_LessonId",
                table: "question",
                column: "LessonId");
        }
    }
}
