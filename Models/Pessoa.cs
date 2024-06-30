using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ImproveU_backend.Models;

public class Pessoa : Base
{
    public int Id { get; private set; }
    public string Cpf { get; set; }
    public string Nome { get; set; }
    // relacionamento com a tabela usuario    
    public string IdentityUserId { get; set; }
    
    public virtual EdFisico EdFisico { get; set; }
    public virtual Aluno? Aluno { get; set; }
    public virtual ICollection<Foto> Fotos { get; set; }
}