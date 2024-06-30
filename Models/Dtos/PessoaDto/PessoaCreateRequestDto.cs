using System.ComponentModel.DataAnnotations;

namespace ImproveU_backend.Models.Dtos.PessoaDto;

public record PessoaCreateRequestDto
{
    [Required(ErrorMessage = "O campo {0} é obrigatório")]
    [StringLength(11, MinimumLength = 11, ErrorMessage = "O campo {0} deve ter {1} caracteres")]
    public string Cpf { get; set; }

    [Required(ErrorMessage = "O campo {0} é obrigatório")]
    [StringLength(100, ErrorMessage = "O campo {0} deve ter entre {2}  e {1} caracteres", MinimumLength = 5)]
    public string Nome { get; set; }

    [Required(ErrorMessage = "O campo {0} é obrigatório")]
    public string EmailUsuario { get; set; }

    //[Required(ErrorMessage = "O campo {0} é obrigatório")]
    //public int UsuarioId { get; set; }

}

