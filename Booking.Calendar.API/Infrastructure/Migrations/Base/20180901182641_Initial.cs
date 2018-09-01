using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Booking.Calendar.API.Infrastructure.Migrations.Base
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateSequence(
                name: "appointmentseq",
                incrementBy: 10);

            migrationBuilder.CreateTable(
                name: "apponintment",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false),
                    StartDate = table.Column<DateTime>(nullable: false),
                    EndDate = table.Column<DateTime>(nullable: false),
                    Categoria = table.Column<string>(nullable: true),
                    ClassEvent = table.Column<string>(nullable: false),
                    Title = table.Column<string>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    From = table.Column<string>(nullable: false),
                    To = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_apponintment", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "requests",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Time = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_requests", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "apponintment");

            migrationBuilder.DropTable(
                name: "requests");

            migrationBuilder.DropSequence(
                name: "appointmentseq");
        }
    }
}
