using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EventRegistration.Infra.Migrations
{
    /// <inheritdoc />
    public partial class updateParticipant : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "Participants");

            migrationBuilder.DropColumn(
                name: "IsPerson",
                table: "Participants");

            migrationBuilder.DropColumn(
                name: "LastName",
                table: "Participants");

            migrationBuilder.DropColumn(
                name: "LegalName",
                table: "Participants");

            migrationBuilder.DropColumn(
                name: "ParticipantType",
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

            migrationBuilder.AddColumn<string>(
                name: "Type",
                table: "Participants",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Type",
                table: "Participants");

            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "Participants",
                type: "nvarchar(max)",
                nullable: true);

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
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LegalName",
                table: "Participants",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ParticipantType",
                table: "Participants",
                type: "nvarchar(13)",
                maxLength: 13,
                nullable: false,
                defaultValue: "");

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
        }
    }
}
