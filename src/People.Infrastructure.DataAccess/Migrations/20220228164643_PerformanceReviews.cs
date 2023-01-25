using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace People.Infrastructure.DataAccess.Migrations
{
    public partial class PerformanceReviews : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PerformanceReviewId",
                table: "AspNetUsers",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Notifications",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Text = table.Column<string>(type: "text", unicode: false, nullable: true),
                    UserId = table.Column<int>(type: "integer", nullable: false),
                    Data = table.Column<string>(type: "text", unicode: false, nullable: true),
                    NotificationType = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notifications", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Notifications_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ReviewTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", unicode: false, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReviewTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ReviewTemplates",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", unicode: false, nullable: true),
                    RelatedPositionId = table.Column<int>(type: "integer", nullable: true),
                    ReviewTypeId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReviewTemplates", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ReviewTemplates_Positions_RelatedPositionId",
                        column: x => x.RelatedPositionId,
                        principalTable: "Positions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ReviewTemplates_ReviewTypes_ReviewTypeId",
                        column: x => x.ReviewTypeId,
                        principalTable: "ReviewTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Answer",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    QuestionId = table.Column<int>(type: "integer", nullable: false),
                    Text = table.Column<string>(type: "text", unicode: false, nullable: true),
                    OptionId = table.Column<int>(type: "integer", nullable: true),
                    NoAnswer = table.Column<bool>(type: "boolean", nullable: false),
                    FeedbackId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Answer", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PerformanceReviews",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ReviewedUserId = table.Column<int>(type: "integer", nullable: false),
                    CreatedById = table.Column<int>(type: "integer", nullable: false),
                    ReviewedUserReplyId = table.Column<int>(type: "integer", nullable: true),
                    FinalReplyId = table.Column<int>(type: "integer", nullable: true),
                    Deadline = table.Column<DateOnly>(type: "date", nullable: true),
                    CompletedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PerformanceReviews", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PerformanceReviews_AspNetUsers_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PerformanceReviews_AspNetUsers_ReviewedUserId",
                        column: x => x.ReviewedUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Questions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    AnswerType = table.Column<int>(type: "integer", nullable: false),
                    Title = table.Column<string>(type: "text", unicode: false, nullable: true),
                    Order = table.Column<int>(type: "integer", nullable: false),
                    Options = table.Column<string>(type: "text", nullable: true),
                    UserReviewTemplateId = table.Column<int>(type: "integer", nullable: true),
                    FeedbackTemplateId = table.Column<int>(type: "integer", nullable: true),
                    UserReviewId = table.Column<int>(type: "integer", nullable: true),
                    FeedbackReviewId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Questions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Questions_PerformanceReviews_FeedbackReviewId",
                        column: x => x.FeedbackReviewId,
                        principalTable: "PerformanceReviews",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Questions_PerformanceReviews_UserReviewId",
                        column: x => x.UserReviewId,
                        principalTable: "PerformanceReviews",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Questions_ReviewTemplates_FeedbackTemplateId",
                        column: x => x.FeedbackTemplateId,
                        principalTable: "ReviewTemplates",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Questions_ReviewTemplates_UserReviewTemplateId",
                        column: x => x.UserReviewTemplateId,
                        principalTable: "ReviewTemplates",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Reply",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<int>(type: "integer", nullable: false),
                    SubmittedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    OptOut = table.Column<bool>(type: "boolean", nullable: false),
                    PerformanceReviewId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reply", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Reply_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Reply_PerformanceReviews_PerformanceReviewId",
                        column: x => x.PerformanceReviewId,
                        principalTable: "PerformanceReviews",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ReviewReminders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    MonthCount = table.Column<int>(type: "integer", nullable: false),
                    ReviewId = table.Column<int>(type: "integer", nullable: false),
                    LastTriggeredDay = table.Column<DateOnly>(type: "date", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReviewReminders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ReviewReminders_PerformanceReviews_ReviewId",
                        column: x => x.ReviewId,
                        principalTable: "PerformanceReviews",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_PerformanceReviewId",
                table: "AspNetUsers",
                column: "PerformanceReviewId");

            migrationBuilder.CreateIndex(
                name: "IX_Answer_FeedbackId",
                table: "Answer",
                column: "FeedbackId");

            migrationBuilder.CreateIndex(
                name: "IX_Answer_QuestionId",
                table: "Answer",
                column: "QuestionId");

            migrationBuilder.CreateIndex(
                name: "IX_Notifications_UserId",
                table: "Notifications",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_PerformanceReviews_CreatedById",
                table: "PerformanceReviews",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_PerformanceReviews_FinalReplyId",
                table: "PerformanceReviews",
                column: "FinalReplyId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PerformanceReviews_ReviewedUserId",
                table: "PerformanceReviews",
                column: "ReviewedUserId");

            migrationBuilder.CreateIndex(
                name: "IX_PerformanceReviews_ReviewedUserReplyId",
                table: "PerformanceReviews",
                column: "ReviewedUserReplyId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Questions_FeedbackReviewId",
                table: "Questions",
                column: "FeedbackReviewId");

            migrationBuilder.CreateIndex(
                name: "IX_Questions_FeedbackTemplateId",
                table: "Questions",
                column: "FeedbackTemplateId");

            migrationBuilder.CreateIndex(
                name: "IX_Questions_UserReviewId",
                table: "Questions",
                column: "UserReviewId");

            migrationBuilder.CreateIndex(
                name: "IX_Questions_UserReviewTemplateId",
                table: "Questions",
                column: "UserReviewTemplateId");

            migrationBuilder.CreateIndex(
                name: "IX_Reply_PerformanceReviewId",
                table: "Reply",
                column: "PerformanceReviewId");

            migrationBuilder.CreateIndex(
                name: "IX_Reply_UserId",
                table: "Reply",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_ReviewReminders_ReviewId",
                table: "ReviewReminders",
                column: "ReviewId");

            migrationBuilder.CreateIndex(
                name: "IX_ReviewTemplates_RelatedPositionId",
                table: "ReviewTemplates",
                column: "RelatedPositionId");

            migrationBuilder.CreateIndex(
                name: "IX_ReviewTemplates_ReviewTypeId",
                table: "ReviewTemplates",
                column: "ReviewTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_PerformanceReviews_PerformanceReviewId",
                table: "AspNetUsers",
                column: "PerformanceReviewId",
                principalTable: "PerformanceReviews",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Answer_Questions_QuestionId",
                table: "Answer",
                column: "QuestionId",
                principalTable: "Questions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Answer_Reply_FeedbackId",
                table: "Answer",
                column: "FeedbackId",
                principalTable: "Reply",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PerformanceReviews_Reply_FinalReplyId",
                table: "PerformanceReviews",
                column: "FinalReplyId",
                principalTable: "Reply",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PerformanceReviews_Reply_ReviewedUserReplyId",
                table: "PerformanceReviews",
                column: "ReviewedUserReplyId",
                principalTable: "Reply",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_PerformanceReviews_PerformanceReviewId",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_PerformanceReviews_Reply_FinalReplyId",
                table: "PerformanceReviews");

            migrationBuilder.DropForeignKey(
                name: "FK_PerformanceReviews_Reply_ReviewedUserReplyId",
                table: "PerformanceReviews");

            migrationBuilder.DropTable(
                name: "Answer");

            migrationBuilder.DropTable(
                name: "Notifications");

            migrationBuilder.DropTable(
                name: "ReviewReminders");

            migrationBuilder.DropTable(
                name: "Questions");

            migrationBuilder.DropTable(
                name: "ReviewTemplates");

            migrationBuilder.DropTable(
                name: "ReviewTypes");

            migrationBuilder.DropTable(
                name: "Reply");

            migrationBuilder.DropTable(
                name: "PerformanceReviews");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_PerformanceReviewId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "PerformanceReviewId",
                table: "AspNetUsers");
        }
    }
}
