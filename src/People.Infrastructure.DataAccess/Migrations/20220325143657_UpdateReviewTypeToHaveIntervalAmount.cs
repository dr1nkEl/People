using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace People.Infrastructure.DataAccess.Migrations
{
    public partial class UpdateReviewTypeToHaveIntervalAmount : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Interval",
                table: "ReviewTypes",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(string),
                oldType: "text",
                oldUnicode: false,
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "IntervalAmount",
                table: "ReviewTypes",
                type: "integer",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IntervalAmount",
                table: "ReviewTypes");

            migrationBuilder.AlterColumn<string>(
                name: "Interval",
                table: "ReviewTypes",
                type: "text",
                unicode: false,
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");
        }
    }
}
