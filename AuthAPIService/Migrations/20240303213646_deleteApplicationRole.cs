using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AuthAPIService.Migrations
{
    public partial class deleteApplicationRole : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetRoles_RoleClaims_ClaimId",
                table: "AspNetRoles");

            migrationBuilder.DropIndex(
                name: "IX_AspNetRoles_ClaimId",
                table: "AspNetRoles");

            migrationBuilder.DropColumn(
                name: "ClaimId",
                table: "AspNetRoles");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "AspNetRoles");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ClaimId",
                table: "AspNetRoles",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "AspNetRoles",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoles_ClaimId",
                table: "AspNetRoles",
                column: "ClaimId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetRoles_RoleClaims_ClaimId",
                table: "AspNetRoles",
                column: "ClaimId",
                principalTable: "RoleClaims",
                principalColumn: "ClaimId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
