using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EventRegistration.Infra.Migrations
{
    /// <inheritdoc />
    public partial class fixParticipantIdTypeMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Drop the primary key constraint on the Participants table
            migrationBuilder.DropPrimaryKey(name: "PK_Participants", table: "Participants");

            // Rename the existing Id column
            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Participants",
                newName: "OldId");

            // Add the new Id column with type Guid
            migrationBuilder.AddColumn<Guid>(
                name: "Id",
                table: "Participants",
                nullable: false,
                defaultValueSql: "NEWID()");

            // Copy data from OldId to Id in Participants
            migrationBuilder.Sql("UPDATE Participants SET Id = NEWID()");

            // Drop the old Id column
            migrationBuilder.DropColumn(name: "OldId", table: "Participants");

            // Add the new primary key constraint on the new Id column
            migrationBuilder.AddPrimaryKey(name: "PK_Participants", table: "Participants", column: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Drop the primary key constraint on the Participants table
            migrationBuilder.DropPrimaryKey(name: "PK_Participants", table: "Participants");

            // Add the old Id column with type int
            migrationBuilder.AddColumn<int>(
                name: "OldId",
                table: "Participants",
                nullable: false,
                defaultValue: 0);

            // Copy data from Id to OldId in Participants
            migrationBuilder.Sql("UPDATE Participants SET OldId = CAST(Id AS int)");

            // Drop the new Id column
            migrationBuilder.DropColumn(name: "Id", table: "Participants");

            // Rename the old Id column back to Id
            migrationBuilder.RenameColumn(
                name: "OldId",
                table: "Participants",
                newName: "Id");

            // Add the old primary key constraint on the Id column
            migrationBuilder.AddPrimaryKey(name: "PK_Participants", table: "Participants", column: "Id");
        }
    }
}
