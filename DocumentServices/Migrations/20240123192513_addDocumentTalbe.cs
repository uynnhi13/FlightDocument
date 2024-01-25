using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DocumentServices.Migrations
{
    public partial class addDocumentTalbe : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Documents",
                columns: table => new
                {
                    documentID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nameDocument = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    updateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    creator = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    typeDocument = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Documents", x => x.documentID);
                });

            migrationBuilder.InsertData(
                table: "Documents",
                columns: new[] { "documentID", "creator", "nameDocument", "typeDocument", "updateTime" },
                values: new object[] { 1, "uynnhi", "HelloWorld", "load Sumary", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Documents");
        }
    }
}
