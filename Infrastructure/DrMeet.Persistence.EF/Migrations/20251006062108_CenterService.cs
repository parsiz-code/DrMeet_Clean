using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DrMeet.Persistence.EF.Migrations
{
    /// <inheritdoc />
    public partial class CenterService : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CenterDoctorsSelected",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CenterId = table.Column<int>(type: "int", nullable: true),
                    DoctorId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CenterDoctorsSelected", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CenterDoctorsSelected_Centers_CenterId",
                        column: x => x.CenterId,
                        principalTable: "Centers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_CenterDoctorsSelected_Doctors_DoctorId",
                        column: x => x.DoctorId,
                        principalTable: "Doctors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CenterDoctorsSelected_CenterId_DoctorId",
                table: "CenterDoctorsSelected",
                columns: new[] { "CenterId", "DoctorId" },
                unique: true,
                filter: "[CenterId] IS NOT NULL AND [DoctorId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_CenterDoctorsSelected_DoctorId",
                table: "CenterDoctorsSelected",
                column: "DoctorId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CenterDoctorsSelected");
        }
    }
}
