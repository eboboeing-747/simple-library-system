using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IlsDb.Migrations
{
    /// <inheritdoc />
    public partial class addUserPfp : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "pfpPath",
                table: "Users",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "pfpPath",
                table: "Users");
        }
    }
}
