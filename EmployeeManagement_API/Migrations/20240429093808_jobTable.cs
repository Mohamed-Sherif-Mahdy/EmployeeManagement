using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EmployeeManagement_API.Migrations
{
    /// <inheritdoc />
    public partial class jobTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "JobDescription",
                table: "Jobs");

            migrationBuilder.DropColumn(
                name: "JobRequirements",
                table: "Jobs");

            migrationBuilder.DropColumn(
                name: "JobSkills",
                table: "Jobs");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "JobDescription",
                table: "Jobs",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "JobRequirements",
                table: "Jobs",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "JobSkills",
                table: "Jobs",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
