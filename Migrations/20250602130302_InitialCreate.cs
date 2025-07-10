using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DrugAbuseReportingSystem.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AdminUsers",
                columns: table => new
                {
                    AdminId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Username = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    FullName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    LastLogin = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AdminUsers", x => x.AdminId);
                });

            migrationBuilder.CreateTable(
                name: "DrugInformation",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Effects = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Risks = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ImagePath = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DrugInformation", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RehabilitationCenters",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Location = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ContactInfo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Services = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ImagePath = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RehabilitationCenters", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Reports",
                columns: table => new
                {
                    ReportId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ReportDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Location = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    DrugType = table.Column<int>(type: "int", nullable: false),
                    Severity = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    AnonymousContact = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    ImagePath = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false),
                    AdminNotes = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reports", x => x.ReportId);
                });

            migrationBuilder.InsertData(
                table: "AdminUsers",
                columns: new[] { "AdminId", "Email", "FullName", "LastLogin", "PasswordHash", "Username" },
                values: new object[] { 1, "admin@drugabusereporting.com", "System Administrator", new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "admin123", "admin" });

            migrationBuilder.InsertData(
                table: "DrugInformation",
                columns: new[] { "Id", "Description", "Effects", "ImagePath", "Name", "Risks" },
                values: new object[,]
                {
                    { 1, "Cannabis, also known as marijuana among other names, is a psychoactive drug from the Cannabis plant.", "Euphoria, relaxation, altered perception of time and space", "/images/drugs/cannabis.jpg", "Cannabis", "Impaired memory, lung damage (from smoking), potential for addiction" },
                    { 2, "Cocaine is a powerful stimulant drug made from the leaves of the coca plant native to South America.", "Increased energy, euphoria, talkativeness, mental alertness", "/images/drugs/cocaine.jpg", "Cocaine", "Heart attack, stroke, seizures, respiratory failure, addiction" }
                });

            migrationBuilder.InsertData(
                table: "RehabilitationCenters",
                columns: new[] { "Id", "ContactInfo", "Description", "ImagePath", "Location", "Name", "Services" },
                values: new object[,]
                {
                    { 1, "555-123-4567", "A comprehensive rehabilitation center offering various treatment programs.", "/images/rehab/hope-center.jpg", "123 Main St, Anytown", "Hope Recovery Center", "Inpatient, Outpatient, Counseling" },
                    { 2, "555-987-6543", "Specialized in helping individuals overcome substance abuse.", "/images/rehab/new-beginnings.jpg", "456 Oak Ave, Somewhere", "New Beginnings Clinic", "Detox, Therapy, Aftercare" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AdminUsers");

            migrationBuilder.DropTable(
                name: "DrugInformation");

            migrationBuilder.DropTable(
                name: "RehabilitationCenters");

            migrationBuilder.DropTable(
                name: "Reports");
        }
    }
}
