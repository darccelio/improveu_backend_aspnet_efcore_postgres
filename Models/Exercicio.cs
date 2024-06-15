namespace ImproveU_backend.Models;

public class Exercicio : Base
{
    public int Id { get; set; }
    public string Nome { get; set; }

    public virtual ICollection<ItemTreinoARealizar>? ItensTreino { get; set; } = null;

    
}