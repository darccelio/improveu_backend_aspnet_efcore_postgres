namespace ImproveU_backend.Models;

public class Exercicio : Base
{
    public int Id { get; set; }
    public string Nome { get; set; }    

    public int GrupoMuscularId { get; set; }
    public int ExercicioTipoId { get; set; }
    public virtual GrupoMuscular GrupoMuscular { get; set; }
    public virtual ExercicioTipo ExercicioTipo { get; set; }
}