using System.ComponentModel.DataAnnotations;

namespace ImproveU_backend.Models.Dtos;

public record AlunoUpdateRequestDto
{

    [Required(ErrorMessage = "O campo {0} é obrigatório")]
    public int Id { get; set; }

    [Required(ErrorMessage = "O campo {0} é obrigatório")]
    [StringLength(100, MinimumLength = 11, ErrorMessage = "O campo {0} deve ter {1} caracteres")]
    public string Nome { get; set; }

    [Required(ErrorMessage = "O campo {0} é obrigatório")]
    [StringLength(11, MinimumLength = 11, ErrorMessage = "O campo {0} deve ter {1} caracteres")]
    public string Cpf { get; set; }
}