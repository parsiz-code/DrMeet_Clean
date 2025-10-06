using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DrMeet.Persistence.EF.Migrations
{
    /// <inheritdoc />
    public partial class DoctorShiftServices : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DoctorShiftServices",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DoctorShiftId = table.Column<int>(type: "int", nullable: false),
                    CenterDoctorsServiceId = table.Column<int>(type: "int", nullable: false),
                    Deleted = table.Column<bool>(type: "bit", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()"),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()"),
                    Status = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DoctorShiftServices", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DoctorShiftServices_CenterDoctorsServiceSelected_CenterDoctorsServiceId",
                        column: x => x.CenterDoctorsServiceId,
                        principalTable: "CenterDoctorsServiceSelected",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DoctorShiftServices_DoctorShifts_DoctorShiftId",
                        column: x => x.DoctorShiftId,
                        principalTable: "DoctorShifts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DoctorShiftServices_CenterDoctorsServiceId",
                table: "DoctorShiftServices",
                column: "CenterDoctorsServiceId");

            migrationBuilder.CreateIndex(
                name: "IX_DoctorShiftServices_DoctorShiftId_CenterDoctorsServiceId",
                table: "DoctorShiftServices",
                columns: new[] { "DoctorShiftId", "CenterDoctorsServiceId" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DoctorShiftServices");
        }
    }
}
