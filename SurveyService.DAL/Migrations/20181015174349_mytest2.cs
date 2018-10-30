using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SurveyService.DAL.Migrations
{
    public partial class mytest2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte[]>(
                name: "Image",
                table: "Surveys",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Image",
                table: "Surveys");
        }
    }
}
