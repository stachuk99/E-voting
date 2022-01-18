using Microsoft.EntityFrameworkCore.Migrations;

namespace EVoting_backend.DB.Migrations
{
    public partial class ivandsecret : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Secret",
                table: "Vote",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "iv",
                table: "Vote",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "iv",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Secret",
                table: "Vote");

            migrationBuilder.DropColumn(
                name: "iv",
                table: "Vote");

            migrationBuilder.DropColumn(
                name: "iv",
                table: "Users");
        }
    }
}
