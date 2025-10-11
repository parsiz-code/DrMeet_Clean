using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DrMeet.Api.Migrations
{
    /// <inheritdoc />
    public partial class init1111 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CenterDoctorsDepartmantSelected_Doctors_DoctorId",
                table: "CenterDoctorsDepartmantSelected");

            migrationBuilder.DropIndex(
                name: "IX_CenterDoctorsDepartmantSelected_DoctorId",
                table: "CenterDoctorsDepartmantSelected");

            migrationBuilder.DropColumn(
                name: "DoctorId",
                table: "CenterDoctorsDepartmantSelected");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DoctorId",
                table: "CenterDoctorsDepartmantSelected",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_CenterDoctorsDepartmantSelected_DoctorId",
                table: "CenterDoctorsDepartmantSelected",
                column: "DoctorId");

            migrationBuilder.AddForeignKey(
                name: "FK_CenterDoctorsDepartmantSelected_Doctors_DoctorId",
                table: "CenterDoctorsDepartmantSelected",
                column: "DoctorId",
                principalTable: "Doctors",
                principalColumn: "Id");
        }
    }
}
