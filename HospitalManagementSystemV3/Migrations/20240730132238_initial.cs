using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace HospitalManagementSystemV3.Migrations
{
    /// <inheritdoc />
    public partial class initial : Migration
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
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
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
                    { new Guid("7a4ad876-faa5-4f06-904b-434ac27e8c77"), "123 Main St, Anytown, USA", "john.doe@example.com", "Dr. John Doe", "password123", "123-456-7890", "john_doe" },
                    { new Guid("d4eb8113-9d41-4000-aee5-2c2282fe39a8"), "456 Oak St, Anytown, USA", "jane.smith@example.com", "Dr. Jane Smith", "password123", "098-765-4321", "jane_smith" }
                });

            migrationBuilder.InsertData(
                table: "Patients",
                columns: new[] { "Id", "Address", "DoctorId", "Email", "Name", "Password", "Phone", "Username" },
                values: new object[,]
                {
                    { new Guid("381b1fbb-27fd-490d-980f-ee915646dee2"), "789 Pine St, Anytown, USA", new Guid("7a4ad876-faa5-4f06-904b-434ac27e8c77"), "patient.one@example.com", "Patient One", "password123", "111-222-3333", "patient_one" },
                    { new Guid("5b83a9fd-de61-4c10-a0d7-b79a861d23d3"), "654 Birch St, Anytown, USA", new Guid("d4eb8113-9d41-4000-aee5-2c2282fe39a8"), "patient.three@example.com", "Patient Three", "password123", "777-888-9999", "patient_three" },
                    { new Guid("fb1d0e77-3ab1-4fe3-ab19-c37be23d4816"), "321 Elm St, Anytown, USA", new Guid("7a4ad876-faa5-4f06-904b-434ac27e8c77"), "patient.two@example.com", "Patient Two", "password123", "444-555-6666", "patient_two" }
                });

            migrationBuilder.InsertData(
                table: "Appointments",
                columns: new[] { "Id", "Description", "DoctorId", "PatientId" },
                values: new object[,]
                {
                    { new Guid("2d270ba2-748b-45e6-bfe5-12c1e39b490e"), "Consultation", new Guid("d4eb8113-9d41-4000-aee5-2c2282fe39a8"), new Guid("5b83a9fd-de61-4c10-a0d7-b79a861d23d3") },
                    { new Guid("95fd3ea6-ed7c-4fbb-b3b6-7c25d7be0233"), "Follow-up Visit", new Guid("7a4ad876-faa5-4f06-904b-434ac27e8c77"), new Guid("fb1d0e77-3ab1-4fe3-ab19-c37be23d4816") },
                    { new Guid("c00e7a77-123e-4b9f-b3dc-30d72d0f016a"), "General Checkup", new Guid("7a4ad876-faa5-4f06-904b-434ac27e8c77"), new Guid("381b1fbb-27fd-490d-980f-ee915646dee2") }
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
