using Microsoft.EntityFrameworkCore.Migrations;

namespace EVoting_backend.DB.Migrations
{
    public partial class addsubformname : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "SubForm",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "SubForm");
        }
    }
}
