using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EventRegistration.Infra.Migrations
{
    /// <inheritdoc />
    public partial class domainFixMigra2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Participants_PaymentMethods_PaymentMethodId",
                table: "Participants");

            migrationBuilder.DropIndex(
                name: "IX_Participants_PaymentMethodId",
                table: "Participants");

            migrationBuilder.DropColumn(
                name: "PaymentMethodId",
                table: "Participants");

            migrationBuilder.AddColumn<Guid>(
                name: "PaymentMethodId",
                table: "EventParticipants",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_EventParticipants_PaymentMethodId",
                table: "EventParticipants",
                column: "PaymentMethodId");

            migrationBuilder.AddForeignKey(
                name: "FK_EventParticipants_PaymentMethods_PaymentMethodId",
                table: "EventParticipants",
                column: "PaymentMethodId",
                principalTable: "PaymentMethods",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EventParticipants_PaymentMethods_PaymentMethodId",
                table: "EventParticipants");

            migrationBuilder.DropIndex(
                name: "IX_EventParticipants_PaymentMethodId",
                table: "EventParticipants");

            migrationBuilder.DropColumn(
                name: "PaymentMethodId",
                table: "EventParticipants");

            migrationBuilder.AddColumn<Guid>(
                name: "PaymentMethodId",
                table: "Participants",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Participants_PaymentMethodId",
                table: "Participants",
                column: "PaymentMethodId");

            migrationBuilder.AddForeignKey(
                name: "FK_Participants_PaymentMethods_PaymentMethodId",
                table: "Participants",
                column: "PaymentMethodId",
                principalTable: "PaymentMethods",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
