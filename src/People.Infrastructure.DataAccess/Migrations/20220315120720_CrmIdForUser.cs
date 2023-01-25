using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace People.Infrastructure.DataAccess.Migrations
{
    public partial class CrmIdForUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CrmId",
                table: "AspNetUsers",
                type: "integer",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CrmId",
                table: "AspNetUsers");
        }
    }
}
