using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DocumentServices.Migrations
{
    public partial class updateEditFlightNotoFlightId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FlightNo",
                table: "Documents");

            migrationBuilder.AddColumn<int>(
                name: "FlightId",
                table: "Documents",
                type: "int",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FlightId",
                table: "Documents");

            migrationBuilder.AddColumn<string>(
                name: "FlightNo",
                table: "Documents",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
