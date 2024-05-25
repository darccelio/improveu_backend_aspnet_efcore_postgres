namespace ImproveU_backend.Models.Dtos;

public record EdFisicoCreateRequestDto
{
    public string? RegistroConselho { get; set; } = null;
    public PessoaCreateResquestDto Pessoa { get; set; }
}
