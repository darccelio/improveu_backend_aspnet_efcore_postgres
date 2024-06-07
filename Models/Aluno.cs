using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ImproveU_backend.Models;

public class Aluno : Base
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; private set; }

    public int PessoaId { get; set; }

    public virtual Pessoa Pessoa { get; set; }

    public int? TreinoId { get; set; }

    public virtual ICollection<Treino> Treinos { get; set; }

    public virtual ICollection<Feedback> Feedbacks { get; set; }
}
