using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DrMeet.Api.Migrations
{
    /// <inheritdoc />
    public partial class init11 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CenterDoctorsDepartmantSelected_Centers_CenterId",
                table: "CenterDoctorsDepartmantSelected");

            migrationBuilder.DropIndex(
                name: "IX_CenterDoctorsDepartmantSelected_CenterId_DoctorId_CenterDepartmentId",
                table: "CenterDoctorsDepartmantSelected");

            migrationBuilder.CreateIndex(
                name: "IX_CenterDoctorsDepartmantSelected_CenterId",
                table: "CenterDoctorsDepartmantSelected",
                column: "CenterId");

            migrationBuilder.AddForeignKey(
                name: "FK_CenterDoctorsDepartmantSelected_Centers_CenterId",
                table: "CenterDoctorsDepartmantSelected",
                column: "CenterId",
                principalTable: "Centers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CenterDoctorsDepartmantSelected_Centers_CenterId",
                table: "CenterDoctorsDepartmantSelected");

            migrationBuilder.DropIndex(
                name: "IX_CenterDoctorsDepartmantSelected_CenterId",
                table: "CenterDoctorsDepartmantSelected");

            migrationBuilder.CreateIndex(
                name: "IX_CenterDoctorsDepartmantSelected_CenterId_DoctorId_CenterDepartmentId",
                table: "CenterDoctorsDepartmantSelected",
                columns: new[] { "CenterId", "DoctorId", "CenterDepartmentId" },
                unique: true,
                filter: "[CenterId] IS NOT NULL AND [DoctorId] IS NOT NULL AND [CenterDepartmentId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_CenterDoctorsDepartmantSelected_Centers_CenterId",
                table: "CenterDoctorsDepartmantSelected",
                column: "CenterId",
                principalTable: "Centers",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }
    }
}
