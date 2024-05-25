using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ImproveU_backend.Migrations;

/// <inheritdoc />
public partial class InsertPessoasEdFisicosAlunosSamples : Migration
{
   
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.Sql("INSERT INTO pessoas (cpf, nome, usuario_id) SELECT '20100987818', 'João Luiz Gonçalves', id FROM usuarios WHERE email = 'uEdFisicoTeste1@email.com'");
        migrationBuilder.Sql("INSERT INTO pessoas (cpf, nome, usuario_id) SELECT '94216062563', 'Emanuel Emanuel Julio Teixeira', id FROM usuarios WHERE email = 'uEdFisicoTeste2@email.com'");
        migrationBuilder.Sql("INSERT INTO pessoas (cpf, nome, usuario_id) SELECT '97142091100', 'Sophia Silvana Raquel Brito', id FROM usuarios WHERE email = 'uEdFisicoTeste3@email.com'");
        migrationBuilder.Sql("INSERT INTO pessoas (cpf, nome, usuario_id) SELECT '88100488223', 'Mariana Silvana Natália Duarte', id FROM usuarios WHERE email = 'uEdFisicoTeste4@email.com'");
        migrationBuilder.Sql("INSERT INTO pessoas (cpf, nome, usuario_id) SELECT '24700976144', 'Rafaela Hadassa Rodrigues', id FROM usuarios WHERE email = 'uEdFisicoTeste5@email.com'");
        migrationBuilder.Sql("INSERT INTO pessoas (cpf, nome, usuario_id) SELECT '29135187883', 'Hadassa Benedita Sales', id FROM usuarios WHERE email = 'uAlunoTeste1@email.com'");
        migrationBuilder.Sql("INSERT INTO pessoas (cpf, nome, usuario_id) SELECT '80486288218', 'Carolina Aurora da Rosa', id FROM usuarios WHERE email = 'uAlunoTeste2@email.com'");
        migrationBuilder.Sql("INSERT INTO pessoas (cpf, nome, usuario_id) SELECT '93275612794', 'Bianca Clara Lavínia Castro', id FROM usuarios WHERE email = 'uAlunoTeste3@email.com'");
        migrationBuilder.Sql("INSERT INTO pessoas (cpf, nome, usuario_id) SELECT '46372333562', 'Vanessa Tatiane Mariana Assis', id FROM usuarios WHERE email = 'uAlunoTeste4@email.com'");
        migrationBuilder.Sql("INSERT INTO pessoas (cpf, nome, usuario_id) SELECT '15705693982', 'Matheus Mateus Melo', id FROM usuarios WHERE email = 'uAlunoTeste5@email.com'");

        migrationBuilder.Sql("INSERT INTO ed_fisicos (registro_conselho, pessoa_id) SELECT '359994301', id FROM pessoas WHERE cpf = '20100987818'");
        migrationBuilder.Sql("INSERT INTO ed_fisicos (registro_conselho, pessoa_id) SELECT '462633652', id FROM pessoas WHERE cpf = '94216062563'");
        migrationBuilder.Sql("INSERT INTO ed_fisicos (registro_conselho, pessoa_id) SELECT '290252581', id FROM pessoas WHERE cpf = '97142091100'");
        migrationBuilder.Sql("INSERT INTO ed_fisicos (registro_conselho, pessoa_id) SELECT '124034469', id FROM pessoas WHERE cpf = '88100488223'");
        migrationBuilder.Sql("INSERT INTO ed_fisicos (registro_conselho, pessoa_id) SELECT '284565301', id FROM pessoas WHERE cpf = '24700976144'");

        migrationBuilder.Sql("INSERT INTO alunos (pessoa_id) SELECT id FROM pessoas WHERE cpf = '29135187883'");
        migrationBuilder.Sql("INSERT INTO alunos (pessoa_id) SELECT id FROM pessoas WHERE cpf = '80486288218'");
        migrationBuilder.Sql("INSERT INTO alunos (pessoa_id) SELECT id FROM pessoas WHERE cpf = '93275612794'");
        migrationBuilder.Sql("INSERT INTO alunos (pessoa_id) SELECT id FROM pessoas WHERE cpf = '46372333562'");
        migrationBuilder.Sql("INSERT INTO alunos (pessoa_id) SELECT id FROM pessoas WHERE cpf = '15705693982'");
    }

    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.Sql("DELETE FROM alunos WHERE pessoa_id IN (SELECT id FROM pessoas WHERE cpf = '29135187883')");
        migrationBuilder.Sql("DELETE FROM alunos WHERE pessoa_id IN (SELECT id FROM pessoas WHERE cpf = '80486288218')");
        migrationBuilder.Sql("DELETE FROM alunos WHERE pessoa_id IN (SELECT id FROM pessoas WHERE cpf = '93275612794')");
        migrationBuilder.Sql("DELETE FROM alunos WHERE pessoa_id IN (SELECT id FROM pessoas WHERE cpf = '46372333562')");
        migrationBuilder.Sql("DELETE FROM alunos WHERE pessoa_id IN (SELECT id FROM pessoas WHERE cpf = '15705693982')");

        migrationBuilder.Sql("DELETE FROM ed_fisicos WHERE pessoa_id IN (SELECT id FROM pessoas WHERE cpf = '20100987818')");
        migrationBuilder.Sql("DELETE FROM ed_fisicos WHERE pessoa_id IN (SELECT id FROM pessoas WHERE cpf = '94216062563')");
        migrationBuilder.Sql("DELETE FROM ed_fisicos WHERE pessoa_id IN (SELECT id FROM pessoas WHERE cpf = '97142091100')");
        migrationBuilder.Sql("DELETE FROM ed_fisicos WHERE pessoa_id IN (SELECT id FROM pessoas WHERE cpf = '88100488223')");
        migrationBuilder.Sql("DELETE FROM ed_fisicos WHERE pessoa_id IN (SELECT id FROM pessoas WHERE cpf = '24700976144')");

        migrationBuilder.Sql("DELETE FROM pessoas WHERE cpf = '20100987818'");
        migrationBuilder.Sql("DELETE FROM pessoas WHERE cpf = '94216062563'");
        migrationBuilder.Sql("DELETE FROM pessoas WHERE cpf = '97142091100'");
        migrationBuilder.Sql("DELETE FROM pessoas WHERE cpf = '88100488223'");
        migrationBuilder.Sql("DELETE FROM pessoas WHERE cpf = '24700976144'");
        migrationBuilder.Sql("DELETE FROM pessoas WHERE cpf = '29135187883'");
        migrationBuilder.Sql("DELETE FROM pessoas WHERE cpf = '80486288218'");
        migrationBuilder.Sql("DELETE FROM pessoas WHERE cpf = '93275612794'");
        migrationBuilder.Sql("DELETE FROM pessoas WHERE cpf = '46372333562'");
        migrationBuilder.Sql("DELETE FROM pessoas WHERE cpf = '15705693982'");
    }
}