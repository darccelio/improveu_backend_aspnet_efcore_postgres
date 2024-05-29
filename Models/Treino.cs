namespace ImproveU_backend.Models;

public class Treino
{
    public int Id { get; set; }
    public int EdFisicoId { get; set; }
    public EdFisico EdFisico { get; set; }
    public int AlunoId { get; set; }
    public Aluno Aluno { get; set; }

    public DateTime DataInicioVigencia { get; set; }
    public DateTime DataFimVigencia { get; set; }

    public List<ItemTreino> ItensTreino { get; set; }

}
