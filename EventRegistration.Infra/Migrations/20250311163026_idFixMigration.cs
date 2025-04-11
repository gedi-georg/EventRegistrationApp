using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EventRegistration.Infra.Migrations
{
    /// <inheritdoc />
    public partial class idFixMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Drop foreign keys and indexes that depend on the Id column
            migrationBuilder.DropForeignKey(name: "FK_Participants_Events_EventId", table: "Participants");
            migrationBuilder.DropIndex(name: "IX_Participants_EventId", table: "Participants");

            // Drop the primary key constraint on the Events table
            migrationBuilder.DropPrimaryKey(name: "PK_Events", table: "Events");

            // Rename the existing Id columns
            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Events",
                newName: "OldId");

            migrationBuilder.RenameColumn(
                name: "EventId",
                table: "Participants",
                newName: "OldEventId");

            // Add the new Id columns with type Guid
            migrationBuilder.AddColumn<Guid>(
                name: "Id",
                table: "Events",
                nullable: false,
                defaultValueSql: "NEWID()");

            migrationBuilder.AddColumn<Guid>(
                name: "EventId",
                table: "Participants",
                nullable: false,
                defaultValueSql: "NEWID()");

            // Copy data from OldId to Id in Events
            migrationBuilder.Sql("UPDATE Events SET Id = NEWID()");

            // Copy data from OldEventId to EventId in Participants
            migrationBuilder.Sql("UPDATE Participants SET EventId = (SELECT Id FROM Events WHERE OldId = Participants.OldEventId)");

            // Drop the old Id columns
            migrationBuilder.DropColumn(name: "OldId", table: "Events");
            migrationBuilder.DropColumn(name: "OldEventId", table: "Participants");

            // Add the new primary key constraint on the new Id column
            migrationBuilder.AddPrimaryKey(name: "PK_Events", table: "Events", column: "Id");

            // Recreate foreign keys and indexes with the new Id columns
            migrationBuilder.AddForeignKey(
                name: "FK_Participants_Events_EventId",
                table: "Participants",
                column: "EventId",
                principalTable: "Events",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.CreateIndex(
                name: "IX_Participants_EventId",
                table: "Participants",
                column: "EventId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Drop foreign keys and indexes that depend on the Id column
            migrationBuilder.DropForeignKey(name: "FK_Participants_Events_EventId", table: "Participants");
            migrationBuilder.DropIndex(name: "IX_Participants_EventId", table: "Participants");

            // Drop the primary key constraint on the Events table
            migrationBuilder.DropPrimaryKey(name: "PK_Events", table: "Events");

            // Add the old Id columns with type int
            migrationBuilder.AddColumn<int>(
                name: "OldId",
                table: "Events",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "OldEventId",
                table: "Participants",
                nullable: false,
                defaultValue: 0);

            // Copy data from Id to OldId in Events
            migrationBuilder.Sql("UPDATE Events SET OldId = CAST(Id AS int)");

            // Copy data from EventId to OldEventId in Participants
            migrationBuilder.Sql("UPDATE Participants SET OldEventId = (SELECT OldId FROM Events WHERE Id = Participants.EventId)");

            // Drop the new Id columns
            migrationBuilder.DropColumn(name: "Id", table: "Events");
            migrationBuilder.DropColumn(name: "EventId", table: "Participants");

            // Rename the old Id columns back to Id
            migrationBuilder.RenameColumn(
                name: "OldId",
                table: "Events",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "OldEventId",
                table: "Participants",
                newName: "EventId");

            // Add the old primary key constraint on the Id column
            migrationBuilder.AddPrimaryKey(name: "PK_Events", table: "Events", column: "Id");

            // Recreate foreign keys and indexes with the old Id columns
            migrationBuilder.AddForeignKey(
                name: "FK_Participants_Events_EventId",
                table: "Participants",
                column: "EventId",
                principalTable: "Events",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.CreateIndex(
                name: "IX_Participants_EventId",
                table: "Participants",
                column: "EventId");
        }
    }
}
