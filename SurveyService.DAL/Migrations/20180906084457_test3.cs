using Microsoft.EntityFrameworkCore.Migrations;

namespace SurveyService.DAL.Migrations
{
    public partial class test3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Surveys_Users_CreategById",
                table: "Surveys");

            migrationBuilder.DropIndex(
                name: "IX_Surveys_CreategById",
                table: "Surveys");

            migrationBuilder.DropColumn(
                name: "CreategById",
                table: "Surveys");

            migrationBuilder.AlterColumn<string>(
                name: "CreatedById",
                table: "Surveys",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Surveys_CreatedById",
                table: "Surveys",
                column: "CreatedById");

            migrationBuilder.AddForeignKey(
                name: "FK_Surveys_Users_CreatedById",
                table: "Surveys",
                column: "CreatedById",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Surveys_Users_CreatedById",
                table: "Surveys");

            migrationBuilder.DropIndex(
                name: "IX_Surveys_CreatedById",
                table: "Surveys");

            migrationBuilder.AlterColumn<string>(
                name: "CreatedById",
                table: "Surveys",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreategById",
                table: "Surveys",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Surveys_CreategById",
                table: "Surveys",
                column: "CreategById");

            migrationBuilder.AddForeignKey(
                name: "FK_Surveys_Users_CreategById",
                table: "Surveys",
                column: "CreategById",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
