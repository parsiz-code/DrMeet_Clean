using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DrMeet.Persistence.EF.Migrations
{
    /// <inheritdoc />
    public partial class CenterDoctorServiceOnlineConsultations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CenterDoctorPricings");

            migrationBuilder.CreateTable(
                name: "CenterDoctorServiceOnlineConsultations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CenterId = table.Column<int>(type: "int", nullable: false),
                    DoctorId = table.Column<int>(type: "int", nullable: false),
                    ServicesAvailableId = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PercentagePayment = table.Column<double>(type: "float", nullable: false),
                    Deleted = table.Column<bool>(type: "bit", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()"),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()"),
                    Status = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CenterDoctorServiceOnlineConsultations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CenterDoctorServiceOnlineConsultations_Centers_CenterId",
                        column: x => x.CenterId,
                        principalTable: "Centers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CenterDoctorServiceOnlineConsultations_Doctors_DoctorId",
                        column: x => x.DoctorId,
                        principalTable: "Doctors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CenterDoctorServiceOnlineConsultations_ProviderServices_ServicesAvailableId",
                        column: x => x.ServicesAvailableId,
                        principalTable: "ProviderServices",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CenterDoctorServicePricing",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DoctorId = table.Column<int>(type: "int", nullable: false),
                    CenterId = table.Column<int>(type: "int", nullable: false),
                    ProviderServicesId = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PercentagePayment = table.Column<double>(type: "float", nullable: false),
                    Deleted = table.Column<bool>(type: "bit", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()"),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()"),
                    Status = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CenterDoctorServicePricing", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CenterDoctorServicePricing_Centers_CenterId",
                        column: x => x.CenterId,
                        principalTable: "Centers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CenterDoctorServicePricing_Doctors_DoctorId",
                        column: x => x.DoctorId,
                        principalTable: "Doctors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CenterDoctorServicePricing_ProviderServices_ProviderServicesId",
                        column: x => x.ProviderServicesId,
                        principalTable: "ProviderServices",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CenterDoctorServiceOnlineConsultations_CenterId_DoctorId_ServicesAvailableId",
                table: "CenterDoctorServiceOnlineConsultations",
                columns: new[] { "CenterId", "DoctorId", "ServicesAvailableId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CenterDoctorServiceOnlineConsultations_DoctorId",
                table: "CenterDoctorServiceOnlineConsultations",
                column: "DoctorId");

            migrationBuilder.CreateIndex(
                name: "IX_CenterDoctorServiceOnlineConsultations_ServicesAvailableId",
                table: "CenterDoctorServiceOnlineConsultations",
                column: "ServicesAvailableId");

            migrationBuilder.CreateIndex(
                name: "IX_CenterDoctorServicePricing_CenterId",
                table: "CenterDoctorServicePricing",
                column: "CenterId");

            migrationBuilder.CreateIndex(
                name: "IX_CenterDoctorServicePricing_DoctorId_CenterId_ProviderServicesId",
                table: "CenterDoctorServicePricing",
                columns: new[] { "DoctorId", "CenterId", "ProviderServicesId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CenterDoctorServicePricing_ProviderServicesId",
                table: "CenterDoctorServicePricing",
                column: "ProviderServicesId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CenterDoctorServiceOnlineConsultations");

            migrationBuilder.DropTable(
                name: "CenterDoctorServicePricing");

            migrationBuilder.CreateTable(
                name: "CenterDoctorPricings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CenterId = table.Column<int>(type: "int", nullable: false),
                    DoctorId = table.Column<int>(type: "int", nullable: false),
                    ProviderServicesId = table.Column<int>(type: "int", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()"),
                    Deleted = table.Column<bool>(type: "bit", nullable: false),
                    PercentagePayment = table.Column<double>(type: "float", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Status = table.Column<bool>(type: "bit", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CenterDoctorPricings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CenterDoctorPricings_Centers_CenterId",
                        column: x => x.CenterId,
                        principalTable: "Centers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CenterDoctorPricings_Doctors_DoctorId",
                        column: x => x.DoctorId,
                        principalTable: "Doctors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CenterDoctorPricings_ProviderServices_ProviderServicesId",
                        column: x => x.ProviderServicesId,
                        principalTable: "ProviderServices",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CenterDoctorPricings_CenterId",
                table: "CenterDoctorPricings",
                column: "CenterId");

            migrationBuilder.CreateIndex(
                name: "IX_CenterDoctorPricings_DoctorId_CenterId_ProviderServicesId",
                table: "CenterDoctorPricings",
                columns: new[] { "DoctorId", "CenterId", "ProviderServicesId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CenterDoctorPricings_ProviderServicesId",
                table: "CenterDoctorPricings",
                column: "ProviderServicesId");
        }
    }
}
