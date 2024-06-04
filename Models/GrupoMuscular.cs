namespace ImproveU_backend.Models;

public class GrupoMuscular : Base
{
    public int Id { get; set; }
    public string Nome { get; set; }

    public virtual List<Exercicio> Exercicios{ get; set; }

}
