using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DrMeet.Persistence.EF.Migrations
{
    /// <inheritdoc />
    public partial class ApplicationSetting : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ApplicationSettings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AppVersion = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    ApiVersion = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Deleted = table.Column<bool>(type: "bit", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApplicationSettings", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CenterQuestionAnswers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CenterId = table.Column<int>(type: "int", nullable: false),
                    ParentId = table.Column<int>(type: "int", nullable: true),
                    Text = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: false),
                    Type = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Deleted = table.Column<bool>(type: "bit", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()"),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()"),
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
                name: "FileUploadSettings",
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
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FileUploadSettings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FileUploadSettings_ApplicationSettings_ApplicationSettingId",
                        column: x => x.ApplicationSettingId,
                        principalTable: "ApplicationSettings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
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
                name: "IX_FileUploadSettings_ApplicationSettingId",
                table: "FileUploadSettings",
                column: "ApplicationSettingId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CenterQuestionAnswerCommentPoints");

            migrationBuilder.DropTable(
                name: "FileUploadSettings");

            migrationBuilder.DropTable(
                name: "CenterQuestionAnswers");

            migrationBuilder.DropTable(
                name: "ApplicationSettings");
        }
    }
}
