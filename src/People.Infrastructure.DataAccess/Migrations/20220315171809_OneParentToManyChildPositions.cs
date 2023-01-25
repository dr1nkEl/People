using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace People.Infrastructure.DataAccess.Migrations
{
    public partial class OneParentToManyChildPositions : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PositionPosition");

            migrationBuilder.AddColumn<int>(
                name: "ParentPositionId",
                table: "Positions",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Positions_ParentPositionId",
                table: "Positions",
                column: "ParentPositionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Positions_Positions_ParentPositionId",
                table: "Positions",
                column: "ParentPositionId",
                principalTable: "Positions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Positions_Positions_ParentPositionId",
                table: "Positions");

            migrationBuilder.DropIndex(
                name: "IX_Positions_ParentPositionId",
                table: "Positions");

            migrationBuilder.DropColumn(
                name: "ParentPositionId",
                table: "Positions");

            migrationBuilder.CreateTable(
                name: "PositionPosition",
                columns: table => new
                {
                    ChildPositionsId = table.Column<int>(type: "integer", nullable: false),
                    ParentPositionsId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PositionPosition", x => new { x.ChildPositionsId, x.ParentPositionsId });
                    table.ForeignKey(
                        name: "FK_PositionPosition_Positions_ChildPositionsId",
                        column: x => x.ChildPositionsId,
                        principalTable: "Positions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PositionPosition_Positions_ParentPositionsId",
                        column: x => x.ParentPositionsId,
                        principalTable: "Positions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PositionPosition_ParentPositionsId",
                table: "PositionPosition",
                column: "ParentPositionsId");
        }
    }
}
