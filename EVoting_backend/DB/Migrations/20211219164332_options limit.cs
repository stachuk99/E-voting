using Microsoft.EntityFrameworkCore.Migrations;

namespace EVoting_backend.DB.Migrations
{
    public partial class optionslimit : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Type",
                table: "SubForm",
                newName: "ChoicesLimit");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ChoicesLimit",
                table: "SubForm",
                newName: "Type");
        }
    }
}
