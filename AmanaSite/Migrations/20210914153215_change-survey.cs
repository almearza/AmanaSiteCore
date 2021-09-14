using Microsoft.EntityFrameworkCore.Migrations;

namespace AmanaSite.Migrations
{
    public partial class changesurvey : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "LangCode",
                table: "SurveyData",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LangCode",
                table: "SurveyData");
        }
    }
}
