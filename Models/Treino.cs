namespace ImproveU_backend.Models;

public class Treino : Base
{
    public int Id { get; set; }
    public int EdFisicoId { get; set; }
    public virtual EdFisico EdFisico { get; set; }
    public int AlunoId { get; set; }
    public virtual Aluno Aluno { get; set; }

    public int Status  { get; set; } = 1;

    public DateOnly? DataInicioVigencia { get; set; }
    public DateOnly? DataFimVigencia { get; set; }

    public virtual ICollection<ItemTreinoARealizar> ItensTreinoARealizar { get; set; }
    public virtual ICollection<ItemTreinoRealizados> ItensTreinoRealizados { get; set; }
}
