using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace api_vet_app.Migrations
{
    /// <inheritdoc />
    public partial class v2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "VetId",
                table: "Payment");

            migrationBuilder.AlterColumn<string>(
                name: "VetId",
                table: "Vaccination",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<DateTime>(
                name: "NextVaccination",
                table: "Vaccination",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "AttendingId",
                table: "Vaccination",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ClientId",
                table: "Vaccination",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ClientPetId",
                table: "Vaccination",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Vaccination",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "VetId",
                table: "ServiceItems",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<decimal>(
                name: "Price",
                table: "ServiceItems",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "ServiceItems",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "AttendingId",
                table: "ServiceItems",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "JobId",
                table: "ServiceItems",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "ServiceItems",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Racas",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "VetId",
                table: "Products",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<decimal>(
                name: "Price",
                table: "Products",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Products",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "LabId",
                table: "Products",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Products",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "VetId",
                table: "ProductItems",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<decimal>(
                name: "Price",
                table: "ProductItems",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "ProductItems",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "AttendingId",
                table: "ProductItems",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ProductId",
                table: "ProductItems",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "ProductItems",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "TypePayment",
                table: "Payment",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "VetId",
                table: "Job",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<decimal>(
                name: "Price",
                table: "Job",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Job",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Job",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Especies",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "VetId",
                table: "ClientsPet",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "Sexo",
                table: "ClientsPet",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "ClientsPet",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Microchip",
                table: "ClientsPet",
                type: "nvarchar(15)",
                maxLength: 15,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ClientId",
                table: "ClientsPet",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "EspecieId",
                table: "ClientsPet",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "RacaId",
                table: "ClientsPet",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "ClientsPet",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "VetId",
                table: "Clients",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Clients",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "VetId",
                table: "Attendings",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Attendings",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ClientId",
                table: "Attendings",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ClientPetId",
                table: "Attendings",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PaymentId",
                table: "Attendings",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Attendings",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Lab",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    VetId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Lab", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Lab_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Vaccination_AttendingId",
                table: "Vaccination",
                column: "AttendingId");

            migrationBuilder.CreateIndex(
                name: "IX_Vaccination_ClientId",
                table: "Vaccination",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_Vaccination_ClientPetId",
                table: "Vaccination",
                column: "ClientPetId");

            migrationBuilder.CreateIndex(
                name: "IX_Vaccination_UserId",
                table: "Vaccination",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_ServiceItems_AttendingId",
                table: "ServiceItems",
                column: "AttendingId");

            migrationBuilder.CreateIndex(
                name: "IX_ServiceItems_JobId",
                table: "ServiceItems",
                column: "JobId");

            migrationBuilder.CreateIndex(
                name: "IX_ServiceItems_UserId",
                table: "ServiceItems",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_LabId",
                table: "Products",
                column: "LabId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_UserId",
                table: "Products",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductItems_AttendingId",
                table: "ProductItems",
                column: "AttendingId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductItems_ProductId",
                table: "ProductItems",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductItems_UserId",
                table: "ProductItems",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Job_UserId",
                table: "Job",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_ClientsPet_ClientId",
                table: "ClientsPet",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_ClientsPet_EspecieId",
                table: "ClientsPet",
                column: "EspecieId");

            migrationBuilder.CreateIndex(
                name: "IX_ClientsPet_RacaId",
                table: "ClientsPet",
                column: "RacaId");

            migrationBuilder.CreateIndex(
                name: "IX_ClientsPet_UserId",
                table: "ClientsPet",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Attendings_ClientId",
                table: "Attendings",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_Attendings_ClientPetId",
                table: "Attendings",
                column: "ClientPetId");

            migrationBuilder.CreateIndex(
                name: "IX_Attendings_PaymentId",
                table: "Attendings",
                column: "PaymentId");

            migrationBuilder.CreateIndex(
                name: "IX_Attendings_UserId",
                table: "Attendings",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Lab_UserId",
                table: "Lab",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Attendings_AspNetUsers_UserId",
                table: "Attendings",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Attendings_ClientsPet_ClientPetId",
                table: "Attendings",
                column: "ClientPetId",
                principalTable: "ClientsPet",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Attendings_Clients_ClientId",
                table: "Attendings",
                column: "ClientId",
                principalTable: "Clients",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Attendings_Payment_PaymentId",
                table: "Attendings",
                column: "PaymentId",
                principalTable: "Payment",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ClientsPet_AspNetUsers_UserId",
                table: "ClientsPet",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ClientsPet_Clients_ClientId",
                table: "ClientsPet",
                column: "ClientId",
                principalTable: "Clients",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ClientsPet_Especies_EspecieId",
                table: "ClientsPet",
                column: "EspecieId",
                principalTable: "Especies",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ClientsPet_Racas_RacaId",
                table: "ClientsPet",
                column: "RacaId",
                principalTable: "Racas",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Job_AspNetUsers_UserId",
                table: "Job",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductItems_AspNetUsers_UserId",
                table: "ProductItems",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductItems_Attendings_AttendingId",
                table: "ProductItems",
                column: "AttendingId",
                principalTable: "Attendings",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductItems_Products_ProductId",
                table: "ProductItems",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_AspNetUsers_UserId",
                table: "Products",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Lab_LabId",
                table: "Products",
                column: "LabId",
                principalTable: "Lab",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ServiceItems_AspNetUsers_UserId",
                table: "ServiceItems",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ServiceItems_Attendings_AttendingId",
                table: "ServiceItems",
                column: "AttendingId",
                principalTable: "Attendings",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ServiceItems_Job_JobId",
                table: "ServiceItems",
                column: "JobId",
                principalTable: "Job",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Vaccination_AspNetUsers_UserId",
                table: "Vaccination",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Vaccination_Attendings_AttendingId",
                table: "Vaccination",
                column: "AttendingId",
                principalTable: "Attendings",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Vaccination_ClientsPet_ClientPetId",
                table: "Vaccination",
                column: "ClientPetId",
                principalTable: "ClientsPet",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Vaccination_Clients_ClientId",
                table: "Vaccination",
                column: "ClientId",
                principalTable: "Clients",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Attendings_AspNetUsers_UserId",
                table: "Attendings");

            migrationBuilder.DropForeignKey(
                name: "FK_Attendings_ClientsPet_ClientPetId",
                table: "Attendings");

            migrationBuilder.DropForeignKey(
                name: "FK_Attendings_Clients_ClientId",
                table: "Attendings");

            migrationBuilder.DropForeignKey(
                name: "FK_Attendings_Payment_PaymentId",
                table: "Attendings");

            migrationBuilder.DropForeignKey(
                name: "FK_ClientsPet_AspNetUsers_UserId",
                table: "ClientsPet");

            migrationBuilder.DropForeignKey(
                name: "FK_ClientsPet_Clients_ClientId",
                table: "ClientsPet");

            migrationBuilder.DropForeignKey(
                name: "FK_ClientsPet_Especies_EspecieId",
                table: "ClientsPet");

            migrationBuilder.DropForeignKey(
                name: "FK_ClientsPet_Racas_RacaId",
                table: "ClientsPet");

            migrationBuilder.DropForeignKey(
                name: "FK_Job_AspNetUsers_UserId",
                table: "Job");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductItems_AspNetUsers_UserId",
                table: "ProductItems");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductItems_Attendings_AttendingId",
                table: "ProductItems");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductItems_Products_ProductId",
                table: "ProductItems");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_AspNetUsers_UserId",
                table: "Products");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_Lab_LabId",
                table: "Products");

            migrationBuilder.DropForeignKey(
                name: "FK_ServiceItems_AspNetUsers_UserId",
                table: "ServiceItems");

            migrationBuilder.DropForeignKey(
                name: "FK_ServiceItems_Attendings_AttendingId",
                table: "ServiceItems");

            migrationBuilder.DropForeignKey(
                name: "FK_ServiceItems_Job_JobId",
                table: "ServiceItems");

            migrationBuilder.DropForeignKey(
                name: "FK_Vaccination_AspNetUsers_UserId",
                table: "Vaccination");

            migrationBuilder.DropForeignKey(
                name: "FK_Vaccination_Attendings_AttendingId",
                table: "Vaccination");

            migrationBuilder.DropForeignKey(
                name: "FK_Vaccination_ClientsPet_ClientPetId",
                table: "Vaccination");

            migrationBuilder.DropForeignKey(
                name: "FK_Vaccination_Clients_ClientId",
                table: "Vaccination");

            migrationBuilder.DropTable(
                name: "Lab");

            migrationBuilder.DropIndex(
                name: "IX_Vaccination_AttendingId",
                table: "Vaccination");

            migrationBuilder.DropIndex(
                name: "IX_Vaccination_ClientId",
                table: "Vaccination");

            migrationBuilder.DropIndex(
                name: "IX_Vaccination_ClientPetId",
                table: "Vaccination");

            migrationBuilder.DropIndex(
                name: "IX_Vaccination_UserId",
                table: "Vaccination");

            migrationBuilder.DropIndex(
                name: "IX_ServiceItems_AttendingId",
                table: "ServiceItems");

            migrationBuilder.DropIndex(
                name: "IX_ServiceItems_JobId",
                table: "ServiceItems");

            migrationBuilder.DropIndex(
                name: "IX_ServiceItems_UserId",
                table: "ServiceItems");

            migrationBuilder.DropIndex(
                name: "IX_Products_LabId",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Products_UserId",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_ProductItems_AttendingId",
                table: "ProductItems");

            migrationBuilder.DropIndex(
                name: "IX_ProductItems_ProductId",
                table: "ProductItems");

            migrationBuilder.DropIndex(
                name: "IX_ProductItems_UserId",
                table: "ProductItems");

            migrationBuilder.DropIndex(
                name: "IX_Job_UserId",
                table: "Job");

            migrationBuilder.DropIndex(
                name: "IX_ClientsPet_ClientId",
                table: "ClientsPet");

            migrationBuilder.DropIndex(
                name: "IX_ClientsPet_EspecieId",
                table: "ClientsPet");

            migrationBuilder.DropIndex(
                name: "IX_ClientsPet_RacaId",
                table: "ClientsPet");

            migrationBuilder.DropIndex(
                name: "IX_ClientsPet_UserId",
                table: "ClientsPet");

            migrationBuilder.DropIndex(
                name: "IX_Attendings_ClientId",
                table: "Attendings");

            migrationBuilder.DropIndex(
                name: "IX_Attendings_ClientPetId",
                table: "Attendings");

            migrationBuilder.DropIndex(
                name: "IX_Attendings_PaymentId",
                table: "Attendings");

            migrationBuilder.DropIndex(
                name: "IX_Attendings_UserId",
                table: "Attendings");

            migrationBuilder.DropColumn(
                name: "AttendingId",
                table: "Vaccination");

            migrationBuilder.DropColumn(
                name: "ClientId",
                table: "Vaccination");

            migrationBuilder.DropColumn(
                name: "ClientPetId",
                table: "Vaccination");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Vaccination");

            migrationBuilder.DropColumn(
                name: "AttendingId",
                table: "ServiceItems");

            migrationBuilder.DropColumn(
                name: "JobId",
                table: "ServiceItems");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "ServiceItems");

            migrationBuilder.DropColumn(
                name: "LabId",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "AttendingId",
                table: "ProductItems");

            migrationBuilder.DropColumn(
                name: "ProductId",
                table: "ProductItems");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "ProductItems");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Job");

            migrationBuilder.DropColumn(
                name: "ClientId",
                table: "ClientsPet");

            migrationBuilder.DropColumn(
                name: "EspecieId",
                table: "ClientsPet");

            migrationBuilder.DropColumn(
                name: "RacaId",
                table: "ClientsPet");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "ClientsPet");

            migrationBuilder.DropColumn(
                name: "ClientId",
                table: "Attendings");

            migrationBuilder.DropColumn(
                name: "ClientPetId",
                table: "Attendings");

            migrationBuilder.DropColumn(
                name: "PaymentId",
                table: "Attendings");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Attendings");

            migrationBuilder.AlterColumn<int>(
                name: "VetId",
                table: "Vaccination",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<DateTime>(
                name: "NextVaccination",
                table: "Vaccination",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<int>(
                name: "VetId",
                table: "ServiceItems",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<decimal>(
                name: "Price",
                table: "ServiceItems",
                type: "decimal(18,2)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "ServiceItems",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Racas",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<int>(
                name: "VetId",
                table: "Products",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<decimal>(
                name: "Price",
                table: "Products",
                type: "decimal(18,2)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Products",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<int>(
                name: "VetId",
                table: "ProductItems",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<decimal>(
                name: "Price",
                table: "ProductItems",
                type: "decimal(18,2)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "ProductItems",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "TypePayment",
                table: "Payment",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.AddColumn<int>(
                name: "VetId",
                table: "Payment",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<int>(
                name: "VetId",
                table: "Job",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<decimal>(
                name: "Price",
                table: "Job",
                type: "decimal(18,2)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Job",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Especies",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<int>(
                name: "VetId",
                table: "ClientsPet",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Sexo",
                table: "ClientsPet",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "ClientsPet",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Microchip",
                table: "ClientsPet",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(15)",
                oldMaxLength: 15);

            migrationBuilder.AlterColumn<int>(
                name: "VetId",
                table: "Clients",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Clients",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<int>(
                name: "VetId",
                table: "Attendings",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Attendings",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");
        }
    }
}
