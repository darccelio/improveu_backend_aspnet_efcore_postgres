using System.ComponentModel.DataAnnotations;

namespace ImproveU_backend.Models.Dtos.TreinoDto;

public record ExercicioCreateRequestDto
{
    [Required(ErrorMessage = "O campo {0} é obrigatório")]
    [StringLength(100, MinimumLength = 5, ErrorMessage = "O campo {0} deve ter {1} caracteres")]
    public string Nome { get; set; }
}
