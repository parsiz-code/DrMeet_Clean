using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DrMeet.Persistence.EF.Migrations
{
    /// <inheritdoc />
    public partial class addpatientuser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Center_Users_UserId",
                table: "Center");

            migrationBuilder.DropForeignKey(
                name: "FK_CenterUsers_Center_CenterId",
                table: "CenterUsers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Center",
                table: "Center");

            migrationBuilder.RenameTable(
                name: "Center",
                newName: "Centers");

            migrationBuilder.RenameIndex(
                name: "IX_Center_UserId",
                table: "Centers",
                newName: "IX_Centers_UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Centers",
                table: "Centers",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Centers_Users_UserId",
                table: "Centers",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CenterUsers_Centers_CenterId",
                table: "CenterUsers",
                column: "CenterId",
                principalTable: "Centers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Centers_Users_UserId",
                table: "Centers");

            migrationBuilder.DropForeignKey(
                name: "FK_CenterUsers_Centers_CenterId",
                table: "CenterUsers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Centers",
                table: "Centers");

            migrationBuilder.RenameTable(
                name: "Centers",
                newName: "Center");

            migrationBuilder.RenameIndex(
                name: "IX_Centers_UserId",
                table: "Center",
                newName: "IX_Center_UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Center",
                table: "Center",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Center_Users_UserId",
                table: "Center",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CenterUsers_Center_CenterId",
                table: "CenterUsers",
                column: "CenterId",
                principalTable: "Center",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
