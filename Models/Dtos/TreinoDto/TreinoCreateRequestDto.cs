using System.ComponentModel.DataAnnotations;

namespace ImproveU_backend.Models.Dtos.TreinoDto;

public record TreinoCreateRequestDto()
{
    [Required(ErrorMessage = "O campo {0} é obrigatório")]
    public int EdFisicoId { get; set; }

    [Required(ErrorMessage = "O campo {0} é obrigatório")]
    public int AlunoId { get; set; }

    public DateTime? DataInicioVigencia { get; set; }
    public DateTime? DataFimVigencia { get; set; }

    [Required(ErrorMessage = "O campo {0} é obrigatório")]
    public virtual ICollection<ItemTreinoCreateRequestDto> ItensTreino { get; set; }
}
