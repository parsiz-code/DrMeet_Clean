using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DrMeet.Persistence.EF.Migrations
{
    /// <inheritdoc />
    public partial class addbaseentityCenterServiceSelectedCenterdepartmantSelected : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CreateDate",
                table: "CenterDoctorsServiceSelected",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "GETDATE()");

            migrationBuilder.AddColumn<bool>(
                name: "Deleted",
                table: "CenterDoctorsServiceSelected",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Status",
                table: "CenterDoctorsServiceSelected",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdateDate",
                table: "CenterDoctorsServiceSelected",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "GETDATE()");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreateDate",
                table: "CenterDoctorsDepartmantSelected",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "GETDATE()");

            migrationBuilder.AddColumn<bool>(
                name: "Deleted",
                table: "CenterDoctorsDepartmantSelected",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Status",
                table: "CenterDoctorsDepartmantSelected",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdateDate",
                table: "CenterDoctorsDepartmantSelected",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "GETDATE()");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreateDate",
                table: "CenterDoctorsServiceSelected");

            migrationBuilder.DropColumn(
                name: "Deleted",
                table: "CenterDoctorsServiceSelected");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "CenterDoctorsServiceSelected");

            migrationBuilder.DropColumn(
                name: "UpdateDate",
                table: "CenterDoctorsServiceSelected");

            migrationBuilder.DropColumn(
                name: "CreateDate",
                table: "CenterDoctorsDepartmantSelected");

            migrationBuilder.DropColumn(
                name: "Deleted",
                table: "CenterDoctorsDepartmantSelected");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "CenterDoctorsDepartmantSelected");

            migrationBuilder.DropColumn(
                name: "UpdateDate",
                table: "CenterDoctorsDepartmantSelected");
        }
    }
}
