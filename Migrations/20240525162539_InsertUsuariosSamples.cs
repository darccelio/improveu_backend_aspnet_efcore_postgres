using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ImproveU_backend.Migrations
{
    /// <inheritdoc />
    public partial class InsertUsuariosSamples : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
               table: "usuarios",
               columns: new[] { "email", "tipo_pessoa", "senha", "ativo" },
               values: new object[,]
               {
                    { "uEdFisicoTeste1@email.com", 0, "123456", 1 },
                    { "uEdFisicoTeste2@email.com", 0, "123456", 1 },
                    { "uEdFisicoTeste3@email.com", 0, "123456", 1 },
                    { "uEdFisicoTeste4@email.com", 0, "123456", 1 },
                    { "uEdFisicoTeste5@email.com", 0, "123456", 1 },
                    { "uAlunoTeste1@email.com", 1, "123456", 1 },
                    { "uAlunoTeste2@email.com", 1, "123456", 1 },
                    { "uAlunoTeste3@email.com", 1, "123456", 1 },
                    { "uAlunoTeste4@email.com", 1, "123456", 1 },
                    { "uAlunoTeste5@email.com", 1, "123456", 1 },
               }
               );

        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.Sql("DELETE FROM Usuarios");

            migrationBuilder.DeleteData(
                table: "usuarios",
                keyColumn: "email",
                keyValue: "uEdFisicoTeste1@email.com"
            );
            migrationBuilder.DeleteData(
                table: "usuarios",
                keyColumn: "email",
                keyValue: "uEdFisicoTeste2@email.com"
            );
            migrationBuilder.DeleteData(
                table: "usuarios",
                keyColumn: "email",
                keyValue: "uEdFisicoTeste3@email.com"
            );
            migrationBuilder.DeleteData(
                table: "usuarios",
                keyColumn: "email",
                keyValue: "uEdFisicoTeste4@email.com"
            );
            migrationBuilder.DeleteData(
                table: "usuarios",
                keyColumn: "email",
                keyValue: "uEdFisicoTeste5@email.com"
            );
            migrationBuilder.DeleteData(
                table: "usuarios",
                keyColumn: "email",
                keyValue: "uAlunoTeste1@email.com"
            );
            migrationBuilder.DeleteData(
                table: "usuarios",
                keyColumn: "email",
                keyValue: "uAlunoTeste2@email.com"
            );
            migrationBuilder.DeleteData(
                table: "usuarios",
                keyColumn: "email",
                keyValue: "uAlunoTeste3@email.com"
            );
            migrationBuilder.DeleteData(
                table: "usuarios",
                keyColumn: "email",
                keyValue: "uAlunoTeste4@email.com"
            );
            migrationBuilder.DeleteData(
                table: "usuarios",
                keyColumn: "email",
                keyValue: "uAlunoTeste5@email.com"
            );

        }
    }
}
