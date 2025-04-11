using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EventRegistration.Infra.Migrations
{
    /// <inheritdoc />
    public partial class domainFixMigra : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Participants_PaymentMethod_PaymentMethodId",
                table: "Participants");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PaymentMethod",
                table: "PaymentMethod");

            migrationBuilder.RenameTable(
                name: "PaymentMethod",
                newName: "PaymentMethods");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PaymentMethods",
                table: "PaymentMethods",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Participants_PaymentMethods_PaymentMethodId",
                table: "Participants",
                column: "PaymentMethodId",
                principalTable: "PaymentMethods",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Participants_PaymentMethods_PaymentMethodId",
                table: "Participants");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PaymentMethods",
                table: "PaymentMethods");

            migrationBuilder.RenameTable(
                name: "PaymentMethods",
                newName: "PaymentMethod");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PaymentMethod",
                table: "PaymentMethod",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Participants_PaymentMethod_PaymentMethodId",
                table: "Participants",
                column: "PaymentMethodId",
                principalTable: "PaymentMethod",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
