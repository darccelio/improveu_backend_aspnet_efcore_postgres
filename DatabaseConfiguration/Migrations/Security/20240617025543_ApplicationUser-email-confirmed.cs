using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ImproveU_backend.DatabaseConfiguration.Migrations.Security
{
    /// <inheritdoc />
    public partial class ApplicationUseremailconfirmed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Ativo",
                table: "pessoas");

            migrationBuilder.DropColumn(
                name: "Papel",
                table: "pessoas");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Ativo",
                table: "pessoas",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Papel",
                table: "pessoas",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }
    }
}
