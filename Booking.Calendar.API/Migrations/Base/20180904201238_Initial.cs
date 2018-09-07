using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Booking.Calendar.API.Migrations.Base
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

            migrationBuilder.CreateTable(
                name: "IDSpecifications",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    GoogleId = table.Column<string>(nullable: true),
                    AppointmentId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IDSpecifications", x => x.Id);
                    table.ForeignKey(
                        name: "FK_IDSpecifications_apponintment_AppointmentId",
                        column: x => x.AppointmentId,
                        principalTable: "apponintment",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_IDSpecifications_AppointmentId",
                table: "IDSpecifications",
                column: "AppointmentId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "IDSpecifications");

            migrationBuilder.DropTable(
                name: "requests");

            migrationBuilder.DropTable(
                name: "apponintment");

            migrationBuilder.DropSequence(
                name: "appointmentseq");
        }
    }
}
