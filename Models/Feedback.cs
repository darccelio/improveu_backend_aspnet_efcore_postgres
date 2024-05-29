namespace ImproveU_backend.Models;

public class Feedback : Base
{
    public int Id { get; set; }
    public string Mensagem { get; set; }
    public int UsuarioId { get; set; }
    public virtual Usuario Usuario { get; set; }
    public int EdFisicoId { get; set; }
    public virtual EdFisico EdFisico { get; set; }
    public int ItemTreinoId { get; set; }
    public virtual ItemTreino ItemTreino { get; set; }

}
