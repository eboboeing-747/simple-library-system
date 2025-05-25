using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IlsDb.Migrations
{
    /// <inheritdoc />
    public partial class FixUserTypeName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Type",
                table: "UserTypes",
                newName: "Name");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Name",
                table: "UserTypes",
                newName: "Type");
        }
    }
}
