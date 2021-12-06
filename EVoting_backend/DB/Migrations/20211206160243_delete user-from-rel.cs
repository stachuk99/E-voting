using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EVoting_backend.DB.Migrations
{
    public partial class deleteuserfromrel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserFormRelation");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UserFormRelation",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FormId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FormId1 = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserFormRelation", x => x.ID);
                    table.ForeignKey(
                        name: "FK_UserFormRelation_Form_FormId1",
                        column: x => x.FormId1,
                        principalTable: "Form",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserFormRelation_FormId1",
                table: "UserFormRelation",
                column: "FormId1");
        }
    }
}
