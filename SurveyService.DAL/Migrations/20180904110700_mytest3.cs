using Microsoft.EntityFrameworkCore.Migrations;

namespace SurveyService.DAL.Migrations
{
    public partial class mytest3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Order",
                table: "OptionsForQuestions",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Order",
                table: "OptionsForQuestions");
        }
    }
}
