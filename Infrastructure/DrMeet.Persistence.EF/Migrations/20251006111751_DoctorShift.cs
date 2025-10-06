using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DrMeet.Persistence.EF.Migrations
{
    /// <inheritdoc />
    public partial class DoctorShift : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DoctorShifts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CenterDoctorsDepartmantId = table.Column<int>(type: "int", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    StartTime = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    EndTime = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    MeetTime = table.Column<int>(type: "int", nullable: false),
                    ActivityStatus = table.Column<int>(type: "int", nullable: false),
                    ShiftType = table.Column<int>(type: "int", nullable: false),
                    DayOfWeek = table.Column<int>(type: "int", nullable: false),
                    Deleted = table.Column<bool>(type: "bit", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()"),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()"),
                    Status = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DoctorShifts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DoctorShifts_CenterDoctorsDepartmantSelected_CenterDoctorsDepartmantId",
                        column: x => x.CenterDoctorsDepartmantId,
                        principalTable: "CenterDoctorsDepartmantSelected",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "DoctorShiftTimeItems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StartTime = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    EndTime = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    IsShiftAvailable = table.Column<bool>(type: "bit", nullable: false),
                    DoctorShiftId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DoctorShiftTimeItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DoctorShiftTimeItems_DoctorShifts_DoctorShiftId",
                        column: x => x.DoctorShiftId,
                        principalTable: "DoctorShifts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DoctorShifts_CenterDoctorsDepartmantId_DayOfWeek_StartTime_EndTime",
                table: "DoctorShifts",
                columns: new[] { "CenterDoctorsDepartmantId", "DayOfWeek", "StartTime", "EndTime" },
                unique: true,
                filter: "[CenterDoctorsDepartmantId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_DoctorShiftTimeItems_DoctorShiftId_StartTime_EndTime",
                table: "DoctorShiftTimeItems",
                columns: new[] { "DoctorShiftId", "StartTime", "EndTime" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DoctorShiftTimeItems");

            migrationBuilder.DropTable(
                name: "DoctorShifts");
        }
    }
}
