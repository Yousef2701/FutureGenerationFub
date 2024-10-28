using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CenterManagement.Data.Migrations
{
    public partial class Migration_24 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "StudendId",
                table: "Barcodes",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "StudendId",
                table: "Barcodes");
        }
    }
}
