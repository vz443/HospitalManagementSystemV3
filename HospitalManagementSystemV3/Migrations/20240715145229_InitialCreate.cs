using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace HospitalManagementSystemV3.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Doctors",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Email = table.Column<string>(type: "TEXT", nullable: false),
                    Phone = table.Column<string>(type: "TEXT", nullable: false),
                    Address = table.Column<string>(type: "TEXT", nullable: false),
                    Username = table.Column<string>(type: "TEXT", nullable: false),
                    Password = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Doctors", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Patients",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Email = table.Column<string>(type: "TEXT", nullable: false),
                    Phone = table.Column<string>(type: "TEXT", nullable: false),
                    Address = table.Column<string>(type: "TEXT", nullable: false),
                    Username = table.Column<string>(type: "TEXT", nullable: false),
                    Password = table.Column<string>(type: "TEXT", nullable: false),
                    DoctorId = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Patients", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Patients_Doctors_DoctorId",
                        column: x => x.DoctorId,
                        principalTable: "Doctors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Appointments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    DoctorId = table.Column<Guid>(type: "TEXT", nullable: false),
                    PatientId = table.Column<Guid>(type: "TEXT", nullable: false),
                    Description = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Appointments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Appointments_Doctors_DoctorId",
                        column: x => x.DoctorId,
                        principalTable: "Doctors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Appointments_Patients_PatientId",
                        column: x => x.PatientId,
                        principalTable: "Patients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Doctors",
                columns: new[] { "Id", "Address", "Email", "Name", "Password", "Phone", "Username" },
                values: new object[,]
                {
                    { new Guid("383bf44b-f74e-4b21-be15-339866d29628"), "123 Main St, Anytown, USA", "john.doe@example.com", "Dr. John Doe", "password123", "123-456-7890", "john_doe" },
                    { new Guid("946b8c60-77fa-49d9-8aed-b42817ca2113"), "456 Oak St, Anytown, USA", "jane.smith@example.com", "Dr. Jane Smith", "password123", "098-765-4321", "jane_smith" }
                });

            migrationBuilder.InsertData(
                table: "Patients",
                columns: new[] { "Id", "Address", "DoctorId", "Email", "Name", "Password", "Phone", "Username" },
                values: new object[,]
                {
                    { new Guid("08742018-7f29-403d-bbc7-8fc5c095df25"), "789 Pine St, Anytown, USA", new Guid("383bf44b-f74e-4b21-be15-339866d29628"), "patient.one@example.com", "Patient One", "password123", "111-222-3333", "patient_one" },
                    { new Guid("a7fcb488-3a02-4b3e-a9c2-dc4793ce3bda"), "321 Elm St, Anytown, USA", new Guid("383bf44b-f74e-4b21-be15-339866d29628"), "patient.two@example.com", "Patient Two", "password123", "444-555-6666", "patient_two" },
                    { new Guid("ab17abe4-be1d-4d9c-9050-b01b29c303d8"), "654 Birch St, Anytown, USA", new Guid("946b8c60-77fa-49d9-8aed-b42817ca2113"), "patient.three@example.com", "Patient Three", "password123", "777-888-9999", "patient_three" }
                });

            migrationBuilder.InsertData(
                table: "Appointments",
                columns: new[] { "Id", "Description", "DoctorId", "PatientId" },
                values: new object[,]
                {
                    { 1, "General Checkup", new Guid("383bf44b-f74e-4b21-be15-339866d29628"), new Guid("08742018-7f29-403d-bbc7-8fc5c095df25") },
                    { 2, "Follow-up Visit", new Guid("383bf44b-f74e-4b21-be15-339866d29628"), new Guid("a7fcb488-3a02-4b3e-a9c2-dc4793ce3bda") },
                    { 3, "Consultation", new Guid("946b8c60-77fa-49d9-8aed-b42817ca2113"), new Guid("ab17abe4-be1d-4d9c-9050-b01b29c303d8") }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Appointments_DoctorId",
                table: "Appointments",
                column: "DoctorId");

            migrationBuilder.CreateIndex(
                name: "IX_Appointments_PatientId",
                table: "Appointments",
                column: "PatientId");

            migrationBuilder.CreateIndex(
                name: "IX_Patients_DoctorId",
                table: "Patients",
                column: "DoctorId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Appointments");

            migrationBuilder.DropTable(
                name: "Patients");

            migrationBuilder.DropTable(
                name: "Doctors");
        }
    }
}
