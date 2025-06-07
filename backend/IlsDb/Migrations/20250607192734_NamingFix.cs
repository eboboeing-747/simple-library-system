using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IlsDb.Migrations
{
    /// <inheritdoc />
    public partial class NamingFix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "pfpPath",
                table: "Users",
                newName: "PfpPath");

            migrationBuilder.RenameColumn(
                name: "name",
                table: "Subsidiaries",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "description",
                table: "Subsidiaries",
                newName: "Description");

            migrationBuilder.RenameColumn(
                name: "address",
                table: "Subsidiaries",
                newName: "Address");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PfpPath",
                table: "Users",
                newName: "pfpPath");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Subsidiaries",
                newName: "name");

            migrationBuilder.RenameColumn(
                name: "Description",
                table: "Subsidiaries",
                newName: "description");

            migrationBuilder.RenameColumn(
                name: "Address",
                table: "Subsidiaries",
                newName: "address");
        }
    }
}
