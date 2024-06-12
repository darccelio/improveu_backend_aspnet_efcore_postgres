namespace ImproveU_backend.Models.Dtos.TreinoDto;

public record TreinoRealizadoResponseDto
{
    public int TreinoId { get; set; }
    public string? Nome { get; set; }
    public int EdFisicoId { get; set; }
    public int AlunoId { get; set; }
    public List<ItemTreinoResponseDto> ItemTreinoResponseDto { get; set; }
    public DateTime DataCriacao { get; set; }
}
