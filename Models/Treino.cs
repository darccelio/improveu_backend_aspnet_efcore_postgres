namespace ImproveU_backend.Models;

public class Treino : Base
{
    public int Id { get; set; }
    public int EdFisicoId { get; set; }
    public virtual EdFisico EdFisico { get; set; }
    public int AlunoId { get; set; }
    public virtual Aluno Aluno { get; set; }

    public int Status = 1;

    public DateTime DataInicioVigencia { get; set; }
    public DateTime DataFimVigencia { get; set; }

    public ICollection<GrupoTreino> GruposTreinos { get; set; }

}
