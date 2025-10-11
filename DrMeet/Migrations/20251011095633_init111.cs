using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DrMeet.Api.Migrations
{
    /// <inheritdoc />
    public partial class init111 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CenterDoctorsDepartmantSelected_CenterDoctorsSelected_CenterDoctorsSelectedId",
                table: "CenterDoctorsDepartmantSelected");

            migrationBuilder.DropForeignKey(
                name: "FK_CenterDoctorsDepartmantSelected_Centers_CenterId",
                table: "CenterDoctorsDepartmantSelected");

            migrationBuilder.DropForeignKey(
                name: "FK_CenterDoctorsDepartmantSelected_Doctors_DoctorId",
                table: "CenterDoctorsDepartmantSelected");

            migrationBuilder.DropIndex(
                name: "IX_CenterDoctorsDepartmantSelected_CenterId",
                table: "CenterDoctorsDepartmantSelected");

            migrationBuilder.DropColumn(
                name: "CenterId",
                table: "CenterDoctorsDepartmantSelected");

            migrationBuilder.AddForeignKey(
                name: "FK_CenterDoctorsDepartmantSelected_CenterDoctorsSelected_CenterDoctorsSelectedId",
                table: "CenterDoctorsDepartmantSelected",
                column: "CenterDoctorsSelectedId",
                principalTable: "CenterDoctorsSelected",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_CenterDoctorsDepartmantSelected_Doctors_DoctorId",
                table: "CenterDoctorsDepartmantSelected",
                column: "DoctorId",
                principalTable: "Doctors",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CenterDoctorsDepartmantSelected_CenterDoctorsSelected_CenterDoctorsSelectedId",
                table: "CenterDoctorsDepartmantSelected");

            migrationBuilder.DropForeignKey(
                name: "FK_CenterDoctorsDepartmantSelected_Doctors_DoctorId",
                table: "CenterDoctorsDepartmantSelected");

            migrationBuilder.AddColumn<int>(
                name: "CenterId",
                table: "CenterDoctorsDepartmantSelected",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_CenterDoctorsDepartmantSelected_CenterId",
                table: "CenterDoctorsDepartmantSelected",
                column: "CenterId");

            migrationBuilder.AddForeignKey(
                name: "FK_CenterDoctorsDepartmantSelected_CenterDoctorsSelected_CenterDoctorsSelectedId",
                table: "CenterDoctorsDepartmantSelected",
                column: "CenterDoctorsSelectedId",
                principalTable: "CenterDoctorsSelected",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CenterDoctorsDepartmantSelected_Centers_CenterId",
                table: "CenterDoctorsDepartmantSelected",
                column: "CenterId",
                principalTable: "Centers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CenterDoctorsDepartmantSelected_Doctors_DoctorId",
                table: "CenterDoctorsDepartmantSelected",
                column: "DoctorId",
                principalTable: "Doctors",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }
    }
}
