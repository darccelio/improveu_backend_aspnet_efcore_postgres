using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ImproveU_backend.DatabaseConfiguration.Migrations.Security
{
    /// <inheritdoc />
    public partial class PapelUsuario : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ativo",
                table: "pessoas",
                newName: "Ativo");

            migrationBuilder.RenameColumn(
                name: "tipo_pessoa",
                table: "pessoas",
                newName: "Papel");

            migrationBuilder.AlterColumn<int>(
                name: "Ativo",
                table: "pessoas",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "Papel",
                table: "pessoas",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "Ativo",
                table: "ApplicationUser",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Papel",
                table: "ApplicationUser",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Ativo",
                table: "ApplicationUser");

            migrationBuilder.DropColumn(
                name: "Papel",
                table: "ApplicationUser");

            migrationBuilder.RenameColumn(
                name: "Ativo",
                table: "pessoas",
                newName: "ativo");

            migrationBuilder.RenameColumn(
                name: "Papel",
                table: "pessoas",
                newName: "tipo_pessoa");

            migrationBuilder.AlterColumn<int>(
                name: "ativo",
                table: "pessoas",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "tipo_pessoa",
                table: "pessoas",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");
        }
    }
}
