using System.ComponentModel.DataAnnotations;

namespace ImproveU_backend.Models.Dtos.TreinoDto;

public record TreinoRealizadoCreateRequestDto
{
    [Required(ErrorMessage = "O campo {0} é obrigatório")]
    public int TreinoId { get; set; }

    [Required(ErrorMessage = "O campo {0} é obrigatório")]
    public int EdFisicoId { get; set; }

    [Required(ErrorMessage = "O campo {0} é obrigatório")]
    public int AlunoId { get; set; }

    [Required(ErrorMessage = "O campo {0} é obrigatório")]
    public List<ItemTreinoRealizadoCreateRequestDto> ItemTreinoRealizadoCreateReqDto { get; set; }
}