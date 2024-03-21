using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DocumentServices.Migrations
{
    public partial class ThemPhanQUyen : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "phanQuyenTaiLieus",
                columns: table => new
                {
                    TypeDocumentID = table.Column<int>(type: "int", nullable: false),
                    NameRole = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Claims = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_phanQuyenTaiLieus", x => new { x.TypeDocumentID, x.NameRole });
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "phanQuyenTaiLieus");
        }
    }
}
