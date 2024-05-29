namespace ImproveU_backend.Models;

public class ExercicioTipo : Base
{
    public int Id { get; set; }
    public string Tipo { get; set; }

    public virtual List<Exercicio> Exercicios { get; set; }
}
