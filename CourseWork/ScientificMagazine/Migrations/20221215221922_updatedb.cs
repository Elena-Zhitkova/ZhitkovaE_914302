using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ScientificMagazine.Migrations
{
    /// <inheritdoc />
    public partial class updatedb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Statuses_Applications_ApplicationId",
                table: "Statuses");

            migrationBuilder.DropIndex(
                name: "IX_Statuses_ApplicationId",
                table: "Statuses");

            migrationBuilder.DropColumn(
                name: "ApplicationId",
                table: "Statuses");

            migrationBuilder.RenameColumn(
                name: "RedactorReview",
                table: "Applications",
                newName: "RedactorReviewFile");

            migrationBuilder.RenameColumn(
                name: "CriticReview",
                table: "Applications",
                newName: "KeyWords");

            migrationBuilder.AddColumn<string>(
                name: "ArticleFile",
                table: "Applications",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "AuthorUserId",
                table: "Applications",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "CriticReviewFile",
                table: "Applications",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "GradeAntiplagiat",
                table: "Applications",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ArticleFile",
                table: "Applications");

            migrationBuilder.DropColumn(
                name: "AuthorUserId",
                table: "Applications");

            migrationBuilder.DropColumn(
                name: "CriticReviewFile",
                table: "Applications");

            migrationBuilder.DropColumn(
                name: "GradeAntiplagiat",
                table: "Applications");

            migrationBuilder.RenameColumn(
                name: "RedactorReviewFile",
                table: "Applications",
                newName: "RedactorReview");

            migrationBuilder.RenameColumn(
                name: "KeyWords",
                table: "Applications",
                newName: "CriticReview");

            migrationBuilder.AddColumn<int>(
                name: "ApplicationId",
                table: "Statuses",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Statuses_ApplicationId",
                table: "Statuses",
                column: "ApplicationId");

            migrationBuilder.AddForeignKey(
                name: "FK_Statuses_Applications_ApplicationId",
                table: "Statuses",
                column: "ApplicationId",
                principalTable: "Applications",
                principalColumn: "Id");
        }
    }
}
