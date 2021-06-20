using Microsoft.EntityFrameworkCore.Migrations;

namespace AmanaSite.Migrations
{
    public partial class removetoolimgads : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ToolImgUrl",
                table: "SlidersShows");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ToolImgUrl",
                table: "SlidersShows",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
