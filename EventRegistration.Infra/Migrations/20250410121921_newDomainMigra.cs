using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EventRegistration.Infra.Migrations
{
    /// <inheritdoc />
    public partial class newDomainMigra : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Participants_Events_EventId",
                table: "Participants");

            migrationBuilder.DropIndex(
                name: "IX_Participants_EventId",
                table: "Participants");

            migrationBuilder.DropColumn(
                name: "EventId",
                table: "Participants");

            migrationBuilder.DropColumn(
                name: "Type",
                table: "Participants");

            migrationBuilder.AlterColumn<int>(
                name: "PaymentMethod",
                table: "Participants",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "AdditionalInfo",
                table: "Participants",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "Participants",
                type: "nvarchar(13)",
                maxLength: 13,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "Participants",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastName",
                table: "Participants",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LegalName",
                table: "Participants",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ParticipantsCount",
                table: "Participants",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PersonalIdCode",
                table: "Participants",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RegistrationCode",
                table: "Participants",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "EventParticipants",
                columns: table => new
                {
                    EventId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ParticipantId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EventParticipants", x => new { x.EventId, x.ParticipantId });
                    table.ForeignKey(
                        name: "FK_EventParticipants_Events_EventId",
                        column: x => x.EventId,
                        principalTable: "Events",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EventParticipants_Participants_ParticipantId",
                        column: x => x.ParticipantId,
                        principalTable: "Participants",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EventParticipants_ParticipantId",
                table: "EventParticipants",
                column: "ParticipantId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EventParticipants");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "Participants");

            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "Participants");

            migrationBuilder.DropColumn(
                name: "LastName",
                table: "Participants");

            migrationBuilder.DropColumn(
                name: "LegalName",
                table: "Participants");

            migrationBuilder.DropColumn(
                name: "ParticipantsCount",
                table: "Participants");

            migrationBuilder.DropColumn(
                name: "PersonalIdCode",
                table: "Participants");

            migrationBuilder.DropColumn(
                name: "RegistrationCode",
                table: "Participants");

            migrationBuilder.AlterColumn<string>(
                name: "PaymentMethod",
                table: "Participants",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "AdditionalInfo",
                table: "Participants",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "EventId",
                table: "Participants",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<string>(
                name: "Type",
                table: "Participants",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Participants_EventId",
                table: "Participants",
                column: "EventId");

            migrationBuilder.AddForeignKey(
                name: "FK_Participants_Events_EventId",
                table: "Participants",
                column: "EventId",
                principalTable: "Events",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
