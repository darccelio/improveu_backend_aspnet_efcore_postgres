using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ImproveU_backend.Migrations
{
    /// <inheritdoc />
    public partial class InsertListaExercicios : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
            table: "exercicios",
            columns: new[] { "nome" },
            values: new object[,]
            {
                { "Aberturas" },
                { "Aberturas Inclinadas" },
                { "Aberturas Cabos" },
                { "Aberturas Cabos Declinado" },
                { "Peck-Deck" },
                { "Press Peito Halter" },
                { "Press Peito Inclinado" },
                { "Press Peito Inclinado Maquina (Chest Press)" },
                { "Supino Plano" },
                { "Supino Inclinado" },
                { "Supino Declinado" },
                { "Press de peito inclinado unilateral" },
                { "Pullover com halter" },
                { "Fundos com peso" },
                { "Elevações" },
                { "Elevações Pega Curta" },
                { "Remada T" },
                { "Remada com Barra" },
                { "Remada com Halter" },
                { "Remada Alta Maquina" },
                { "Remada Sentada Maquina" },
                { "Peso-Morto" },
                { "Pulldown Maquina" },
                { "Peso morto com halteres" },
                { "Remada alta na polia alternada" },
                { "Hiperextensões" },
                { "Elevações com peso e pega curta" },
                { "Elevações com pega larga" },
                { "Agachamento" },
                { "Prensa" },
                { "Lunges com halteres" },
                { "Leg Extension" },
                { "Leg Curl" },
                { "Agachamento com halteres" },
                { "Peso morto com halteres e pernas semi esticadas" },
                { "Step Up com halteres" },
                { "Peso morto sumo" },
                { "Ponte para glúteos" },
                { "Peso morto com trap bar" },
                { "Lunges com barra" },
                { "Peso morto romeno com barra" },
                { "Agachamento com uma perna" },
                { "Hip Thrust" },
                { "Agachamento búlgaro" },
                { "Peso morto romeno com halteres" },
                { "Farmer's Walk" },
                { "Agachamento com um halter" },
                { "Abdutores em máquina" },
                { "Agachamento frontal" },
                { "Agachamento Hack" },
                { "Arnold Press" },
                { "Frontais" },
                { "Laterais" },
                { "Press Militar" },
                { "Press Ombro Halter" },
                { "Remada Alta" },
                { "Aberturas invertidas" },
                { "Laterais com cabo unilateral" },
                { "Puxada com cabos inclinado" },
                { "Extensão lateral com halter" },
                { "Curl de bícep com press de ombros" },
                { "Press de ombros unilateral" },
                { "Puxada frontal" },
                { "Bicep Halter Concentrado" },
                { "Bicep Concentrado Banco" },
                { "Bicep Cabos" },
                { "Curl Bicep Barra" },
                { "Curl Bicep Halter" }
               }
               );

        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
            name: "Exercicios",
            schema: "public");
        }
    }
}
