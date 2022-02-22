using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Mailer.Infrastructure.Migrations
{
    public partial class QueuedEmailMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "QueuedEmails",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    From = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    FromName = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    To = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    ToName = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    ReplyTo = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    ReplyToName = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    Cc = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Bcc = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Subject = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    PriorityId = table.Column<int>(type: "int", nullable: false),
                    Body = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EmailPriorityId = table.Column<int>(type: "int", nullable: false),
                    CreatedOnUtc = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SentOnUtc = table.Column<DateTime>(type: "datetime2", nullable: true),
                    FolderId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QueuedEmails", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "QueuedEmails");
        }
    }
}
