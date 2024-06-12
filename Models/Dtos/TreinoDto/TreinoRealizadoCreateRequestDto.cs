namespace ImproveU_backend.Models.Dtos.TreinoDto;

public record TreinoRealizadoCreateRequestDto
{
    public int TreinoId { get; set; }
    public string? Nome { get; set; }
    public int EdFisicoId { get; set; }
    public int AlunoId { get; set; }
    public List<ItemTreinoARealizarCreateRequestDto> ItemTreinoRealizadoCreateReqDto { get; set; }
}