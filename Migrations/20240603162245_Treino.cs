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
                name: "TreinoId",
                table: "ed_fisicos",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TreinoId",
                table: "alunos",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "treinos",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn),
                    ed_fisico_id = table.Column<int>(type: "int", nullable: false),
                    aluno_id = table.Column<int>(type: "int", nullable: false),
                    data_inicio_vigencia = table.Column<DateTime>(type: "TIMESTAMP", nullable: false),
                    data_fim_vigencia = table.Column<DateTime>(type: "TIMESTAMP", nullable: false),
                    status = table.Column<int>(type: "int", nullable: false, defaultValue: 1),
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
                name: "grupos_treino",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn),
                    nome = table.Column<string>(type: "varchar(255)", nullable: false),
                    treino_id = table.Column<int>(type: "int", nullable: false),
                    data_criacao = table.Column<DateTime>(type: "TIMESTAMP", nullable: false, defaultValueSql: "now()"),
                    ultima_atualizacao = table.Column<DateTime>(type: "TIMESTAMP", rowVersion: true, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_grupos_treino", x => x.id);
                    table.ForeignKey(
                        name: "FK_grupos_treino_treinos_treino_id",
                        column: x => x.treino_id,
                        principalTable: "treinos",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "itens_treino",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn),
                    carga = table.Column<int>(type: "int", nullable: false),
                    repeticoes = table.Column<int>(type: "int", nullable: false),
                    series = table.Column<int>(type: "int", nullable: false),
                    exercicio_id = table.Column<int>(type: "int", nullable: false),
                    grupo_treino_id = table.Column<int>(type: "int", nullable: false),
                    feedback_id = table.Column<int>(type: "int", nullable: true),
                    data_criacao = table.Column<DateTime>(type: "TIMESTAMP", nullable: false, defaultValueSql: "now()"),
                    ultima_atualizacao = table.Column<DateTime>(type: "TIMESTAMP", rowVersion: true, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_itens_treino", x => x.id);
                    table.ForeignKey(
                        name: "FK_itens_treino_grupos_treino_grupo_treino_id",
                        column: x => x.grupo_treino_id,
                        principalTable: "grupos_treino",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "exercicios",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn),
                    Nome = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    item_treino_id = table.Column<int>(type: "int", nullable: false),
                    data_criacao = table.Column<DateTime>(type: "TIMESTAMP", nullable: false, defaultValueSql: "now()"),
                    ultima_atualizacao = table.Column<DateTime>(type: "TIMESTAMP", rowVersion: true, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_exercicios", x => x.id);
                    table.ForeignKey(
                        name: "FK_exercicios_itens_treino_item_treino_id",
                        column: x => x.item_treino_id,
                        principalTable: "itens_treino",
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
                name: "IX_exercicios_item_treino_id",
                table: "exercicios",
                column: "item_treino_id",
                unique: true);

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
                name: "IX_grupos_treino_treino_id",
                table: "grupos_treino",
                column: "treino_id");

            migrationBuilder.CreateIndex(
                name: "IX_itens_treino_grupo_treino_id",
                table: "itens_treino",
                column: "grupo_treino_id");

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
                name: "exercicios");

            migrationBuilder.DropTable(
                name: "feedbacks");

            migrationBuilder.DropTable(
                name: "itens_treino");

            migrationBuilder.DropTable(
                name: "grupos_treino");

            migrationBuilder.DropTable(
                name: "treinos");

            migrationBuilder.DropColumn(
                name: "TreinoId",
                table: "ed_fisicos");

            migrationBuilder.DropColumn(
                name: "TreinoId",
                table: "alunos");
        }
    }
}
