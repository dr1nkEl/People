using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace People.Infrastructure.DataAccess.Migrations
{
    public partial class ChangeRoleCrmIdType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "CrmRoleId",
                table: "AspNetRoles",
                type: "text",
                unicode: false,
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "CrmRoleId",
                table: "AspNetRoles",
                type: "integer",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text",
                oldUnicode: false,
                oldNullable: true);
        }
    }
}
