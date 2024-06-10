using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace ImproveU_backend.Migrations
{
    /// <inheritdoc />
    public partial class Treino : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "treino_id",
                table: "ed_fisicos",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "treino_id",
                table: "alunos",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "exercicios",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn),
                    Nome = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    data_criacao = table.Column<DateTime>(type: "TIMESTAMP", nullable: false, defaultValueSql: "now()"),
                    ultima_atualizacao = table.Column<DateTime>(type: "TIMESTAMP", rowVersion: true, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_exercicios", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "treinos",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn),
                    ed_fisico_id = table.Column<int>(type: "int", nullable: false),
                    aluno_id = table.Column<int>(type: "int", nullable: false),
                    status = table.Column<int>(type: "int", nullable: false, defaultValue: 1),
                    data_inicio_vigencia = table.Column<DateTime>(type: "TIMESTAMP", nullable: true),
                    data_fim_vigencia = table.Column<DateTime>(type: "TIMESTAMP", nullable: true),
                    data_criacao = table.Column<DateTime>(type: "TIMESTAMP", nullable: false, defaultValueSql: "now()"),
                    ultima_atualizacao = table.Column<DateTime>(type: "TIMESTAMP", rowVersion: true, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_treinos", x => x.id);
                    table.ForeignKey(
                        name: "FK_treinos_alunos_aluno_id",
                        column: x => x.aluno_id,
                        principalTable: "alunos",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_treinos_ed_fisicos_ed_fisico_id",
                        column: x => x.ed_fisico_id,
                        principalTable: "ed_fisicos",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "itens_treino",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn),
                    carga = table.Column<int>(type: "int", nullable: true),
                    repeticoes = table.Column<int>(type: "int", nullable: true),
                    series = table.Column<int>(type: "int", nullable: true),
                    intervalo_descanso = table.Column<int>(type: "int", nullable: true),
                    exercicio_id = table.Column<int>(type: "int", nullable: false),
                    treino_id = table.Column<int>(type: "int", nullable: false),
                    feedback_id = table.Column<int>(type: "int", nullable: true),
                    data_criacao = table.Column<DateTime>(type: "TIMESTAMP", nullable: false, defaultValueSql: "now()"),
                    ultima_atualizacao = table.Column<DateTime>(type: "TIMESTAMP", rowVersion: true, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_itens_treino", x => x.id);
                    table.ForeignKey(
                        name: "FK_itens_treino_exercicios_exercicio_id",
                        column: x => x.exercicio_id,
                        principalTable: "exercicios",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_itens_treino_treinos_treino_id",
                        column: x => x.treino_id,
                        principalTable: "treinos",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "feedbacks",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn),
                    mensagem = table.Column<string>(type: "varchar(255)", nullable: false),
                    aluno_id = table.Column<int>(type: "int", nullable: false),
                    ed_fisico_id = table.Column<int>(type: "int", nullable: false),
                    ItemTreinoId = table.Column<int>(type: "int", nullable: true),
                    direcao = table.Column<int>(type: "int", nullable: false),
                    data_criacao = table.Column<DateTime>(type: "TIMESTAMP", nullable: false, defaultValueSql: "now()"),
                    ultima_atualizacao = table.Column<DateTime>(type: "TIMESTAMP", rowVersion: true, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_feedbacks", x => x.id);
                    table.ForeignKey(
                        name: "FK_feedbacks_alunos_aluno_id",
                        column: x => x.aluno_id,
                        principalTable: "alunos",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_feedbacks_ed_fisicos_ed_fisico_id",
                        column: x => x.ed_fisico_id,
                        principalTable: "ed_fisicos",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_feedbacks_itens_treino_ItemTreinoId",
                        column: x => x.ItemTreinoId,
                        principalTable: "itens_treino",
                        principalColumn: "id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_feedbacks_aluno_id",
                table: "feedbacks",
                column: "aluno_id");

            migrationBuilder.CreateIndex(
                name: "IX_feedbacks_ed_fisico_id",
                table: "feedbacks",
                column: "ed_fisico_id");

            migrationBuilder.CreateIndex(
                name: "IX_feedbacks_ItemTreinoId",
                table: "feedbacks",
                column: "ItemTreinoId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_itens_treino_exercicio_id",
                table: "itens_treino",
                column: "exercicio_id");

            migrationBuilder.CreateIndex(
                name: "IX_itens_treino_treino_id",
                table: "itens_treino",
                column: "treino_id");

            migrationBuilder.CreateIndex(
                name: "IX_treinos_aluno_id",
                table: "treinos",
                column: "aluno_id");

            migrationBuilder.CreateIndex(
                name: "IX_treinos_ed_fisico_id",
                table: "treinos",
                column: "ed_fisico_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "feedbacks");

            migrationBuilder.DropTable(
                name: "itens_treino");

            migrationBuilder.DropTable(
                name: "exercicios");

            migrationBuilder.DropTable(
                name: "treinos");

            migrationBuilder.DropColumn(
                name: "treino_id",
                table: "ed_fisicos");

            migrationBuilder.DropColumn(
                name: "treino_id",
                table: "alunos");
        }
    }
}
