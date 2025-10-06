using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DrMeet.Persistence.EF.Migrations
{
    /// <inheritdoc />
    public partial class CenterDoctorsServiceSelected : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CenterDoctorsServiceSelected",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CenterDoctorId = table.Column<int>(type: "int", nullable: true),
                    ProviderServiceId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CenterDoctorsServiceSelected", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CenterDoctorsServiceSelected_CenterDoctorsSelected_CenterDoctorId",
                        column: x => x.CenterDoctorId,
                        principalTable: "CenterDoctorsSelected",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_CenterDoctorsServiceSelected_ProviderServices_ProviderServiceId",
                        column: x => x.ProviderServiceId,
                        principalTable: "ProviderServices",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CenterDoctorsServiceSelected_CenterDoctorId_ProviderServiceId",
                table: "CenterDoctorsServiceSelected",
                columns: new[] { "CenterDoctorId", "ProviderServiceId" },
                unique: true,
                filter: "[CenterDoctorId] IS NOT NULL AND [ProviderServiceId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_CenterDoctorsServiceSelected_ProviderServiceId",
                table: "CenterDoctorsServiceSelected",
                column: "ProviderServiceId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CenterDoctorsServiceSelected");
        }
    }
}
