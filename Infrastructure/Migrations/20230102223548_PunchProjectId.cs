using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class PunchProjectId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "EmployeeId",
                table: "Punches",
                newName: "ProjectId");

            migrationBuilder.AddColumn<int>(
                name: "AccountId",
                table: "Punches",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AccountId",
                table: "Punches");

            migrationBuilder.RenameColumn(
                name: "ProjectId",
                table: "Punches",
                newName: "EmployeeId");
        }
    }
}
