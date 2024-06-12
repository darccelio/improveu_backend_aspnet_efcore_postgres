namespace ImproveU_backend.Models.Dtos;

public record ItemTreinoRealizadoCreateRequestDto
{
    public int ExercicioId { get; init; }
    public int? TreinoId { get; set; }
    public int? Repeticoes { get; init; }
    public int? Series { get; init; }
    public int? IntervaloDescanso { get; init; }
    public int? CargaEmKg { get; set; }
    public int? FeedbackId { get; set; }
    public DateTime DataCriacao { get; init; }
}