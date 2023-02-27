using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ScientificMagazine.Migrations
{
    /// <inheritdoc />
    public partial class AddStatusToDatabase123 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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
        }
    }
}
