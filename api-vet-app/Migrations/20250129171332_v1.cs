using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace api_vet_app.Migrations
{
    /// <inheritdoc />
    public partial class v1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "VetId",
                table: "Vaccination",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "VetId",
                table: "ServiceItems",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "VetId",
                table: "Products",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "VetId",
                table: "ProductItems",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "VetId",
                table: "Payment",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "VetId",
                table: "Job",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "VetId",
                table: "ClientsPet",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "VetId",
                table: "Clients",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "VetId",
                table: "Attendings",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "VetId",
                table: "Vaccination");

            migrationBuilder.DropColumn(
                name: "VetId",
                table: "ServiceItems");

            migrationBuilder.DropColumn(
                name: "VetId",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "VetId",
                table: "ProductItems");

            migrationBuilder.DropColumn(
                name: "VetId",
                table: "Payment");

            migrationBuilder.DropColumn(
                name: "VetId",
                table: "Job");

            migrationBuilder.DropColumn(
                name: "VetId",
                table: "ClientsPet");

            migrationBuilder.DropColumn(
                name: "VetId",
                table: "Clients");

            migrationBuilder.DropColumn(
                name: "VetId",
                table: "Attendings");
        }
    }
}
