using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace People.Infrastructure.DataAccess.Migrations
{
    public partial class MadeIntervalNullableOfReviewTypeEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Interval",
                table: "ReviewTypes",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Interval",
                table: "ReviewTypes",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);
        }
    }
}
