namespace ImproveU_backend.Models.Dtos;

public record EdFisicoRequestDto
{
    public string? RegistroConselho { get; set; } = null;
    public PessoaCreateResquestDto Pessoa { get; set; }
}
