using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace ImproveU_backend.Migrations.NegocioContext
{
    /// <inheritdoc />
    public partial class CriaçãoTabelas : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "exercicios",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn),
                    nome = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    data_criacao = table.Column<DateTime>(type: "TIMESTAMP", nullable: false, defaultValueSql: "now()"),
                    ultima_atualizacao = table.Column<DateTime>(type: "TIMESTAMP", rowVersion: true, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_exercicios", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "pessoas",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn),
                    cpf = table.Column<string>(type: "varchar(11)", nullable: false),
                    nome = table.Column<string>(type: "varchar(255)", nullable: false),
                    identity_user_id = table.Column<string>(type: "varchar(255)", nullable: false),
                    data_criacao = table.Column<DateTime>(type: "TIMESTAMP", nullable: false, defaultValueSql: "now()"),
                    ultima_atualizacao = table.Column<DateTime>(type: "TIMESTAMP", rowVersion: true, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_pessoas", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "alunos",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn),
                    pessoa_id = table.Column<int>(type: "int", nullable: false),
                    treino_id = table.Column<int>(type: "int", nullable: true),
                    data_criacao = table.Column<DateTime>(type: "TIMESTAMP", nullable: false, defaultValueSql: "now()"),
                    ultima_atualizacao = table.Column<DateTime>(type: "TIMESTAMP", rowVersion: true, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_alunos", x => x.id);
                    table.ForeignKey(
                        name: "FK_alunos_pessoas_pessoa_id",
                        column: x => x.pessoa_id,
                        principalTable: "pessoas",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ed_fisicos",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn),
                    registro_conselho = table.Column<string>(type: "varchar(20)", nullable: true),
                    pessoa_id = table.Column<int>(type: "int", nullable: false),
                    treino_id = table.Column<int>(type: "int", nullable: true),
                    data_criacao = table.Column<DateTime>(type: "TIMESTAMP", nullable: false, defaultValueSql: "now()"),
                    ultima_atualizacao = table.Column<DateTime>(type: "TIMESTAMP", rowVersion: true, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ed_fisicos", x => x.id);
                    table.ForeignKey(
                        name: "FK_ed_fisicos_pessoas_pessoa_id",
                        column: x => x.pessoa_id,
                        principalTable: "pessoas",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "fotos",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn),
                    path = table.Column<string>(type: "varchar(100)", nullable: false),
                    extensão = table.Column<string>(type: "varchar(5)", nullable: false),
                    pessoa_id = table.Column<int>(type: "int", nullable: false),
                    data_criacao = table.Column<DateTime>(type: "TIMESTAMP", nullable: false, defaultValueSql: "now()"),
                    ultima_atualizacao = table.Column<DateTime>(type: "TIMESTAMP", rowVersion: true, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_fotos", x => x.id);
                    table.ForeignKey(
                        name: "FK_fotos_pessoas_pessoa_id",
                        column: x => x.pessoa_id,
                        principalTable: "pessoas",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
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
                    data_inicio_vigencia = table.Column<DateOnly>(type: "DATE", nullable: true, defaultValueSql: "now()"),
                    data_fim_vigencia = table.Column<DateOnly>(type: "DATE", nullable: true),
                    data_criacao = table.Column<DateTime>(type: "timestamp", nullable: false, defaultValueSql: "now()"),
                    ultima_atualizacao = table.Column<DateTime>(type: "timestamp", rowVersion: true, nullable: true)
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
                name: "itens_treino_realizado",
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
                    status = table.Column<bool>(type: "boolean", nullable: false),
                    feedback_id = table.Column<int>(type: "int", nullable: true),
                    data_criacao = table.Column<DateTime>(type: "TIMESTAMP", nullable: false, defaultValueSql: "now()"),
                    ultima_atualizacao = table.Column<DateTime>(type: "TIMESTAMP", rowVersion: true, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_itens_treino_realizado", x => x.id);
                    table.ForeignKey(
                        name: "FK_itens_treino_realizado_exercicios_exercicio_id",
                        column: x => x.exercicio_id,
                        principalTable: "exercicios",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_itens_treino_realizado_treinos_treino_id",
                        column: x => x.treino_id,
                        principalTable: "treinos",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "itens_treino_realizar",
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
                    data_criacao = table.Column<DateTime>(type: "TIMESTAMP", nullable: false, defaultValueSql: "now()"),
                    ultima_atualizacao = table.Column<DateTime>(type: "TIMESTAMP", rowVersion: true, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_itens_treino_realizar", x => x.id);
                    table.ForeignKey(
                        name: "FK_itens_treino_realizar_exercicios_exercicio_id",
                        column: x => x.exercicio_id,
                        principalTable: "exercicios",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_itens_treino_realizar_treinos_treino_id",
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
                        name: "FK_feedbacks_itens_treino_realizado_ItemTreinoId",
                        column: x => x.ItemTreinoId,
                        principalTable: "itens_treino_realizado",
                        principalColumn: "id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_alunos_pessoa_id",
                table: "alunos",
                column: "pessoa_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ed_fisicos_pessoa_id",
                table: "ed_fisicos",
                column: "pessoa_id",
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
                name: "IX_fotos_pessoa_id",
                table: "fotos",
                column: "pessoa_id");

            migrationBuilder.CreateIndex(
                name: "IX_itens_treino_realizado_exercicio_id",
                table: "itens_treino_realizado",
                column: "exercicio_id");

            migrationBuilder.CreateIndex(
                name: "IX_itens_treino_realizado_treino_id",
                table: "itens_treino_realizado",
                column: "treino_id");

            migrationBuilder.CreateIndex(
                name: "IX_itens_treino_realizar_exercicio_id",
                table: "itens_treino_realizar",
                column: "exercicio_id");

            migrationBuilder.CreateIndex(
                name: "IX_itens_treino_realizar_treino_id",
                table: "itens_treino_realizar",
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
                name: "fotos");

            migrationBuilder.DropTable(
                name: "itens_treino_realizar");

            migrationBuilder.DropTable(
                name: "itens_treino_realizado");

            migrationBuilder.DropTable(
                name: "exercicios");

            migrationBuilder.DropTable(
                name: "treinos");

            migrationBuilder.DropTable(
                name: "alunos");

            migrationBuilder.DropTable(
                name: "ed_fisicos");

            migrationBuilder.DropTable(
                name: "pessoas");
        }
    }
}
