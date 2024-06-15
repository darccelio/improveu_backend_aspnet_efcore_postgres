using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ImproveU_backend.Migrations
{
    /// <inheritdoc />
    public partial class alteracaoexerciciocolumnnome : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Nome",
                table: "exercicios",
                newName: "nome");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "nome",
                table: "exercicios",
                newName: "Nome");
        }
    }
}
