using ImproveU_backend.Models.Enums;

namespace ImproveU_backend.Models;

public class Feedback : Base
{
    public int Id { get; set; }
    public string Mensagem { get; set; }

    public int AlunoId { get; set; }
    public virtual Aluno Aluno { get; set; }

    public int EdFisicoId { get; set; }
    public virtual EdFisico EdFisico { get; set; }
    public int? ItemTreinoId { get; set; }
    public virtual ItemTreinoARealizar ItemTreino { get; set; }

    public virtual EDirecaoFeedback Direcao { get; set; }
}
