using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DrMeet.Persistence.EF.Migrations
{
    /// <inheritdoc />
    public partial class initdatabase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FullName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Password = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    MobileNumber = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Salt = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    Picture = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    VerifyCode = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    VerifyExpire = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Deleted = table.Column<bool>(type: "bit", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()"),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()"),
                    Status = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Doctors",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    RemoteDoctorId = table.Column<int>(type: "int", nullable: false),
                    ExperienceYears = table.Column<int>(type: "int", nullable: false, defaultValue: 0),
                    Score = table.Column<float>(type: "real", nullable: false, defaultValue: 0f),
                    ShowPicture = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    DayVisitTime = table.Column<int>(type: "int", nullable: false, defaultValue: 0),
                    DoctorGroup = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Bio = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    Description = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: true),
                    Over15YearsOfExperience = table.Column<bool>(type: "bit", nullable: true, defaultValue: false),
                    NumberMedicalSystem = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    ShowInOnlineReserveTime = table.Column<bool>(type: "bit", nullable: false),
                    OverFifteenYearsExperience = table.Column<bool>(type: "bit", nullable: false),
                    WebSite = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    InPerson = table.Column<bool>(type: "bit", nullable: false),
                    PriceInPerson = table.Column<decimal>(type: "decimal(18,2)", nullable: false, defaultValue: 0m),
                    IsVideoConsultation = table.Column<bool>(type: "bit", nullable: false),
                    PriceIsVideoConsultation = table.Column<decimal>(type: "decimal(18,2)", nullable: false, defaultValue: 0m),
                    IsPhoneConsultation = table.Column<bool>(type: "bit", nullable: false),
                    PriceIsPhoneConsultation = table.Column<decimal>(type: "decimal(18,2)", nullable: false, defaultValue: 0m),
                    IsTextConsultation = table.Column<bool>(type: "bit", nullable: false),
                    PriceIsTextConsultation = table.Column<decimal>(type: "decimal(18,2)", nullable: false, defaultValue: 0m),
                    Deleted = table.Column<bool>(type: "bit", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()"),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()"),
                    Status = table.Column<bool>(type: "bit", nullable: false),
                    Gender = table.Column<int>(type: "int", nullable: true),
                    FatherName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BirthDate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NationalCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProvinceId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CityId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Region = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Doctors", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Doctors_Users_Id",
                        column: x => x.Id,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Doctors");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
