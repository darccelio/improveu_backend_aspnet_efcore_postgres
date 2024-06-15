using System.ComponentModel.DataAnnotations;

namespace ImproveU_backend.Models.Dtos.TreinoDto;

public record TreinoARealizarCreateRequestDto()
{
    [Required(ErrorMessage = "O campo {0} é obrigatório")]
    public int EdFisicoId { get; set; }

    [Required(ErrorMessage = "O campo {0} é obrigatório")]
    public int AlunoId { get; set; }

    public DateOnly? DataInicioVigencia { get; set; }
    public DateOnly? DataFimVigencia { get; set; }

    [Required(ErrorMessage = "O campo {0} é obrigatório")]
    public virtual ICollection<ItensTreinoARealizarCreateRequestDto> ItemTreinoARealizarCreateRequestDto { get; set; }
}
