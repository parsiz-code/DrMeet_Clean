using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DrMeet.Persistence.EF.Migrations
{
    /// <inheritdoc />
    public partial class DoctorReserveTimes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DoctorReserveTimes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    DoctorTimeId = table.Column<int>(type: "int", maxLength: 50, nullable: true),
                    CenterDoctorsServiceId = table.Column<int>(type: "int", maxLength: 50, nullable: false),
                    PatientId = table.Column<int>(type: "int", nullable: false),
                    ShiftStatus = table.Column<int>(type: "int", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Deleted = table.Column<bool>(type: "bit", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()"),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()"),
                    Status = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DoctorReserveTimes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DoctorReserveTimes_CenterDoctorsServiceSelected_CenterDoctorsServiceId",
                        column: x => x.CenterDoctorsServiceId,
                        principalTable: "CenterDoctorsServiceSelected",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DoctorReserveTimes_DoctorShiftTimeItems_DoctorTimeId",
                        column: x => x.DoctorTimeId,
                        principalTable: "DoctorShiftTimeItems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DoctorReserveTimes_Patients_PatientId",
                        column: x => x.PatientId,
                        principalTable: "Patients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DoctorReserveTimes_CenterDoctorsServiceId",
                table: "DoctorReserveTimes",
                column: "CenterDoctorsServiceId");

            migrationBuilder.CreateIndex(
                name: "IX_DoctorReserveTimes_DoctorTimeId",
                table: "DoctorReserveTimes",
                column: "DoctorTimeId");

            migrationBuilder.CreateIndex(
                name: "IX_DoctorReserveTimes_PatientId_Date_CenterDoctorsServiceId",
                table: "DoctorReserveTimes",
                columns: new[] { "PatientId", "Date", "CenterDoctorsServiceId" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DoctorReserveTimes");
        }
    }
}
