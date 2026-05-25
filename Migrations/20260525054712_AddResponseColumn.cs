using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Authservice.Migrations
{
    /// <inheritdoc />
    public partial class AddResponseColumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Answer_Feedbacks_FeedbackId",
                table: "Answer");

            migrationBuilder.DropColumn(
                name: "OptionId",
                table: "Answer");

            migrationBuilder.DropColumn(
                name: "TextValue",
                table: "Answer");

            migrationBuilder.AlterColumn<Guid>(
                name: "FeedbackId",
                table: "Answer",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Response",
                table: "Answer",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Answer_QuestionId",
                table: "Answer",
                column: "QuestionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Answer_Feedbacks_FeedbackId",
                table: "Answer",
                column: "FeedbackId",
                principalTable: "Feedbacks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Answer_Questions_QuestionId",
                table: "Answer",
                column: "QuestionId",
                principalTable: "Questions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Answer_Feedbacks_FeedbackId",
                table: "Answer");

            migrationBuilder.DropForeignKey(
                name: "FK_Answer_Questions_QuestionId",
                table: "Answer");

            migrationBuilder.DropIndex(
                name: "IX_Answer_QuestionId",
                table: "Answer");

            migrationBuilder.DropColumn(
                name: "Response",
                table: "Answer");

            migrationBuilder.AlterColumn<Guid>(
                name: "FeedbackId",
                table: "Answer",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddColumn<Guid>(
                name: "OptionId",
                table: "Answer",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TextValue",
                table: "Answer",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Answer_Feedbacks_FeedbackId",
                table: "Answer",
                column: "FeedbackId",
                principalTable: "Feedbacks",
                principalColumn: "Id");
        }
    }
}
