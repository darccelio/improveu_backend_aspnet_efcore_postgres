namespace ImproveU_backend.Models;

public class ItemTreino : Base
{
    public int Id { get; set; }
    public int CargaEmKg { get; set; }
    public int Repeticoes { get; set; }

    public int ExercicioId { get; set; }
    public Exercicio Exercicio { get; set; }

    public int TreinoId { get; set; }
    public Treino Treino { get; set; }

    public int  GrupoTreinoId { get; set; }
    public virtual GrupoTreino GrupoTreino { get; set; }

    public int FeedbackId{ get; set; }
    public Feedback Feedback { get; set; }

}
