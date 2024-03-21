using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DocumentServices.Migrations
{
    public partial class updateAddFlightinDocument : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FlightNo",
                table: "Documents",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FlightNo",
                table: "Documents");
        }
    }
}
