using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EventRegistration.Infra.Migrations
{
    public partial class newMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Participants_Companies_CompanyId",
                table: "Participants");

            migrationBuilder.DropForeignKey(
                name: "FK_Participants_Persons_PersonId",
                table: "Participants");

            migrationBuilder.DropTable(name: "Companies");
            migrationBuilder.DropTable(name: "Persons");

            migrationBuilder.DropIndex(name: "IX_Participants_CompanyId", table: "Participants");
            migrationBuilder.DropIndex(name: "IX_Participants_PersonId", table: "Participants");

            migrationBuilder.DropColumn(name: "CompanyId", table: "Participants");
            migrationBuilder.DropColumn(name: "PersonId", table: "Participants");

            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "Participants",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "IsPerson",
                table: "Participants",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "LastName",
                table: "Participants",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "LegalName",
                table: "Participants",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "ParticipantsCount",
                table: "Participants",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "PersonalIdCode",
                table: "Participants",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "RegistrationCode",
                table: "Participants",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(name: "FirstName", table: "Participants");
            migrationBuilder.DropColumn(name: "IsPerson", table: "Participants");
            migrationBuilder.DropColumn(name: "LastName", table: "Participants");
            migrationBuilder.DropColumn(name: "LegalName", table: "Participants");
            migrationBuilder.DropColumn(name: "ParticipantsCount", table: "Participants");
            migrationBuilder.DropColumn(name: "PersonalIdCode", table: "Participants");
            migrationBuilder.DropColumn(name: "RegistrationCode", table: "Participants");

            migrationBuilder.AddColumn<int>(
                name: "CompanyId",
                table: "Participants",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PersonId",
                table: "Participants",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Companies",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LegalName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ParticipantsCount = table.Column<int>(type: "int", nullable: false),
                    RegistrationCode = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Companies", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Persons",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PersonalIdCode = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Persons", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Participants_CompanyId",
                table: "Participants",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_Participants_PersonId",
                table: "Participants",
                column: "PersonId");
        }
    }
}
