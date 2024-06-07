using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ImproveU_backend.Models.Dtos;

public record UsuarioCreateRequestDto
{
    [Required(ErrorMessage = "O campo {0} é obrigatório")]
    public string Email { get; set; }

    [Required(ErrorMessage = "O campo {0} é obrigatório")]
    public int Papel { get; set; }

    [PasswordPropertyText(true)]
    [Required(ErrorMessage = "O campo {0} é obrigatório")]
    [StringLength(15, MinimumLength = 5, ErrorMessage = "O campo {0} deve ter {1} caracteres")]
    public string Senha { get; set; }


    public UsuarioCreateRequestDto(int papel, string email, string senha)
    {
        Papel = papel;
        Email = email;
        Senha = senha;
    }

}


