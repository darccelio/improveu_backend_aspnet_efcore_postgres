namespace ImproveU_backend.Models;

public class Exercicio : Base
{
    public int Id { get; set; }
    public string Nome { get; set; }    

    public int ItemTreinoId { get; set; }
    public virtual ItemTreino ItemTreino { get; set; }
}