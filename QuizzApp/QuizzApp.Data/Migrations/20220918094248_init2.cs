using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace QuizzApp.Data.Migrations
{
    public partial class init2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<Guid>(
                name: "AnswerId",
                table: "TestQuestionAnswers",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<DateTime>(
                name: "PostedOn",
                table: "Posts",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2022, 9, 18, 16, 42, 48, 46, DateTimeKind.Local).AddTicks(7498),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2022, 8, 27, 19, 3, 40, 505, DateTimeKind.Local).AddTicks(1616));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedTime",
                table: "Courses",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 9, 18, 16, 42, 48, 45, DateTimeKind.Local).AddTicks(6077),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 8, 27, 19, 3, 40, 504, DateTimeKind.Local).AddTicks(159));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CommentTime",
                table: "Comments",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 9, 18, 16, 42, 48, 42, DateTimeKind.Local).AddTicks(1369),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 8, 27, 19, 3, 40, 500, DateTimeKind.Local).AddTicks(4007));

            migrationBuilder.AddColumn<string>(
                name: "Extend",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_TestQuestionAnswers_AnswerId",
                table: "TestQuestionAnswers",
                column: "AnswerId");

            migrationBuilder.CreateIndex(
                name: "IX_TestQuestionAnswers_QuestionId",
                table: "TestQuestionAnswers",
                column: "QuestionId");

            migrationBuilder.AddForeignKey(
                name: "FK_TestQuestionAnswers_Options_AnswerId",
                table: "TestQuestionAnswers",
                column: "AnswerId",
                principalTable: "Options",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TestQuestionAnswers_Questions_QuestionId",
                table: "TestQuestionAnswers",
                column: "QuestionId",
                principalTable: "Questions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TestQuestionAnswers_Options_AnswerId",
                table: "TestQuestionAnswers");

            migrationBuilder.DropForeignKey(
                name: "FK_TestQuestionAnswers_Questions_QuestionId",
                table: "TestQuestionAnswers");

            migrationBuilder.DropIndex(
                name: "IX_TestQuestionAnswers_AnswerId",
                table: "TestQuestionAnswers");

            migrationBuilder.DropIndex(
                name: "IX_TestQuestionAnswers_QuestionId",
                table: "TestQuestionAnswers");

            migrationBuilder.DropColumn(
                name: "Extend",
                table: "AspNetUsers");

            migrationBuilder.AlterColumn<Guid>(
                name: "AnswerId",
                table: "TestQuestionAnswers",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "PostedOn",
                table: "Posts",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2022, 8, 27, 19, 3, 40, 505, DateTimeKind.Local).AddTicks(1616),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2022, 9, 18, 16, 42, 48, 46, DateTimeKind.Local).AddTicks(7498));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedTime",
                table: "Courses",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 8, 27, 19, 3, 40, 504, DateTimeKind.Local).AddTicks(159),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 9, 18, 16, 42, 48, 45, DateTimeKind.Local).AddTicks(6077));

            migrationBuilder.AlterColumn<DateTime>(
                name: "CommentTime",
                table: "Comments",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 8, 27, 19, 3, 40, 500, DateTimeKind.Local).AddTicks(4007),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 9, 18, 16, 42, 48, 42, DateTimeKind.Local).AddTicks(1369));
        }
    }
}
