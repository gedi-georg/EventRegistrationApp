using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace EventRegistration.Infra.Migrations
{
    /// <inheritdoc />
    public partial class PaymentMethodMigra : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PaymentMethod",
                table: "Participants");

            migrationBuilder.AddColumn<Guid>(
                name: "PaymentMethodId",
                table: "Participants",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "PaymentMethod",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaymentMethod", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "PaymentMethod",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("11111111-1111-1111-1111-111111111111"), "Pangaülekanne" },
                    { new Guid("22222222-2222-2222-2222-222222222222"), "Sularaha" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Participants_PaymentMethodId",
                table: "Participants",
                column: "PaymentMethodId");

            migrationBuilder.AddForeignKey(
                name: "FK_Participants_PaymentMethod_PaymentMethodId",
                table: "Participants",
                column: "PaymentMethodId",
                principalTable: "PaymentMethod",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Participants_PaymentMethod_PaymentMethodId",
                table: "Participants");

            migrationBuilder.DropTable(
                name: "PaymentMethod");

            migrationBuilder.DropIndex(
                name: "IX_Participants_PaymentMethodId",
                table: "Participants");

            migrationBuilder.DropColumn(
                name: "PaymentMethodId",
                table: "Participants");

            migrationBuilder.AddColumn<int>(
                name: "PaymentMethod",
                table: "Participants",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
