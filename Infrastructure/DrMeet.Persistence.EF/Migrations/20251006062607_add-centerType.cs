using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DrMeet.Persistence.EF.Migrations
{
    /// <inheritdoc />
    public partial class addcenterType : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CenterOfType",
                table: "Centers",
                type: "int",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CenterOfType",
                table: "Centers");
        }
    }
}
