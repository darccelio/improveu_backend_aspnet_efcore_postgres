namespace ImproveU_backend.Models.Dtos;

public record FotosMetadadosResponseDto
{
    public int Id { get; set; }
    public string Path { get; set; }
    public int PessoaId { get; set; }
}
