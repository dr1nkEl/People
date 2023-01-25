using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace People.Infrastructure.DataAccess.Migrations
{
    public partial class AddedDeletedAtFieldForReviewTemplateAndIntervalForReviewType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Interval",
                table: "ReviewTypes",
                type: "integer",
                unicode: false,
                nullable: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedAt",
                table: "ReviewTemplates",
                type: "timestamp with time zone",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Interval",
                table: "ReviewTypes");

            migrationBuilder.DropColumn(
                name: "DeletedAt",
                table: "ReviewTemplates");
        }
    }
}
