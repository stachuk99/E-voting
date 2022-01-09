using Microsoft.EntityFrameworkCore.Migrations;

namespace EVoting_backend.DB.Migrations
{
    public partial class votesrelation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "FormId",
                table: "Vote",
                type: "int",
                nullable: false,
                defaultValue: 0);


            migrationBuilder.CreateIndex(
                name: "IX_Vote_FormId",
                table: "Vote",
                column: "FormId");

            migrationBuilder.AddForeignKey(
                name: "FK_Vote_Form_FormId",
                table: "Vote",
                column: "FormId",
                principalTable: "Form",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Vote_Form_FormId",
                table: "Vote");

            migrationBuilder.DropIndex(
                name: "IX_Vote_FormId",
                table: "Vote");

            migrationBuilder.DropColumn(
                name: "FormId",
                table: "Vote");

        }
    }
}
