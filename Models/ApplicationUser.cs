using Microsoft.AspNetCore.Identity;

namespace ImproveU_backend.Models;

public class ApplicationUser : IdentityUser
{
    // Propriedade de navegação para o Usuario
    public virtual Pessoa Pessoa{ get; set; }
    public int Papel { get; set; }
    public int Ativo { get; set; } = 1;
    // Propriedades do IdentityUser que são necessárias
    public bool EmailConfirmed { get; set; } = true; // Definir como true ou false conforme necessário
}
