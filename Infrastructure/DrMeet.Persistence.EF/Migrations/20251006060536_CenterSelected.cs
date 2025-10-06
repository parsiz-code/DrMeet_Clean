using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DrMeet.Persistence.EF.Migrations
{
    /// <inheritdoc />
    public partial class CenterSelected : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CenterInsurances_Centers_CenterId",
                table: "CenterInsurances");

            migrationBuilder.DropTable(
                name: "CenterServices");

            migrationBuilder.DropTable(
                name: "CenterUsers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CenterInsurances",
                table: "CenterInsurances");

            migrationBuilder.DropIndex(
                name: "IX_CenterInsurances_CenterId",
                table: "CenterInsurances");

            migrationBuilder.DropIndex(
                name: "IX_CenterInsurances_Name_CenterId",
                table: "CenterInsurances");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "CenterInsurances");

            migrationBuilder.RenameTable(
                name: "CenterInsurances",
                newName: "CenterInsurancesSelected");

            migrationBuilder.AddColumn<int>(
                name: "InsuranceId",
                table: "CenterInsurancesSelected",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "InsuranceId1",
                table: "CenterInsurancesSelected",
                type: "int",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_CenterInsurancesSelected",
                table: "CenterInsurancesSelected",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "CenterServicesSelected",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CenterId = table.Column<int>(type: "int", nullable: true),
                    ServiceId = table.Column<int>(type: "int", nullable: true),
                    Deleted = table.Column<bool>(type: "bit", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()"),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()"),
                    Status = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CenterServicesSelected", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CenterServicesSelected_Centers_CenterId",
                        column: x => x.CenterId,
                        principalTable: "Centers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CenterServicesSelected_ProviderServices_ServiceId",
                        column: x => x.ServiceId,
                        principalTable: "ProviderServices",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CenterUsersSelected",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false),
                    CenterId = table.Column<int>(type: "int", nullable: false),
                    Id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CenterUsersSelected", x => new { x.UserId, x.CenterId });
                    table.ForeignKey(
                        name: "FK_CenterUsersSelected_Centers_CenterId",
                        column: x => x.CenterId,
                        principalTable: "Centers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CenterUsersSelected_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CenterInsurancesSelected_CenterId_InsuranceId",
                table: "CenterInsurancesSelected",
                columns: new[] { "CenterId", "InsuranceId" },
                unique: true,
                filter: "[CenterId] IS NOT NULL AND [InsuranceId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_CenterInsurancesSelected_InsuranceId",
                table: "CenterInsurancesSelected",
                column: "InsuranceId");

            migrationBuilder.CreateIndex(
                name: "IX_CenterInsurancesSelected_InsuranceId1",
                table: "CenterInsurancesSelected",
                column: "InsuranceId1");

            migrationBuilder.CreateIndex(
                name: "IX_CenterServicesSelected_CenterId_ServiceId",
                table: "CenterServicesSelected",
                columns: new[] { "CenterId", "ServiceId" },
                unique: true,
                filter: "[CenterId] IS NOT NULL AND [ServiceId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_CenterServicesSelected_ServiceId",
                table: "CenterServicesSelected",
                column: "ServiceId");

            migrationBuilder.CreateIndex(
                name: "IX_CenterUsersSelected_CenterId",
                table: "CenterUsersSelected",
                column: "CenterId");

            migrationBuilder.AddForeignKey(
                name: "FK_CenterInsurancesSelected_Centers_CenterId",
                table: "CenterInsurancesSelected",
                column: "CenterId",
                principalTable: "Centers",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_CenterInsurancesSelected_Insurances_InsuranceId",
                table: "CenterInsurancesSelected",
                column: "InsuranceId",
                principalTable: "Insurances",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_CenterInsurancesSelected_Insurances_InsuranceId1",
                table: "CenterInsurancesSelected",
                column: "InsuranceId1",
                principalTable: "Insurances",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CenterInsurancesSelected_Centers_CenterId",
                table: "CenterInsurancesSelected");

            migrationBuilder.DropForeignKey(
                name: "FK_CenterInsurancesSelected_Insurances_InsuranceId",
                table: "CenterInsurancesSelected");

            migrationBuilder.DropForeignKey(
                name: "FK_CenterInsurancesSelected_Insurances_InsuranceId1",
                table: "CenterInsurancesSelected");

            migrationBuilder.DropTable(
                name: "CenterServicesSelected");

            migrationBuilder.DropTable(
                name: "CenterUsersSelected");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CenterInsurancesSelected",
                table: "CenterInsurancesSelected");

            migrationBuilder.DropIndex(
                name: "IX_CenterInsurancesSelected_CenterId_InsuranceId",
                table: "CenterInsurancesSelected");

            migrationBuilder.DropIndex(
                name: "IX_CenterInsurancesSelected_InsuranceId",
                table: "CenterInsurancesSelected");

            migrationBuilder.DropIndex(
                name: "IX_CenterInsurancesSelected_InsuranceId1",
                table: "CenterInsurancesSelected");

            migrationBuilder.DropColumn(
                name: "InsuranceId",
                table: "CenterInsurancesSelected");

            migrationBuilder.DropColumn(
                name: "InsuranceId1",
                table: "CenterInsurancesSelected");

            migrationBuilder.RenameTable(
                name: "CenterInsurancesSelected",
                newName: "CenterInsurances");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "CenterInsurances",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CenterInsurances",
                table: "CenterInsurances",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "CenterServices",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CenterId = table.Column<int>(type: "int", nullable: true),
                    ServiceId = table.Column<int>(type: "int", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()"),
                    Deleted = table.Column<bool>(type: "bit", nullable: false),
                    Status = table.Column<bool>(type: "bit", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CenterServices", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CenterServices_Centers_CenterId",
                        column: x => x.CenterId,
                        principalTable: "Centers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CenterServices_ProviderServices_ServiceId",
                        column: x => x.ServiceId,
                        principalTable: "ProviderServices",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CenterUsers",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false),
                    CenterId = table.Column<int>(type: "int", nullable: false),
                    Id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CenterUsers", x => new { x.UserId, x.CenterId });
                    table.ForeignKey(
                        name: "FK_CenterUsers_Centers_CenterId",
                        column: x => x.CenterId,
                        principalTable: "Centers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CenterUsers_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CenterInsurances_CenterId",
                table: "CenterInsurances",
                column: "CenterId");

            migrationBuilder.CreateIndex(
                name: "IX_CenterInsurances_Name_CenterId",
                table: "CenterInsurances",
                columns: new[] { "Name", "CenterId" },
                unique: true,
                filter: "[CenterId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_CenterServices_CenterId_ServiceId",
                table: "CenterServices",
                columns: new[] { "CenterId", "ServiceId" },
                unique: true,
                filter: "[CenterId] IS NOT NULL AND [ServiceId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_CenterServices_ServiceId",
                table: "CenterServices",
                column: "ServiceId");

            migrationBuilder.CreateIndex(
                name: "IX_CenterUsers_CenterId",
                table: "CenterUsers",
                column: "CenterId");

            migrationBuilder.AddForeignKey(
                name: "FK_CenterInsurances_Centers_CenterId",
                table: "CenterInsurances",
                column: "CenterId",
                principalTable: "Centers",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }
    }
}
