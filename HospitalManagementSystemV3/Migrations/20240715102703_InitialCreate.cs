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
                    DoctorId = table.Column<Guid>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Patients", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Patients_Doctors_DoctorId",
                        column: x => x.DoctorId,
                        principalTable: "Doctors",
                        principalColumn: "Id");
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
                    { new Guid("9f57f93c-22fb-413a-8206-e814efc81c8d"), "456 Oak St, Anytown, USA", "jane.smith@example.com", "Dr. Jane Smith", "password123", "098-765-4321", "jane_smith" },
                    { new Guid("ba1d5a8b-063f-473e-b9c0-524b89b3f825"), "123 Main St, Anytown, USA", "john.doe@example.com", "Dr. John Doe", "password123", "123-456-7890", "john_doe" }
                });

            migrationBuilder.InsertData(
                table: "Patients",
                columns: new[] { "Id", "Address", "DoctorId", "Email", "Name", "Password", "Phone", "Username" },
                values: new object[,]
                {
                    { new Guid("224678c3-2865-4dc0-876c-670df6a74d22"), "321 Elm St, Anytown, USA", null, "patient.two@example.com", "Patient Two", "password123", "444-555-6666", "patient_two" },
                    { new Guid("caefec1f-667b-458c-87b5-adeae3ee760c"), "789 Pine St, Anytown, USA", null, "patient.one@example.com", "Patient One", "password123", "111-222-3333", "patient_one" },
                    { new Guid("fa700bb3-ff4a-407f-be1f-dbe45c6ae509"), "654 Birch St, Anytown, USA", null, "patient.three@example.com", "Patient Three", "password123", "777-888-9999", "patient_three" }
                });

            migrationBuilder.InsertData(
                table: "Appointments",
                columns: new[] { "Id", "Description", "DoctorId", "PatientId" },
                values: new object[,]
                {
                    { 1, "General Checkup", new Guid("ba1d5a8b-063f-473e-b9c0-524b89b3f825"), new Guid("caefec1f-667b-458c-87b5-adeae3ee760c") },
                    { 2, "Follow-up Visit", new Guid("ba1d5a8b-063f-473e-b9c0-524b89b3f825"), new Guid("224678c3-2865-4dc0-876c-670df6a74d22") },
                    { 3, "Consultation", new Guid("9f57f93c-22fb-413a-8206-e814efc81c8d"), new Guid("fa700bb3-ff4a-407f-be1f-dbe45c6ae509") }
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
