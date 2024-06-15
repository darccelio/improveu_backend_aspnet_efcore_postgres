namespace ImproveU_backend.Models;

public class ItemTreinoRealizados : Base
{
    public int Id { get; set; }
    public int? CargaEmKg { get; set; } = null;
    public int? Repeticoes { get; set; } = null;
    public int? Series { get; set; } = null;
    public int? IntervaloDescanso { get; set; } = null;

    public int ExercicioId { get; set; }
    public virtual Exercicio ExercicioRealizado { get; set; }

    public int TreinoId { get; set; }
    public virtual Treino Treino { get; set; }

    public bool status { get; set; } = false;

    public int? FeedbackId { get; set; }
    public Feedback Feedback { get; set; }

}