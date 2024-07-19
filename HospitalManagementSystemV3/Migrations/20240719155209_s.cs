using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace HospitalManagementSystemV3.Migrations
{
    /// <inheritdoc />
    public partial class s : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Admins",
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
                    table.PrimaryKey("PK_Admins", x => x.Id);
                });

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
                    { new Guid("25208e07-fbda-42de-ba5a-ad2259fe4125"), "456 Oak St, Anytown, USA", "jane.smith@example.com", "Dr. Jane Smith", "password123", "098-765-4321", "jane_smith" },
                    { new Guid("98b81add-f033-4c87-9681-9eda5d492fe2"), "123 Main St, Anytown, USA", "john.doe@example.com", "Dr. John Doe", "password123", "123-456-7890", "john_doe" }
                });

            migrationBuilder.InsertData(
                table: "Patients",
                columns: new[] { "Id", "Address", "DoctorId", "Email", "Name", "Password", "Phone", "Username" },
                values: new object[,]
                {
                    { new Guid("2240ad66-74a9-4bff-a81d-ee455f9831bf"), "321 Elm St, Anytown, USA", new Guid("98b81add-f033-4c87-9681-9eda5d492fe2"), "patient.two@example.com", "Patient Two", "password123", "444-555-6666", "patient_two" },
                    { new Guid("5f283af1-c2b1-4d7f-bd05-3fa37ef1c89f"), "789 Pine St, Anytown, USA", new Guid("98b81add-f033-4c87-9681-9eda5d492fe2"), "patient.one@example.com", "Patient One", "password123", "111-222-3333", "patient_one" },
                    { new Guid("b3798083-0b1c-42b9-90c3-c1265072602f"), "654 Birch St, Anytown, USA", new Guid("25208e07-fbda-42de-ba5a-ad2259fe4125"), "patient.three@example.com", "Patient Three", "password123", "777-888-9999", "patient_three" }
                });

            migrationBuilder.InsertData(
                table: "Appointments",
                columns: new[] { "Id", "Description", "DoctorId", "PatientId" },
                values: new object[,]
                {
                    { 1, "General Checkup", new Guid("98b81add-f033-4c87-9681-9eda5d492fe2"), new Guid("5f283af1-c2b1-4d7f-bd05-3fa37ef1c89f") },
                    { 2, "Follow-up Visit", new Guid("98b81add-f033-4c87-9681-9eda5d492fe2"), new Guid("2240ad66-74a9-4bff-a81d-ee455f9831bf") },
                    { 3, "Consultation", new Guid("25208e07-fbda-42de-ba5a-ad2259fe4125"), new Guid("b3798083-0b1c-42b9-90c3-c1265072602f") }
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
                name: "Admins");

            migrationBuilder.DropTable(
                name: "Appointments");

            migrationBuilder.DropTable(
                name: "Patients");

            migrationBuilder.DropTable(
                name: "Doctors");
        }
    }
}
