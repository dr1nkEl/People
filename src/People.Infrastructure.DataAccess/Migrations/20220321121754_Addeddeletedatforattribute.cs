using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace People.Infrastructure.DataAccess.Migrations
{
    public partial class Addeddeletedatforattribute : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AttributeOption_Attributes_AttributeId",
                table: "AttributeOption");

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedAt",
                table: "Attributes",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_AttributeOption_Attributes_AttributeId",
                table: "AttributeOption",
                column: "AttributeId",
                principalTable: "Attributes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AttributeOption_Attributes_AttributeId",
                table: "AttributeOption");

            migrationBuilder.DropColumn(
                name: "DeletedAt",
                table: "Attributes");

            migrationBuilder.AddForeignKey(
                name: "FK_AttributeOption_Attributes_AttributeId",
                table: "AttributeOption",
                column: "AttributeId",
                principalTable: "Attributes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
