using System.ComponentModel.DataAnnotations.Schema;

namespace ImproveU_backend.Models;

public class EdFisico : Base
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; private set; }
    public string? RegistroConselho { get; set; } = null;
    public virtual Pessoa Pessoa { get; set; }
    public int PessoaId { get; set; }

    public int? TreinoId { get; set; }

    public virtual ICollection<Treino> Treinos { get; set; }

    public virtual ICollection<Feedback> Feedbacks { get; set; }    
}