using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class updateTeam : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TeamID",
                table: "Accounts",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Accounts_TeamID",
                table: "Accounts",
                column: "TeamID");

            migrationBuilder.AddForeignKey(
                name: "FK_Accounts_Teams_TeamID",
                table: "Accounts",
                column: "TeamID",
                principalTable: "Teams",
                principalColumn: "TeamID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Accounts_Teams_TeamID",
                table: "Accounts");

            migrationBuilder.DropIndex(
                name: "IX_Accounts_TeamID",
                table: "Accounts");

            migrationBuilder.DropColumn(
                name: "TeamID",
                table: "Accounts");
        }
    }
}
