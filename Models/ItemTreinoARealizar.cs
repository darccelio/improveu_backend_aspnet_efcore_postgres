namespace ImproveU_backend.Models;

public class ItemTreinoARealizar : Base
{
    public int Id { get; set; }
    public int? CargaEmKg { get; set; } = null;
    public int? Repeticoes { get; set; } = null;
    public int? Series { get; set; } = null;
    public int? IntervaloDescanso { get; set; } = null;

    public int ExercicioId { get; set; }
    public virtual Exercicio ExercicioARealizar { get; set; }
   
    public int  TreinoId { get; set; }
    public virtual Treino Treino { get; set; }
}
