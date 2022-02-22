using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Mailer.Infrastructure.Migrations
{
    public partial class StripedBody : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EmailPriorityId",
                table: "QueuedEmails");

            migrationBuilder.RenameColumn(
                name: "PriorityId",
                table: "QueuedEmails",
                newName: "EmailPriority");

            migrationBuilder.AlterColumn<string>(
                name: "ReplyToName",
                table: "QueuedEmails",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(500)",
                oldMaxLength: 500);

            migrationBuilder.AddColumn<string>(
                name: "BodyStriped",
                table: "QueuedEmails",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BodyStriped",
                table: "QueuedEmails");

            migrationBuilder.RenameColumn(
                name: "EmailPriority",
                table: "QueuedEmails",
                newName: "PriorityId");

            migrationBuilder.AlterColumn<string>(
                name: "ReplyToName",
                table: "QueuedEmails",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(500)",
                oldMaxLength: 500,
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "EmailPriorityId",
                table: "QueuedEmails",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
