using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DrMeet.Persistence.EF.Migrations
{
    /// <inheritdoc />
    public partial class renameApplicationSetting : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FileUploadSettings_ApplicationSettings_ApplicationSettingId",
                table: "FileUploadSettings");

            migrationBuilder.DropPrimaryKey(
                name: "PK_FileUploadSettings",
                table: "FileUploadSettings");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ApplicationSettings",
                table: "ApplicationSettings");

            migrationBuilder.RenameTable(
                name: "FileUploadSettings",
                newName: "ApplicationSettingFileUpload");

            migrationBuilder.RenameTable(
                name: "ApplicationSettings",
                newName: "ApplicationSetting");

            migrationBuilder.RenameIndex(
                name: "IX_FileUploadSettings_ApplicationSettingId",
                table: "ApplicationSettingFileUpload",
                newName: "IX_ApplicationSettingFileUpload_ApplicationSettingId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ApplicationSettingFileUpload",
                table: "ApplicationSettingFileUpload",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ApplicationSetting",
                table: "ApplicationSetting",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ApplicationSettingFileUpload_ApplicationSetting_ApplicationSettingId",
                table: "ApplicationSettingFileUpload",
                column: "ApplicationSettingId",
                principalTable: "ApplicationSetting",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ApplicationSettingFileUpload_ApplicationSetting_ApplicationSettingId",
                table: "ApplicationSettingFileUpload");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ApplicationSettingFileUpload",
                table: "ApplicationSettingFileUpload");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ApplicationSetting",
                table: "ApplicationSetting");

            migrationBuilder.RenameTable(
                name: "ApplicationSettingFileUpload",
                newName: "FileUploadSettings");

            migrationBuilder.RenameTable(
                name: "ApplicationSetting",
                newName: "ApplicationSettings");

            migrationBuilder.RenameIndex(
                name: "IX_ApplicationSettingFileUpload_ApplicationSettingId",
                table: "FileUploadSettings",
                newName: "IX_FileUploadSettings_ApplicationSettingId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_FileUploadSettings",
                table: "FileUploadSettings",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ApplicationSettings",
                table: "ApplicationSettings",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_FileUploadSettings_ApplicationSettings_ApplicationSettingId",
                table: "FileUploadSettings",
                column: "ApplicationSettingId",
                principalTable: "ApplicationSettings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
