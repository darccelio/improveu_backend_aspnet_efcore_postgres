using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace ImproveU_backend.Migrations
{
    /// <inheritdoc />
    public partial class treinorealido : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_feedbacks_itens_treino_ItemTreinoId",
                table: "feedbacks");

            migrationBuilder.DropTable(
                name: "itens_treino");

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

            migrationBuilder.AddForeignKey(
                name: "FK_feedbacks_itens_treino_realizado_ItemTreinoId",
                table: "feedbacks",
                column: "ItemTreinoId",
                principalTable: "itens_treino_realizado",
                principalColumn: "id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_feedbacks_itens_treino_realizado_ItemTreinoId",
                table: "feedbacks");

            migrationBuilder.DropTable(
                name: "itens_treino_realizado");

            migrationBuilder.DropTable(
                name: "itens_treino_realizar");

            migrationBuilder.CreateTable(
                name: "itens_treino",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn),
                    exercicio_id = table.Column<int>(type: "int", nullable: false),
                    treino_id = table.Column<int>(type: "int", nullable: false),
                    carga = table.Column<int>(type: "int", nullable: true),
                    data_criacao = table.Column<DateTime>(type: "TIMESTAMP", nullable: false, defaultValueSql: "now()"),
                    feedback_id = table.Column<int>(type: "int", nullable: true),
                    intervalo_descanso = table.Column<int>(type: "int", nullable: true),
                    repeticoes = table.Column<int>(type: "int", nullable: true),
                    series = table.Column<int>(type: "int", nullable: true),
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

            migrationBuilder.CreateIndex(
                name: "IX_itens_treino_exercicio_id",
                table: "itens_treino",
                column: "exercicio_id");

            migrationBuilder.CreateIndex(
                name: "IX_itens_treino_treino_id",
                table: "itens_treino",
                column: "treino_id");

            migrationBuilder.AddForeignKey(
                name: "FK_feedbacks_itens_treino_ItemTreinoId",
                table: "feedbacks",
                column: "ItemTreinoId",
                principalTable: "itens_treino",
                principalColumn: "id");
        }
    }
}
