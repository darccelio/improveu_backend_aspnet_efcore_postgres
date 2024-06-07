using System.ComponentModel.DataAnnotations;

namespace ImproveU_backend.Models.Dtos;

public record EdFisicoCreateRequestDto
{
    [Required(ErrorMessage = "O campo {0} é obrigatório")]    
    public string? RegistroConselho { get; set; } = null;

    public PessoaCreateResquestDto Pessoa { get; set; }
}
