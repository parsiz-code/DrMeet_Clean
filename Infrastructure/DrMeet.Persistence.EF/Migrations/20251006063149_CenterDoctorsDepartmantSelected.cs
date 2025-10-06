using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DrMeet.Persistence.EF.Migrations
{
    /// <inheritdoc />
    public partial class CenterDoctorsDepartmantSelected : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CenterDoctorsDepartmantSelected",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CenterDoctorId = table.Column<int>(type: "int", nullable: true),
                    CenterDepartmentId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CenterDoctorsDepartmantSelected", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CenterDoctorsDepartmantSelected_CenterDepartments_CenterDepartmentId",
                        column: x => x.CenterDepartmentId,
                        principalTable: "CenterDepartments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_CenterDoctorsDepartmantSelected_CenterDoctorsSelected_CenterDoctorId",
                        column: x => x.CenterDoctorId,
                        principalTable: "CenterDoctorsSelected",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CenterDoctorsDepartmantSelected_CenterDepartmentId",
                table: "CenterDoctorsDepartmantSelected",
                column: "CenterDepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_CenterDoctorsDepartmantSelected_CenterDoctorId_CenterDepartmentId",
                table: "CenterDoctorsDepartmantSelected",
                columns: new[] { "CenterDoctorId", "CenterDepartmentId" },
                unique: true,
                filter: "[CenterDoctorId] IS NOT NULL AND [CenterDepartmentId] IS NOT NULL");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CenterDoctorsDepartmantSelected");
        }
    }
}
