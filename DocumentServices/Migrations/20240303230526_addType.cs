using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DocumentServices.Migrations
{
    public partial class addType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Documents",
                keyColumn: "documentID",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Documents",
                keyColumn: "documentID",
                keyValue: 2);

            migrationBuilder.DropColumn(
                name: "typeDocument",
                table: "Documents");

            migrationBuilder.AddColumn<int>(
                name: "TypeDocumentId",
                table: "Documents",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "TypeDocuments",
                columns: table => new
                {
                    TypeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GroupName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Note = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Creator = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TypeDocuments", x => x.TypeId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Documents_TypeDocumentId",
                table: "Documents",
                column: "TypeDocumentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Documents_TypeDocuments_TypeDocumentId",
                table: "Documents",
                column: "TypeDocumentId",
                principalTable: "TypeDocuments",
                principalColumn: "TypeId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Documents_TypeDocuments_TypeDocumentId",
                table: "Documents");

            migrationBuilder.DropTable(
                name: "TypeDocuments");

            migrationBuilder.DropIndex(
                name: "IX_Documents_TypeDocumentId",
                table: "Documents");

            migrationBuilder.DropColumn(
                name: "TypeDocumentId",
                table: "Documents");

            migrationBuilder.AddColumn<string>(
                name: "typeDocument",
                table: "Documents",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.InsertData(
                table: "Documents",
                columns: new[] { "documentID", "FlightId", "Version", "creator", "filePath", "nameDocument", "typeDocument", "updateTime" },
                values: new object[] { 1, null, null, "uynnhi", null, "HelloWorld", "load Sumary", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.InsertData(
                table: "Documents",
                columns: new[] { "documentID", "FlightId", "Version", "creator", "filePath", "nameDocument", "typeDocument", "updateTime" },
                values: new object[] { 2, null, null, "kimenk", null, "HelloSadari", "load Sumary", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });
        }
    }
}
