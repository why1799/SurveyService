using Microsoft.EntityFrameworkCore.Migrations;

namespace SurveyService.DAL.Migrations
{
    public partial class test2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OptionsForQuestions_Options_OptionId",
                table: "OptionsForQuestions");

            migrationBuilder.DropForeignKey(
                name: "FK_OptionsForQuestions_Questions_UserAnswerId",
                table: "OptionsForQuestions");

            migrationBuilder.DropForeignKey(
                name: "FK_Questions_SurveyQuestions_QuestionId",
                table: "Questions");

            migrationBuilder.DropForeignKey(
                name: "FK_Questions_Users_UserId",
                table: "Questions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Questions",
                table: "Questions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_OptionsForQuestions",
                table: "OptionsForQuestions");

            migrationBuilder.RenameTable(
                name: "Questions",
                newName: "UserAnswers");

            migrationBuilder.RenameTable(
                name: "OptionsForQuestions",
                newName: "OptionsForAnswers");

            migrationBuilder.RenameIndex(
                name: "IX_Questions_UserId",
                table: "UserAnswers",
                newName: "IX_UserAnswers_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Questions_QuestionId",
                table: "UserAnswers",
                newName: "IX_UserAnswers_QuestionId");

            migrationBuilder.RenameIndex(
                name: "IX_OptionsForQuestions_UserAnswerId",
                table: "OptionsForAnswers",
                newName: "IX_OptionsForAnswers_UserAnswerId");

            migrationBuilder.RenameIndex(
                name: "IX_OptionsForQuestions_OptionId",
                table: "OptionsForAnswers",
                newName: "IX_OptionsForAnswers_OptionId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserAnswers",
                table: "UserAnswers",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_OptionsForAnswers",
                table: "OptionsForAnswers",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_OptionsForAnswers_Options_OptionId",
                table: "OptionsForAnswers",
                column: "OptionId",
                principalTable: "Options",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_OptionsForAnswers_UserAnswers_UserAnswerId",
                table: "OptionsForAnswers",
                column: "UserAnswerId",
                principalTable: "UserAnswers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_UserAnswers_SurveyQuestions_QuestionId",
                table: "UserAnswers",
                column: "QuestionId",
                principalTable: "SurveyQuestions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_UserAnswers_Users_UserId",
                table: "UserAnswers",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OptionsForAnswers_Options_OptionId",
                table: "OptionsForAnswers");

            migrationBuilder.DropForeignKey(
                name: "FK_OptionsForAnswers_UserAnswers_UserAnswerId",
                table: "OptionsForAnswers");

            migrationBuilder.DropForeignKey(
                name: "FK_UserAnswers_SurveyQuestions_QuestionId",
                table: "UserAnswers");

            migrationBuilder.DropForeignKey(
                name: "FK_UserAnswers_Users_UserId",
                table: "UserAnswers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserAnswers",
                table: "UserAnswers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_OptionsForAnswers",
                table: "OptionsForAnswers");

            migrationBuilder.RenameTable(
                name: "UserAnswers",
                newName: "Questions");

            migrationBuilder.RenameTable(
                name: "OptionsForAnswers",
                newName: "OptionsForQuestions");

            migrationBuilder.RenameIndex(
                name: "IX_UserAnswers_UserId",
                table: "Questions",
                newName: "IX_Questions_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_UserAnswers_QuestionId",
                table: "Questions",
                newName: "IX_Questions_QuestionId");

            migrationBuilder.RenameIndex(
                name: "IX_OptionsForAnswers_UserAnswerId",
                table: "OptionsForQuestions",
                newName: "IX_OptionsForQuestions_UserAnswerId");

            migrationBuilder.RenameIndex(
                name: "IX_OptionsForAnswers_OptionId",
                table: "OptionsForQuestions",
                newName: "IX_OptionsForQuestions_OptionId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Questions",
                table: "Questions",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_OptionsForQuestions",
                table: "OptionsForQuestions",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_OptionsForQuestions_Options_OptionId",
                table: "OptionsForQuestions",
                column: "OptionId",
                principalTable: "Options",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_OptionsForQuestions_Questions_UserAnswerId",
                table: "OptionsForQuestions",
                column: "UserAnswerId",
                principalTable: "Questions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Questions_SurveyQuestions_QuestionId",
                table: "Questions",
                column: "QuestionId",
                principalTable: "SurveyQuestions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Questions_Users_UserId",
                table: "Questions",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
