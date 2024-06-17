using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace ImproveU_backend.DatabaseConfiguration.Migrations.Security
{
    /// <inheritdoc />
    public partial class IdentityConfigs : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ApplicationRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApplicationRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    UserName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "boolean", nullable: false),
                    PasswordHash = table.Column<string>(type: "text", nullable: true),
                    SecurityStamp = table.Column<string>(type: "text", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "text", nullable: true),
                    PhoneNumber = table.Column<string>(type: "text", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "boolean", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "boolean", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "boolean", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            //migrationBuilder.CreateTable(
            //    name: "Exercicio",
            //    columns: table => new
            //    {
            //        Id = table.Column<int>(type: "integer", nullable: false)
            //            .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
            //        Nome = table.Column<string>(type: "text", nullable: false),
            //        DataCriacao = table.Column<DateTime>(type: "timestamp with time zone", rowVersion: true, nullable: false),
            //        UltimaAlteracao = table.Column<DateTime>(type: "timestamp with time zone", rowVersion: true, nullable: true)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_Exercicio", x => x.Id);
            //    });


                

            migrationBuilder.CreateTable(
                name: "ApplicationRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    RoleId = table.Column<string>(type: "text", nullable: false),
                    ClaimType = table.Column<string>(type: "text", nullable: true),
                    ClaimValue = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApplicationRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ApplicationRoleClaims_ApplicationRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "ApplicationRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            //migrationBuilder.CreateTable(
            //    name: "ApplicationUser",
            //    columns: table => new
            //    {
            //        Id = table.Column<string>(type: "text", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_ApplicationUser", x => x.Id);
            //        table.ForeignKey(
            //            name: "FK_ApplicationUser_AspNetUsers_Id",
            //            column: x => x.Id,
            //            principalTable: "AspNetUsers",
            //            principalColumn: "Id",
            //            onDelete: ReferentialAction.Cascade);
            //    });

            

            migrationBuilder.CreateTable(
                name: "ApplicationUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<string>(type: "text", nullable: false),
                    ClaimType = table.Column<string>(type: "text", nullable: true),
                    ClaimValue = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApplicationUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ApplicationUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ApplicationUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "text", nullable: false),
                    ProviderKey = table.Column<string>(type: "text", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "text", nullable: true),
                    UserId = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApplicationUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_ApplicationUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ApplicationUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "text", nullable: false),
                    RoleId = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApplicationUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_ApplicationUserRoles_ApplicationRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "ApplicationRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ApplicationUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ApplicationUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "text", nullable: false),
                    LoginProvider = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Value = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApplicationUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_ApplicationUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            

            //migrationBuilder.CreateTable(
            //    name: "Aluno",
            //    columns: table => new
            //    {
            //        Id = table.Column<int>(type: "integer", nullable: false)
            //            .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
            //        PessoaId = table.Column<int>(type: "int", nullable: false),
            //        TreinoId = table.Column<int>(type: "integer", nullable: true),
            //        DataCriacao = table.Column<DateTime>(type: "timestamp with time zone", rowVersion: true, nullable: false),
            //        UltimaAlteracao = table.Column<DateTime>(type: "timestamp with time zone", rowVersion: true, nullable: true)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_Aluno", x => x.Id);
            //        table.ForeignKey(
            //            name: "FK_Aluno_pessoas_PessoaId",
            //            column: x => x.PessoaId,
            //            principalTable: "pessoas",
            //            principalColumn: "id",
            //            onDelete: ReferentialAction.Cascade);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "EdFisico",
            //    columns: table => new
            //    {
            //        Id = table.Column<int>(type: "integer", nullable: false)
            //            .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
            //        RegistroConselho = table.Column<string>(type: "text", nullable: true),
            //        PessoaId = table.Column<int>(type: "int", nullable: false),
            //        TreinoId = table.Column<int>(type: "integer", nullable: true),
            //        DataCriacao = table.Column<DateTime>(type: "timestamp with time zone", rowVersion: true, nullable: false),
            //        UltimaAlteracao = table.Column<DateTime>(type: "timestamp with time zone", rowVersion: true, nullable: true)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_EdFisico", x => x.Id);
            //        table.ForeignKey(
            //            name: "FK_EdFisico_pessoas_PessoaId",
            //            column: x => x.PessoaId,
            //            principalTable: "pessoas",
            //            principalColumn: "id",
            //            onDelete: ReferentialAction.Cascade);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "Foto",
            //    columns: table => new
            //    {
            //        Id = table.Column<int>(type: "integer", nullable: false)
            //            .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
            //        Path = table.Column<string>(type: "text", nullable: false),
            //        Extensão = table.Column<string>(type: "text", nullable: false),
            //        PessoaId = table.Column<int>(type: "int", nullable: false),
            //        DataCriacao = table.Column<DateTime>(type: "timestamp with time zone", rowVersion: true, nullable: false),
            //        UltimaAlteracao = table.Column<DateTime>(type: "timestamp with time zone", rowVersion: true, nullable: true)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_Foto", x => x.Id);
            //        table.ForeignKey(
            //            name: "FK_Foto_pessoas_PessoaId",
            //            column: x => x.PessoaId,
            //            principalTable: "pessoas",
            //            principalColumn: "id",
            //            onDelete: ReferentialAction.Cascade);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "Feedback",
            //    columns: table => new
            //    {
            //        Id = table.Column<int>(type: "integer", nullable: false)
            //            .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
            //        Mensagem = table.Column<string>(type: "text", nullable: false),
            //        AlunoId = table.Column<int>(type: "integer", nullable: false),
            //        EdFisicoId = table.Column<int>(type: "integer", nullable: false),
            //        ItemTreinoId = table.Column<int>(type: "integer", nullable: true),
            //        Direcao = table.Column<int>(type: "integer", nullable: false),
            //        DataCriacao = table.Column<DateTime>(type: "timestamp with time zone", rowVersion: true, nullable: false),
            //        UltimaAlteracao = table.Column<DateTime>(type: "timestamp with time zone", rowVersion: true, nullable: true)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_Feedback", x => x.Id);
            //        table.ForeignKey(
            //            name: "FK_Feedback_Aluno_AlunoId",
            //            column: x => x.AlunoId,
            //            principalTable: "Aluno",
            //            principalColumn: "Id",
            //            onDelete: ReferentialAction.Cascade);
            //        table.ForeignKey(
            //            name: "FK_Feedback_EdFisico_EdFisicoId",
            //            column: x => x.EdFisicoId,
            //            principalTable: "EdFisico",
            //            principalColumn: "Id",
            //            onDelete: ReferentialAction.Cascade);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "Treino",
            //    columns: table => new
            //    {
            //        Id = table.Column<int>(type: "integer", nullable: false)
            //            .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
            //        EdFisicoId = table.Column<int>(type: "integer", nullable: false),
            //        AlunoId = table.Column<int>(type: "integer", nullable: false),
            //        Status = table.Column<int>(type: "integer", nullable: false),
            //        DataInicioVigencia = table.Column<DateOnly>(type: "date", nullable: true),
            //        DataFimVigencia = table.Column<DateOnly>(type: "date", nullable: true),
            //        DataCriacao = table.Column<DateTime>(type: "timestamp with time zone", rowVersion: true, nullable: false),
            //        UltimaAlteracao = table.Column<DateTime>(type: "timestamp with time zone", rowVersion: true, nullable: true)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_Treino", x => x.Id);
            //        table.ForeignKey(
            //            name: "FK_Treino_Aluno_AlunoId",
            //            column: x => x.AlunoId,
            //            principalTable: "Aluno",
            //            principalColumn: "Id",
            //            onDelete: ReferentialAction.Cascade);
            //        table.ForeignKey(
            //            name: "FK_Treino_EdFisico_EdFisicoId",
            //            column: x => x.EdFisicoId,
            //            principalTable: "EdFisico",
            //            principalColumn: "Id",
            //            onDelete: ReferentialAction.Cascade);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "ItemTreinoARealizar",
            //    columns: table => new
            //    {
            //        Id = table.Column<int>(type: "integer", nullable: false)
            //            .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
            //        CargaEmKg = table.Column<int>(type: "integer", nullable: true),
            //        Repeticoes = table.Column<int>(type: "integer", nullable: true),
            //        Series = table.Column<int>(type: "integer", nullable: true),
            //        IntervaloDescanso = table.Column<int>(type: "integer", nullable: true),
            //        ExercicioId = table.Column<int>(type: "integer", nullable: false),
            //        TreinoId = table.Column<int>(type: "integer", nullable: false),
            //        DataCriacao = table.Column<DateTime>(type: "timestamp with time zone", rowVersion: true, nullable: false),
            //        UltimaAlteracao = table.Column<DateTime>(type: "timestamp with time zone", rowVersion: true, nullable: true)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_ItemTreinoARealizar", x => x.Id);
            //        table.ForeignKey(
            //            name: "FK_ItemTreinoARealizar_Exercicio_ExercicioId",
            //            column: x => x.ExercicioId,
            //            principalTable: "Exercicio",
            //            principalColumn: "Id",
            //            onDelete: ReferentialAction.Cascade);
            //        table.ForeignKey(
            //            name: "FK_ItemTreinoARealizar_Treino_TreinoId",
            //            column: x => x.TreinoId,
            //            principalTable: "Treino",
            //            principalColumn: "Id",
            //            onDelete: ReferentialAction.Cascade);
            //    });

            //migrationBuilder.CreateTable(
            //    name: "ItemTreinoRealizados",
            //    columns: table => new
            //    {
            //        Id = table.Column<int>(type: "integer", nullable: false)
            //            .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
            //        CargaEmKg = table.Column<int>(type: "integer", nullable: true),
            //        Repeticoes = table.Column<int>(type: "integer", nullable: true),
            //        Series = table.Column<int>(type: "integer", nullable: true),
            //        IntervaloDescanso = table.Column<int>(type: "integer", nullable: true),
            //        ExercicioId = table.Column<int>(type: "integer", nullable: false),
            //        TreinoId = table.Column<int>(type: "integer", nullable: false),
            //        status = table.Column<bool>(type: "boolean", nullable: false),
            //        FeedbackId = table.Column<int>(type: "integer", nullable: true),
            //        DataCriacao = table.Column<DateTime>(type: "timestamp with time zone", rowVersion: true, nullable: false),
            //        UltimaAlteracao = table.Column<DateTime>(type: "timestamp with time zone", rowVersion: true, nullable: true)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_ItemTreinoRealizados", x => x.Id);
            //        table.ForeignKey(
            //            name: "FK_ItemTreinoRealizados_Exercicio_ExercicioId",
            //            column: x => x.ExercicioId,
            //            principalTable: "Exercicio",
            //            principalColumn: "Id",
            //            onDelete: ReferentialAction.Cascade);
            //        table.ForeignKey(
            //            name: "FK_ItemTreinoRealizados_Feedback_FeedbackId",
            //            column: x => x.FeedbackId,
            //            principalTable: "Feedback",
            //            principalColumn: "Id");
            //        table.ForeignKey(
            //            name: "FK_ItemTreinoRealizados_Treino_TreinoId",
            //            column: x => x.TreinoId,
            //            principalTable: "Treino",
            //            principalColumn: "Id",
            //            onDelete: ReferentialAction.Cascade);
            //    });

            migrationBuilder.AddColumn<string>(
            name: "AspNetUserId",
            table: "ApplicationUser",
            type: "varchar(255)",
            nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ApplicationUser_AspNetUserId",
                table: "ApplicationUser",
                column: "AspNetUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_ApplicationUser_AspNetUsers_AspNetUserId",
                table: "ApplicationUser",
                column: "AspNetUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            //migrationBuilder.AddColumn<string>(
            //name: "identity_user_id",
            //table: "pessoas",
            //type: "varchar(255)",
            //nullable: false);

            //migrationBuilder.CreateIndex(
            //    name: "IX_identity_user_id",
            //    table: "pessoas",
            //    column: "identity_user_id");

            //migrationBuilder.AddForeignKey(
            //    name: "FK_pessoas_ApplicationUser_identity_user_id",
            //    table: "pessoas",
            //    column: "identity_user_id",
            //    principalTable: "ApplicationUser",
            //    principalColumn: "Id",
            //    onDelete: ReferentialAction.Cascade);           


            //migrationBuilder.CreateIndex(
            //    name: "IX_Aluno_PessoaId",
            //    table: "Aluno",
            //    column: "PessoaId",
            //    unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ApplicationRoleClaims_RoleId",
                table: "ApplicationRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "ApplicationRoles",
                column: "NormalizedName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ApplicationUserClaims_UserId",
                table: "ApplicationUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_ApplicationUserLogins_UserId",
                table: "ApplicationUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_ApplicationUserRoles_RoleId",
                table: "ApplicationUserRoles",
                column: "RoleId");

            //migrationBuilder.CreateIndex(
            //    name: "EmailIndex",
            //    table: "AspNetUsers",
            //    column: "NormalizedEmail");

            //migrationBuilder.CreateIndex(
            //    name: "UserNameIndex",
            //    table: "AspNetUsers",
            //    column: "NormalizedUserName",
            //    unique: true);

            //migrationBuilder.CreateIndex(
            //    name: "IX_EdFisico_PessoaId",
            //    table: "EdFisico",
            //    column: "PessoaId",
            //    unique: true);

            //migrationBuilder.CreateIndex(
            //    name: "IX_Feedback_AlunoId",
            //    table: "Feedback",
            //    column: "AlunoId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_Feedback_EdFisicoId",
            //    table: "Feedback",
            //    column: "EdFisicoId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_Foto_PessoaId",
            //    table: "Foto",
            //    column: "PessoaId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_ItemTreinoARealizar_ExercicioId",
            //    table: "ItemTreinoARealizar",
            //    column: "ExercicioId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_ItemTreinoARealizar_TreinoId",
            //    table: "ItemTreinoARealizar",
            //    column: "TreinoId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_ItemTreinoRealizados_ExercicioId",
            //    table: "ItemTreinoRealizados",
            //    column: "ExercicioId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_ItemTreinoRealizados_FeedbackId",
            //    table: "ItemTreinoRealizados",
            //    column: "FeedbackId",
            //    unique: true);

            //migrationBuilder.CreateIndex(
            //    name: "IX_ItemTreinoRealizados_TreinoId",
            //    table: "ItemTreinoRealizados",
            //    column: "TreinoId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_pessoas_identity_user_id",
            //    table: "pessoas",
            //    column: "identity_user_id",
            //    unique: true);

            //migrationBuilder.CreateIndex(
            //    name: "IX_Treino_AlunoId",
            //    table: "Treino",
            //    column: "AlunoId");

            //migrationBuilder.CreateIndex(
            //    name: "IX_Treino_EdFisicoId",
            //    table: "Treino",
            //    column: "EdFisicoId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ApplicationRoleClaims");

            migrationBuilder.DropTable(
                name: "ApplicationUserClaims");

            migrationBuilder.DropTable(
                name: "ApplicationUserLogins");

            migrationBuilder.DropTable(
                name: "ApplicationUserRoles");

            migrationBuilder.DropTable(
                name: "ApplicationUserTokens");

            //migrationBuilder.DropTable(
            //    name: "Foto");

            //migrationBuilder.DropTable(
            //    name: "ItemTreinoARealizar");

            //migrationBuilder.DropTable(
            //    name: "ItemTreinoRealizados");

            migrationBuilder.DropTable(
                name: "ApplicationRoles");

            //migrationBuilder.DropTable(
            //    name: "Exercicio");

            //migrationBuilder.DropTable(
            //    name: "Feedback");

            //migrationBuilder.DropTable(
            //    name: "Treino");

            //migrationBuilder.DropTable(
            //    name: "Aluno");

            //migrationBuilder.DropTable(
            //    name: "EdFisico");

            //migrationBuilder.DropTable(
            //    name: "pessoas");

            //migrationBuilder.DropTable(
            //    name: "ApplicationUser");

            migrationBuilder.DropTable(
                name: "AspNetUsers");
        }
    }
}
