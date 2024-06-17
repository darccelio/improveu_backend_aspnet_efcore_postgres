using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ImproveU_backend.Models.Dtos.UsuarioDto;

public record UsuarioLoginRequestDto
{
    [Required(ErrorMessage = "O campo {0} é obrigatório")]
    [EmailAddress]
    public string Email { get; set; }


    [PasswordPropertyText(true)]
    [Required(ErrorMessage = "O campo {0} é obrigatório")]
    [StringLength(15, MinimumLength = 6, ErrorMessage = "O campo {0} deve ter {1} caracteres")]
    public string Senha { get; set; }
}
