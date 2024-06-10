namespace ImproveU_backend.Models.Dtos;

public record ItemTreinoCreateRequestDto
{
    public int ExercicioId { get; init; }
    public int? TreinoId { get; init; }
    public int Repeticoes { get; init; }
    public int Series { get; init; }
    public int IntervaloDescanso { get; init; }
    public int CargaEmKg { get; set; }
}