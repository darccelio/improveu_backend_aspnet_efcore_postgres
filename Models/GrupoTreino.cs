namespace ImproveU_backend.Models;

public class GrupoTreino : Base
{
    public int Id { get; set; }
    public string Nome { get; set; }

    public virtual List<ItemTreino> ItensTreino { get; set; }

    public int TreinoId { get; set; }
    public virtual Treino Treino { get; set; }

}