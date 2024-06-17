using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace ImproveU_backend.Models;

public class Usuario
{
    public int Id { get; private set; }
    public string Email { get; set; }
    public int Papel { get; set; }
    public int? Ativo { get; set; }
    public string Senha { get; set; }
    //public virtual Pessoa Pessoa { get; set; }

    //public string IdentityUserId { get; set; }
    //public virtual ApplicationUser IdentityUser { get; set; }
}