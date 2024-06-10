namespace ImproveU_backend.Models.Dtos.PessoaDto;

public record FotosMetadadosResponseDto
{
    public int Id { get; set; }
    public string Path { get; set; }
    public int PessoaId { get; set; }
}
