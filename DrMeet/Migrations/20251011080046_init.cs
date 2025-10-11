using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DrMeet.Api.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ApplicationSetting",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AppVersion = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    ApiVersion = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Deleted = table.Column<bool>(type: "bit", nullable: false),
                    Status = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApplicationSetting", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CenterTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Order = table.Column<int>(type: "int", nullable: false),
                    Deleted = table.Column<bool>(type: "bit", nullable: false),
                    Status = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CenterTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Expertises",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Deleted = table.Column<bool>(type: "bit", nullable: false),
                    Status = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Expertises", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Holidays",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    Date = table.Column<DateOnly>(type: "date", nullable: false),
                    Deleted = table.Column<bool>(type: "bit", nullable: false),
                    Status = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Holidays", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Insurances",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Picture = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    Order = table.Column<int>(type: "int", nullable: false),
                    IsBaseInsurance = table.Column<bool>(type: "bit", nullable: false),
                    Deleted = table.Column<bool>(type: "bit", nullable: false),
                    Status = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Insurances", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "IranProvinces",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IranProvinces", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Licenses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Order = table.Column<int>(type: "int", nullable: false),
                    Deleted = table.Column<bool>(type: "bit", nullable: false),
                    Status = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Licenses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ProviderServices",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Order = table.Column<int>(type: "int", nullable: false),
                    Deleted = table.Column<bool>(type: "bit", nullable: false),
                    Status = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProviderServices", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FullName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Password = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MobileNumber = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Salt = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    Picture = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    VerifyCode = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    VerifyExpire = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UserType = table.Column<int>(type: "int", nullable: true),
                    Deleted = table.Column<bool>(type: "bit", nullable: false),
                    Status = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ApplicationSettingFileUpload",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Type = table.Column<int>(type: "int", nullable: false),
                    MaximumSize = table.Column<long>(type: "bigint", nullable: false),
                    MaximumSizeFriendlyName = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    ApplicationSettingId = table.Column<int>(type: "int", nullable: false),
                    ValidExtensions = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Deleted = table.Column<bool>(type: "bit", nullable: false),
                    Status = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApplicationSettingFileUpload", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ApplicationSettingFileUpload_ApplicationSetting_ApplicationSettingId",
                        column: x => x.ApplicationSettingId,
                        principalTable: "ApplicationSetting",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "IranCities",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ProvinceId = table.Column<int>(type: "int", maxLength: 36, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IranCities", x => x.Id);
                    table.ForeignKey(
                        name: "FK_IranCities_IranProvinces_ProvinceId",
                        column: x => x.ProvinceId,
                        principalTable: "IranProvinces",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Centers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CenterRemoteId = table.Column<int>(type: "int", nullable: false),
                    CenterTypeId = table.Column<int>(type: "int", nullable: false),
                    CenterOfType = table.Column<int>(type: "int", nullable: true),
                    Bio = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    DateOfEstablishment = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Phone = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    FaxNumber = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    WebSite = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Address = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Description = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: true),
                    ClinicId = table.Column<int>(type: "int", nullable: true),
                    OfficeId = table.Column<int>(type: "int", nullable: true),
                    TariffExpirationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Picture = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Deleted = table.Column<bool>(type: "bit", nullable: false),
                    Status = table.Column<bool>(type: "bit", nullable: false),
                    ProvinceId = table.Column<int>(type: "int", nullable: true),
                    CityId = table.Column<int>(type: "int", nullable: true),
                    Region = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Centers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Centers_CenterTypes_CenterTypeId",
                        column: x => x.CenterTypeId,
                        principalTable: "CenterTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Centers_IranCities_CityId",
                        column: x => x.CityId,
                        principalTable: "IranCities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Centers_IranProvinces_ProvinceId",
                        column: x => x.ProvinceId,
                        principalTable: "IranProvinces",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Doctors",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RemoteDoctorId = table.Column<int>(type: "int", nullable: false),
                    ExperienceYears = table.Column<int>(type: "int", nullable: false, defaultValue: 0),
                    Score = table.Column<float>(type: "real", nullable: false, defaultValue: 0f),
                    ShowPicture = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    DayVisitTime = table.Column<int>(type: "int", nullable: false, defaultValue: 0),
                    DoctorGroup = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    Bio = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    Description = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: true),
                    NumberMedicalSystem = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    ShowInOnlineReserveTime = table.Column<bool>(type: "bit", nullable: false),
                    OverFifteenYearsExperience = table.Column<bool>(type: "bit", nullable: false),
                    WebSite = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    InPerson = table.Column<bool>(type: "bit", nullable: false),
                    PriceInPerson = table.Column<decimal>(type: "decimal(18,2)", nullable: false, defaultValue: 0m),
                    IsVideoConsultation = table.Column<bool>(type: "bit", nullable: false),
                    PriceIsVideoConsultation = table.Column<decimal>(type: "decimal(18,2)", nullable: false, defaultValue: 0m),
                    IsPhoneConsultation = table.Column<bool>(type: "bit", nullable: false),
                    PriceIsPhoneConsultation = table.Column<decimal>(type: "decimal(18,2)", nullable: false, defaultValue: 0m),
                    IsTextConsultation = table.Column<bool>(type: "bit", nullable: false),
                    PriceIsTextConsultation = table.Column<decimal>(type: "decimal(18,2)", nullable: false, defaultValue: 0m),
                    Deleted = table.Column<bool>(type: "bit", nullable: false),
                    Status = table.Column<bool>(type: "bit", nullable: false),
                    ProvinceId = table.Column<int>(type: "int", nullable: true),
                    CityId = table.Column<int>(type: "int", nullable: true),
                    Region = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Gender = table.Column<int>(type: "int", nullable: true),
                    FatherName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BirthDate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NationalCode = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Doctors", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Doctors_IranCities_CityId",
                        column: x => x.CityId,
                        principalTable: "IranCities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Doctors_IranProvinces_ProvinceId",
                        column: x => x.ProvinceId,
                        principalTable: "IranProvinces",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Doctors_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Patients",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    PatientRemoteId = table.Column<int>(type: "int", nullable: true),
                    Picture = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    InsuranceId = table.Column<int>(type: "int", nullable: true),
                    SupplementInsuranceId = table.Column<int>(type: "int", nullable: true),
                    Deleted = table.Column<bool>(type: "bit", nullable: false),
                    Status = table.Column<bool>(type: "bit", nullable: false),
                    ProvinceId = table.Column<int>(type: "int", nullable: true),
                    CityId = table.Column<int>(type: "int", nullable: true),
                    Region = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Gender = table.Column<int>(type: "int", nullable: true),
                    FatherName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BirthDate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NationalCode = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Patients", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Patients_Insurances_InsuranceId",
                        column: x => x.InsuranceId,
                        principalTable: "Insurances",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Patients_Insurances_SupplementInsuranceId",
                        column: x => x.SupplementInsuranceId,
                        principalTable: "Insurances",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Patients_IranCities_CityId",
                        column: x => x.CityId,
                        principalTable: "IranCities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Patients_IranProvinces_ProvinceId",
                        column: x => x.ProvinceId,
                        principalTable: "IranProvinces",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Patients_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Blogs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    SummaryText = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    ImagePath = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    Text = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Tags = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true),
                    CenterId = table.Column<int>(type: "int", nullable: false),
                    Deleted = table.Column<bool>(type: "bit", nullable: false),
                    Status = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Blogs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Blogs_Centers_CenterId",
                        column: x => x.CenterId,
                        principalTable: "Centers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CenterComments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CenterId = table.Column<int>(type: "int", nullable: false),
                    Deleted = table.Column<bool>(type: "bit", nullable: false),
                    Status = table.Column<bool>(type: "bit", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Subject = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Text = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: false),
                    Score = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CenterComments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CenterComments_Centers_CenterId",
                        column: x => x.CenterId,
                        principalTable: "Centers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CenterComments_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CenterDepartments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    CenterId = table.Column<int>(type: "int", nullable: true),
                    Deleted = table.Column<bool>(type: "bit", nullable: false),
                    Status = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CenterDepartments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CenterDepartments_Centers_CenterId",
                        column: x => x.CenterId,
                        principalTable: "Centers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "CenterInsurancesSelected",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    InsuranceId = table.Column<int>(type: "int", nullable: true),
                    CenterId = table.Column<int>(type: "int", nullable: true),
                    InsuranceId1 = table.Column<int>(type: "int", nullable: true),
                    Deleted = table.Column<bool>(type: "bit", nullable: false),
                    Status = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CenterInsurancesSelected", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CenterInsurancesSelected_Centers_CenterId",
                        column: x => x.CenterId,
                        principalTable: "Centers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_CenterInsurancesSelected_Insurances_InsuranceId",
                        column: x => x.InsuranceId,
                        principalTable: "Insurances",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_CenterInsurancesSelected_Insurances_InsuranceId1",
                        column: x => x.InsuranceId1,
                        principalTable: "Insurances",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "CenterLicensesSelected",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CenterId = table.Column<int>(type: "int", nullable: true),
                    LicensesId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CenterLicensesSelected", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CenterLicensesSelected_Centers_CenterId",
                        column: x => x.CenterId,
                        principalTable: "Centers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_CenterLicensesSelected_Licenses_LicensesId",
                        column: x => x.LicensesId,
                        principalTable: "Licenses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "CenterLocation",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CenterId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CenterLocation", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CenterLocation_Centers_CenterId",
                        column: x => x.CenterId,
                        principalTable: "Centers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CenterPictures",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CenterId = table.Column<int>(type: "int", nullable: false),
                    Picture = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    PictureType = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CenterPictures", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CenterPictures_Centers_CenterId",
                        column: x => x.CenterId,
                        principalTable: "Centers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CenterQuestionAnswers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CenterId = table.Column<int>(type: "int", nullable: false),
                    ParentId = table.Column<int>(type: "int", nullable: true),
                    Text = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: false),
                    Type = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Deleted = table.Column<bool>(type: "bit", nullable: false),
                    Status = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CenterQuestionAnswers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CenterQuestionAnswers_Centers_CenterId",
                        column: x => x.CenterId,
                        principalTable: "Centers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CenterServicesSelected",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CenterId = table.Column<int>(type: "int", nullable: true),
                    ServiceId = table.Column<int>(type: "int", nullable: true),
                    Deleted = table.Column<bool>(type: "bit", nullable: false),
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
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    CenterId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CenterUsersSelected", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CenterUsersSelected_Centers_CenterId",
                        column: x => x.CenterId,
                        principalTable: "Centers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CenterUsersSelected_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Sliders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    ImagePath = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    CenterId = table.Column<int>(type: "int", nullable: false),
                    Deleted = table.Column<bool>(type: "bit", nullable: false),
                    Status = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sliders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Sliders_Centers_CenterId",
                        column: x => x.CenterId,
                        principalTable: "Centers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SocialMediaAccounts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CenterId = table.Column<int>(type: "int", nullable: false),
                    Platform = table.Column<int>(type: "int", nullable: false),
                    UsernameOrUrl = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true),
                    Deleted = table.Column<bool>(type: "bit", nullable: false),
                    Status = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SocialMediaAccounts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SocialMediaAccounts_Centers_CenterId",
                        column: x => x.CenterId,
                        principalTable: "Centers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CenterDoctorsSelected",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CenterId = table.Column<int>(type: "int", nullable: true),
                    DoctorId = table.Column<int>(type: "int", nullable: true),
                    Deleted = table.Column<bool>(type: "bit", nullable: false),
                    Status = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CenterDoctorsSelected", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CenterDoctorsSelected_Centers_CenterId",
                        column: x => x.CenterId,
                        principalTable: "Centers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_CenterDoctorsSelected_Doctors_DoctorId",
                        column: x => x.DoctorId,
                        principalTable: "Doctors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "DoctorComments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BehaviorScore = table.Column<int>(type: "int", nullable: false),
                    TreatmentQualityScore = table.Column<int>(type: "int", nullable: false),
                    EconomicEfficiencyScore = table.Column<int>(type: "int", nullable: false),
                    RecoveryScore = table.Column<int>(type: "int", nullable: false),
                    DoctorId = table.Column<int>(type: "int", nullable: false),
                    Deleted = table.Column<bool>(type: "bit", nullable: false),
                    Status = table.Column<bool>(type: "bit", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Subject = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Text = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Score = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DoctorComments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DoctorComments_Doctors_DoctorId",
                        column: x => x.DoctorId,
                        principalTable: "Doctors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DoctorComments_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "DoctorExpertises",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DoctorId = table.Column<int>(type: "int", nullable: false),
                    ExpertiseId = table.Column<int>(type: "int", nullable: false),
                    Deleted = table.Column<bool>(type: "bit", nullable: false),
                    Status = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DoctorExpertises", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DoctorExpertises_Doctors_DoctorId",
                        column: x => x.DoctorId,
                        principalTable: "Doctors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DoctorExpertises_Expertises_ExpertiseId",
                        column: x => x.ExpertiseId,
                        principalTable: "Expertises",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DoctorSocialMediaAccount",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DoctorId = table.Column<int>(type: "int", nullable: false),
                    Platform = table.Column<int>(type: "int", nullable: false),
                    UsernameOrUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Deleted = table.Column<bool>(type: "bit", nullable: false),
                    Status = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DoctorSocialMediaAccount", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DoctorSocialMediaAccount_Doctors_DoctorId",
                        column: x => x.DoctorId,
                        principalTable: "Doctors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CenterPatientSelected",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CenterId = table.Column<int>(type: "int", nullable: false),
                    PatientId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CenterPatientSelected", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CenterPatientSelected_Centers_CenterId",
                        column: x => x.CenterId,
                        principalTable: "Centers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CenterPatientSelected_Patients_PatientId",
                        column: x => x.PatientId,
                        principalTable: "Patients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BlogComments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BlogId = table.Column<int>(type: "int", nullable: false),
                    Deleted = table.Column<bool>(type: "bit", nullable: false),
                    Status = table.Column<bool>(type: "bit", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Subject = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    Text = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Score = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BlogComments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BlogComments_Blogs_BlogId",
                        column: x => x.BlogId,
                        principalTable: "Blogs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BlogComments_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CenterQuestionAnswerCommentPoints",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CenterQuestionAnswerId = table.Column<int>(type: "int", nullable: false),
                    Message = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    IsNegativePoints = table.Column<bool>(type: "bit", nullable: false),
                    CenterQuestionAnswerId1 = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CenterQuestionAnswerCommentPoints", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CenterQuestionAnswerCommentPoints_CenterQuestionAnswers_CenterQuestionAnswerId",
                        column: x => x.CenterQuestionAnswerId,
                        principalTable: "CenterQuestionAnswers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CenterQuestionAnswerCommentPoints_CenterQuestionAnswers_CenterQuestionAnswerId1",
                        column: x => x.CenterQuestionAnswerId1,
                        principalTable: "CenterQuestionAnswers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "CenterDoctorsDepartmantSelected",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CenterId = table.Column<int>(type: "int", nullable: true),
                    DoctorId = table.Column<int>(type: "int", nullable: true),
                    CenterDepartmentId = table.Column<int>(type: "int", nullable: true),
                    CenterDoctorsSelectedId = table.Column<int>(type: "int", nullable: true),
                    Deleted = table.Column<bool>(type: "bit", nullable: false),
                    Status = table.Column<bool>(type: "bit", nullable: false)
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
                        name: "FK_CenterDoctorsDepartmantSelected_CenterDoctorsSelected_CenterDoctorsSelectedId",
                        column: x => x.CenterDoctorsSelectedId,
                        principalTable: "CenterDoctorsSelected",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_CenterDoctorsDepartmantSelected_Centers_CenterId",
                        column: x => x.CenterId,
                        principalTable: "Centers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_CenterDoctorsDepartmantSelected_Doctors_DoctorId",
                        column: x => x.DoctorId,
                        principalTable: "Doctors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "CenterDoctorServiceOnlineConsultations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CenterId = table.Column<int>(type: "int", nullable: true),
                    DoctorId = table.Column<int>(type: "int", nullable: true),
                    ServicesAvailableId = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PercentagePayment = table.Column<double>(type: "float", nullable: false),
                    CenterDoctorsSelectedId = table.Column<int>(type: "int", nullable: true),
                    Deleted = table.Column<bool>(type: "bit", nullable: false),
                    Status = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CenterDoctorServiceOnlineConsultations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CenterDoctorServiceOnlineConsultations_CenterDoctorsSelected_CenterDoctorsSelectedId",
                        column: x => x.CenterDoctorsSelectedId,
                        principalTable: "CenterDoctorsSelected",
                        principalColumn: "Id");
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
                    CenterId = table.Column<int>(type: "int", nullable: true),
                    DoctorId = table.Column<int>(type: "int", nullable: false),
                    ProviderServicesId = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PercentagePayment = table.Column<double>(type: "float", nullable: false),
                    CenterDoctorsSelectedId = table.Column<int>(type: "int", nullable: true),
                    Deleted = table.Column<bool>(type: "bit", nullable: false),
                    Status = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CenterDoctorServicePricing", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CenterDoctorServicePricing_CenterDoctorsSelected_CenterDoctorsSelectedId",
                        column: x => x.CenterDoctorsSelectedId,
                        principalTable: "CenterDoctorsSelected",
                        principalColumn: "Id");
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

            migrationBuilder.CreateTable(
                name: "CenterDoctorsServiceSelected",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CenterId = table.Column<int>(type: "int", nullable: true),
                    DoctorId = table.Column<int>(type: "int", nullable: true),
                    ProviderServiceId = table.Column<int>(type: "int", nullable: true),
                    CenterDoctorsSelectedId = table.Column<int>(type: "int", nullable: true),
                    Deleted = table.Column<bool>(type: "bit", nullable: false),
                    Status = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CenterDoctorsServiceSelected", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CenterDoctorsServiceSelected_CenterDoctorsSelected_CenterDoctorsSelectedId",
                        column: x => x.CenterDoctorsSelectedId,
                        principalTable: "CenterDoctorsSelected",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_CenterDoctorsServiceSelected_Centers_CenterId",
                        column: x => x.CenterId,
                        principalTable: "Centers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_CenterDoctorsServiceSelected_Doctors_DoctorId",
                        column: x => x.DoctorId,
                        principalTable: "Doctors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_CenterDoctorsServiceSelected_ProviderServices_ProviderServiceId",
                        column: x => x.ProviderServiceId,
                        principalTable: "ProviderServices",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "DoctorShifts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CenterDoctorsDepartmantId = table.Column<int>(type: "int", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    StartTime = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    EndTime = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    MeetTime = table.Column<int>(type: "int", nullable: false),
                    ActivityStatus = table.Column<int>(type: "int", nullable: false),
                    ShiftType = table.Column<int>(type: "int", nullable: false),
                    DayOfWeek = table.Column<int>(type: "int", nullable: false),
                    Deleted = table.Column<bool>(type: "bit", nullable: false),
                    Status = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DoctorShifts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DoctorShifts_CenterDoctorsDepartmantSelected_CenterDoctorsDepartmantId",
                        column: x => x.CenterDoctorsDepartmantId,
                        principalTable: "CenterDoctorsDepartmantSelected",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "DoctorShiftServices",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DoctorShiftId = table.Column<int>(type: "int", nullable: false),
                    CenterDoctorsServiceId = table.Column<int>(type: "int", nullable: false),
                    Deleted = table.Column<bool>(type: "bit", nullable: false),
                    Status = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DoctorShiftServices", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DoctorShiftServices_CenterDoctorsServiceSelected_CenterDoctorsServiceId",
                        column: x => x.CenterDoctorsServiceId,
                        principalTable: "CenterDoctorsServiceSelected",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DoctorShiftServices_DoctorShifts_DoctorShiftId",
                        column: x => x.DoctorShiftId,
                        principalTable: "DoctorShifts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DoctorShiftTimeItems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StartTime = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    EndTime = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    IsShiftAvailable = table.Column<bool>(type: "bit", nullable: false),
                    DoctorShiftId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DoctorShiftTimeItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DoctorShiftTimeItems_DoctorShifts_DoctorShiftId",
                        column: x => x.DoctorShiftId,
                        principalTable: "DoctorShifts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DoctorReserveTimes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    DoctorTimeId = table.Column<int>(type: "int", maxLength: 50, nullable: true),
                    CenterDoctorsServiceId = table.Column<int>(type: "int", maxLength: 50, nullable: false),
                    PatientId = table.Column<int>(type: "int", nullable: false),
                    ShiftStatus = table.Column<int>(type: "int", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Deleted = table.Column<bool>(type: "bit", nullable: false),
                    Status = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DoctorReserveTimes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DoctorReserveTimes_CenterDoctorsServiceSelected_CenterDoctorsServiceId",
                        column: x => x.CenterDoctorsServiceId,
                        principalTable: "CenterDoctorsServiceSelected",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DoctorReserveTimes_DoctorShiftTimeItems_DoctorTimeId",
                        column: x => x.DoctorTimeId,
                        principalTable: "DoctorShiftTimeItems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DoctorReserveTimes_Patients_PatientId",
                        column: x => x.PatientId,
                        principalTable: "Patients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "CenterTypes",
                columns: new[] { "Id", "Deleted", "Name", "Order", "Status" },
                values: new object[,]
                {
                    { 1, false, "کلینیک تخصصی", 1, true },
                    { 2, false, "بیمارستان عمومی", 2, true },
                    { 3, false, "درمانگاه شبانه‌روزی", 3, true },
                    { 4, false, "مرکز تصویربرداری", 4, true },
                    { 5, false, "آزمایشگاه تشخیص طبی", 5, true }
                });

            migrationBuilder.InsertData(
                table: "IranProvinces",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "تهران" },
                    { 2, "فارس" },
                    { 3, "اصفهان" },
                    { 4, "خراسان رضوی" },
                    { 5, "آذربایجان شرقی" }
                });

            migrationBuilder.InsertData(
                table: "IranCities",
                columns: new[] { "Id", "Name", "ProvinceId" },
                values: new object[,]
                {
                    { 1, "تهران", 1 },
                    { 2, "شیراز", 2 },
                    { 3, "اصفهان", 3 },
                    { 4, "مشهد", 4 },
                    { 5, "تبریز", 5 },
                    { 6, "ورامین", 1 },
                    { 7, "مرودشت", 2 },
                    { 8, "کاشان", 3 },
                    { 9, "نیشابور", 4 },
                    { 10, "مراغه", 5 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_ApplicationSettingFileUpload_ApplicationSettingId",
                table: "ApplicationSettingFileUpload",
                column: "ApplicationSettingId");

            migrationBuilder.CreateIndex(
                name: "IX_BlogComments_BlogId",
                table: "BlogComments",
                column: "BlogId");

            migrationBuilder.CreateIndex(
                name: "IX_BlogComments_UserId",
                table: "BlogComments",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Blogs_CenterId",
                table: "Blogs",
                column: "CenterId");

            migrationBuilder.CreateIndex(
                name: "IX_CenterComments_CenterId",
                table: "CenterComments",
                column: "CenterId");

            migrationBuilder.CreateIndex(
                name: "IX_CenterComments_UserId",
                table: "CenterComments",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_CenterDepartments_CenterId",
                table: "CenterDepartments",
                column: "CenterId");

            migrationBuilder.CreateIndex(
                name: "IX_CenterDoctorsDepartmantSelected_CenterDepartmentId",
                table: "CenterDoctorsDepartmantSelected",
                column: "CenterDepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_CenterDoctorsDepartmantSelected_CenterDoctorsSelectedId",
                table: "CenterDoctorsDepartmantSelected",
                column: "CenterDoctorsSelectedId");

            migrationBuilder.CreateIndex(
                name: "IX_CenterDoctorsDepartmantSelected_CenterId_DoctorId_CenterDepartmentId",
                table: "CenterDoctorsDepartmantSelected",
                columns: new[] { "CenterId", "DoctorId", "CenterDepartmentId" },
                unique: true,
                filter: "[CenterId] IS NOT NULL AND [DoctorId] IS NOT NULL AND [CenterDepartmentId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_CenterDoctorsDepartmantSelected_DoctorId",
                table: "CenterDoctorsDepartmantSelected",
                column: "DoctorId");

            migrationBuilder.CreateIndex(
                name: "IX_CenterDoctorServiceOnlineConsultations_CenterDoctorsSelectedId",
                table: "CenterDoctorServiceOnlineConsultations",
                column: "CenterDoctorsSelectedId");

            migrationBuilder.CreateIndex(
                name: "IX_CenterDoctorServiceOnlineConsultations_CenterId_DoctorId_ServicesAvailableId",
                table: "CenterDoctorServiceOnlineConsultations",
                columns: new[] { "CenterId", "DoctorId", "ServicesAvailableId" },
                unique: true,
                filter: "[CenterId] IS NOT NULL AND [DoctorId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_CenterDoctorServiceOnlineConsultations_DoctorId",
                table: "CenterDoctorServiceOnlineConsultations",
                column: "DoctorId");

            migrationBuilder.CreateIndex(
                name: "IX_CenterDoctorServiceOnlineConsultations_ServicesAvailableId",
                table: "CenterDoctorServiceOnlineConsultations",
                column: "ServicesAvailableId");

            migrationBuilder.CreateIndex(
                name: "IX_CenterDoctorServicePricing_CenterDoctorsSelectedId",
                table: "CenterDoctorServicePricing",
                column: "CenterDoctorsSelectedId");

            migrationBuilder.CreateIndex(
                name: "IX_CenterDoctorServicePricing_CenterId",
                table: "CenterDoctorServicePricing",
                column: "CenterId");

            migrationBuilder.CreateIndex(
                name: "IX_CenterDoctorServicePricing_DoctorId_CenterId_ProviderServicesId",
                table: "CenterDoctorServicePricing",
                columns: new[] { "DoctorId", "CenterId", "ProviderServicesId" },
                unique: true,
                filter: "[CenterId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_CenterDoctorServicePricing_ProviderServicesId",
                table: "CenterDoctorServicePricing",
                column: "ProviderServicesId");

            migrationBuilder.CreateIndex(
                name: "IX_CenterDoctorsSelected_CenterId_DoctorId",
                table: "CenterDoctorsSelected",
                columns: new[] { "CenterId", "DoctorId" },
                unique: true,
                filter: "[CenterId] IS NOT NULL AND [DoctorId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_CenterDoctorsSelected_DoctorId",
                table: "CenterDoctorsSelected",
                column: "DoctorId");

            migrationBuilder.CreateIndex(
                name: "IX_CenterDoctorsServiceSelected_CenterDoctorsSelectedId",
                table: "CenterDoctorsServiceSelected",
                column: "CenterDoctorsSelectedId");

            migrationBuilder.CreateIndex(
                name: "IX_CenterDoctorsServiceSelected_CenterId_DoctorId_ProviderServiceId",
                table: "CenterDoctorsServiceSelected",
                columns: new[] { "CenterId", "DoctorId", "ProviderServiceId" },
                unique: true,
                filter: "[CenterId] IS NOT NULL AND [DoctorId] IS NOT NULL AND [ProviderServiceId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_CenterDoctorsServiceSelected_DoctorId",
                table: "CenterDoctorsServiceSelected",
                column: "DoctorId");

            migrationBuilder.CreateIndex(
                name: "IX_CenterDoctorsServiceSelected_ProviderServiceId",
                table: "CenterDoctorsServiceSelected",
                column: "ProviderServiceId");

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
                name: "IX_CenterLicensesSelected_CenterId_LicensesId",
                table: "CenterLicensesSelected",
                columns: new[] { "CenterId", "LicensesId" },
                unique: true,
                filter: "[CenterId] IS NOT NULL AND [LicensesId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_CenterLicensesSelected_LicensesId",
                table: "CenterLicensesSelected",
                column: "LicensesId");

            migrationBuilder.CreateIndex(
                name: "IX_CenterLocation_CenterId",
                table: "CenterLocation",
                column: "CenterId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CenterPatientSelected_CenterId",
                table: "CenterPatientSelected",
                column: "CenterId");

            migrationBuilder.CreateIndex(
                name: "IX_CenterPatientSelected_PatientId",
                table: "CenterPatientSelected",
                column: "PatientId");

            migrationBuilder.CreateIndex(
                name: "IX_CenterPictures_CenterId",
                table: "CenterPictures",
                column: "CenterId");

            migrationBuilder.CreateIndex(
                name: "IX_CenterQuestionAnswerCommentPoints_CenterQuestionAnswerId",
                table: "CenterQuestionAnswerCommentPoints",
                column: "CenterQuestionAnswerId");

            migrationBuilder.CreateIndex(
                name: "IX_CenterQuestionAnswerCommentPoints_CenterQuestionAnswerId1",
                table: "CenterQuestionAnswerCommentPoints",
                column: "CenterQuestionAnswerId1");

            migrationBuilder.CreateIndex(
                name: "IX_CenterQuestionAnswers_CenterId",
                table: "CenterQuestionAnswers",
                column: "CenterId");

            migrationBuilder.CreateIndex(
                name: "IX_Centers_CenterTypeId",
                table: "Centers",
                column: "CenterTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Centers_CityId",
                table: "Centers",
                column: "CityId");

            migrationBuilder.CreateIndex(
                name: "IX_Centers_ProvinceId",
                table: "Centers",
                column: "ProvinceId");

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

            migrationBuilder.CreateIndex(
                name: "IX_CenterUsersSelected_UserId",
                table: "CenterUsersSelected",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_DoctorComments_DoctorId_UserId",
                table: "DoctorComments",
                columns: new[] { "DoctorId", "UserId" },
                unique: true,
                filter: "[UserId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_DoctorComments_UserId",
                table: "DoctorComments",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_DoctorExpertises_DoctorId_ExpertiseId",
                table: "DoctorExpertises",
                columns: new[] { "DoctorId", "ExpertiseId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_DoctorExpertises_ExpertiseId",
                table: "DoctorExpertises",
                column: "ExpertiseId");

            migrationBuilder.CreateIndex(
                name: "IX_DoctorReserveTimes_CenterDoctorsServiceId",
                table: "DoctorReserveTimes",
                column: "CenterDoctorsServiceId");

            migrationBuilder.CreateIndex(
                name: "IX_DoctorReserveTimes_DoctorTimeId",
                table: "DoctorReserveTimes",
                column: "DoctorTimeId");

            migrationBuilder.CreateIndex(
                name: "IX_DoctorReserveTimes_PatientId_Date_CenterDoctorsServiceId",
                table: "DoctorReserveTimes",
                columns: new[] { "PatientId", "Date", "CenterDoctorsServiceId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Doctors_CityId",
                table: "Doctors",
                column: "CityId");

            migrationBuilder.CreateIndex(
                name: "IX_Doctors_ProvinceId",
                table: "Doctors",
                column: "ProvinceId");

            migrationBuilder.CreateIndex(
                name: "IX_Doctors_UserId",
                table: "Doctors",
                column: "UserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_DoctorShifts_CenterDoctorsDepartmantId_DayOfWeek_StartTime_EndTime",
                table: "DoctorShifts",
                columns: new[] { "CenterDoctorsDepartmantId", "DayOfWeek", "StartTime", "EndTime" },
                unique: true,
                filter: "[CenterDoctorsDepartmantId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_DoctorShiftServices_CenterDoctorsServiceId",
                table: "DoctorShiftServices",
                column: "CenterDoctorsServiceId");

            migrationBuilder.CreateIndex(
                name: "IX_DoctorShiftServices_DoctorShiftId_CenterDoctorsServiceId",
                table: "DoctorShiftServices",
                columns: new[] { "DoctorShiftId", "CenterDoctorsServiceId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_DoctorShiftTimeItems_DoctorShiftId_StartTime_EndTime",
                table: "DoctorShiftTimeItems",
                columns: new[] { "DoctorShiftId", "StartTime", "EndTime" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_DoctorSocialMediaAccount_DoctorId",
                table: "DoctorSocialMediaAccount",
                column: "DoctorId");

            migrationBuilder.CreateIndex(
                name: "IX_Expertises_Name",
                table: "Expertises",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Holidays_Date",
                table: "Holidays",
                column: "Date",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Insurances_Name",
                table: "Insurances",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_IranCities_ProvinceId",
                table: "IranCities",
                column: "ProvinceId");

            migrationBuilder.CreateIndex(
                name: "IX_Patients_CityId",
                table: "Patients",
                column: "CityId");

            migrationBuilder.CreateIndex(
                name: "IX_Patients_InsuranceId",
                table: "Patients",
                column: "InsuranceId");

            migrationBuilder.CreateIndex(
                name: "IX_Patients_ProvinceId",
                table: "Patients",
                column: "ProvinceId");

            migrationBuilder.CreateIndex(
                name: "IX_Patients_SupplementInsuranceId",
                table: "Patients",
                column: "SupplementInsuranceId");

            migrationBuilder.CreateIndex(
                name: "IX_Patients_UserId",
                table: "Patients",
                column: "UserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Sliders_CenterId",
                table: "Sliders",
                column: "CenterId");

            migrationBuilder.CreateIndex(
                name: "IX_SocialMediaAccounts_CenterId",
                table: "SocialMediaAccounts",
                column: "CenterId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ApplicationSettingFileUpload");

            migrationBuilder.DropTable(
                name: "BlogComments");

            migrationBuilder.DropTable(
                name: "CenterComments");

            migrationBuilder.DropTable(
                name: "CenterDoctorServiceOnlineConsultations");

            migrationBuilder.DropTable(
                name: "CenterDoctorServicePricing");

            migrationBuilder.DropTable(
                name: "CenterInsurancesSelected");

            migrationBuilder.DropTable(
                name: "CenterLicensesSelected");

            migrationBuilder.DropTable(
                name: "CenterLocation");

            migrationBuilder.DropTable(
                name: "CenterPatientSelected");

            migrationBuilder.DropTable(
                name: "CenterPictures");

            migrationBuilder.DropTable(
                name: "CenterQuestionAnswerCommentPoints");

            migrationBuilder.DropTable(
                name: "CenterServicesSelected");

            migrationBuilder.DropTable(
                name: "CenterUsersSelected");

            migrationBuilder.DropTable(
                name: "DoctorComments");

            migrationBuilder.DropTable(
                name: "DoctorExpertises");

            migrationBuilder.DropTable(
                name: "DoctorReserveTimes");

            migrationBuilder.DropTable(
                name: "DoctorShiftServices");

            migrationBuilder.DropTable(
                name: "DoctorSocialMediaAccount");

            migrationBuilder.DropTable(
                name: "Holidays");

            migrationBuilder.DropTable(
                name: "Sliders");

            migrationBuilder.DropTable(
                name: "SocialMediaAccounts");

            migrationBuilder.DropTable(
                name: "ApplicationSetting");

            migrationBuilder.DropTable(
                name: "Blogs");

            migrationBuilder.DropTable(
                name: "Licenses");

            migrationBuilder.DropTable(
                name: "CenterQuestionAnswers");

            migrationBuilder.DropTable(
                name: "Expertises");

            migrationBuilder.DropTable(
                name: "DoctorShiftTimeItems");

            migrationBuilder.DropTable(
                name: "Patients");

            migrationBuilder.DropTable(
                name: "CenterDoctorsServiceSelected");

            migrationBuilder.DropTable(
                name: "DoctorShifts");

            migrationBuilder.DropTable(
                name: "Insurances");

            migrationBuilder.DropTable(
                name: "ProviderServices");

            migrationBuilder.DropTable(
                name: "CenterDoctorsDepartmantSelected");

            migrationBuilder.DropTable(
                name: "CenterDepartments");

            migrationBuilder.DropTable(
                name: "CenterDoctorsSelected");

            migrationBuilder.DropTable(
                name: "Centers");

            migrationBuilder.DropTable(
                name: "Doctors");

            migrationBuilder.DropTable(
                name: "CenterTypes");

            migrationBuilder.DropTable(
                name: "IranCities");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "IranProvinces");
        }
    }
}
