using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DrMeet.Persistence.EF.Migrations
{
    /// <inheritdoc />
    public partial class removecenterIdDoctorIdAddCenterDoctorIdCenterDoctorServiceOnlineConsultation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CenterDoctorServiceOnlineConsultations_Centers_CenterId",
                table: "CenterDoctorServiceOnlineConsultations");

            migrationBuilder.DropForeignKey(
                name: "FK_CenterDoctorServiceOnlineConsultations_Doctors_DoctorId",
                table: "CenterDoctorServiceOnlineConsultations");

            migrationBuilder.DropIndex(
                name: "IX_CenterDoctorServiceOnlineConsultations_CenterId_DoctorId_ServicesAvailableId",
                table: "CenterDoctorServiceOnlineConsultations");

            migrationBuilder.DropIndex(
                name: "IX_CenterDoctorServiceOnlineConsultations_DoctorId",
                table: "CenterDoctorServiceOnlineConsultations");

            migrationBuilder.DropColumn(
                name: "CenterId",
                table: "CenterDoctorServiceOnlineConsultations");

            migrationBuilder.DropColumn(
                name: "DoctorId",
                table: "CenterDoctorServiceOnlineConsultations");

            migrationBuilder.AddColumn<int>(
                name: "CenterDoctorId",
                table: "CenterDoctorServiceOnlineConsultations",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_CenterDoctorServiceOnlineConsultations_CenterDoctorId_ServicesAvailableId",
                table: "CenterDoctorServiceOnlineConsultations",
                columns: new[] { "CenterDoctorId", "ServicesAvailableId" },
                unique: true,
                filter: "[CenterDoctorId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_CenterDoctorServiceOnlineConsultations_CenterDoctorsSelected_CenterDoctorId",
                table: "CenterDoctorServiceOnlineConsultations",
                column: "CenterDoctorId",
                principalTable: "CenterDoctorsSelected",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CenterDoctorServiceOnlineConsultations_CenterDoctorsSelected_CenterDoctorId",
                table: "CenterDoctorServiceOnlineConsultations");

            migrationBuilder.DropIndex(
                name: "IX_CenterDoctorServiceOnlineConsultations_CenterDoctorId_ServicesAvailableId",
                table: "CenterDoctorServiceOnlineConsultations");

            migrationBuilder.DropColumn(
                name: "CenterDoctorId",
                table: "CenterDoctorServiceOnlineConsultations");

            migrationBuilder.AddColumn<int>(
                name: "CenterId",
                table: "CenterDoctorServiceOnlineConsultations",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "DoctorId",
                table: "CenterDoctorServiceOnlineConsultations",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_CenterDoctorServiceOnlineConsultations_CenterId_DoctorId_ServicesAvailableId",
                table: "CenterDoctorServiceOnlineConsultations",
                columns: new[] { "CenterId", "DoctorId", "ServicesAvailableId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CenterDoctorServiceOnlineConsultations_DoctorId",
                table: "CenterDoctorServiceOnlineConsultations",
                column: "DoctorId");

            migrationBuilder.AddForeignKey(
                name: "FK_CenterDoctorServiceOnlineConsultations_Centers_CenterId",
                table: "CenterDoctorServiceOnlineConsultations",
                column: "CenterId",
                principalTable: "Centers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CenterDoctorServiceOnlineConsultations_Doctors_DoctorId",
                table: "CenterDoctorServiceOnlineConsultations",
                column: "DoctorId",
                principalTable: "Doctors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
