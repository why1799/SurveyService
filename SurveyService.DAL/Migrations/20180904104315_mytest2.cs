using Microsoft.EntityFrameworkCore.Migrations;

namespace SurveyService.DAL.Migrations
{
    public partial class mytest2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsCustomAnswer",
                table: "SurveyQuestion",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsCustomAnswer",
                table: "SurveyQuestion");
        }
    }
}
