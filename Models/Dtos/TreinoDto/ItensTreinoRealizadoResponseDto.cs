using ImproveU_backend.Models.Dtos.TreinoDto;

namespace ImproveU_backend.Models.Dtos;

public record ItensTreinoRealizadoResponseDto
{
    public int Id { get; init; }
    public int TreinoId { get; init; }
    public int ExercicioId { get; init; }
    public ExercicioResponseDto? Exercicio { get; init; }
    public int? Repeticoes { get; init; }
    public int? Series { get; init; }
    public int? IntervaloDescanso { get; init; }
    public int? CargaEmKg { get; init; }
    public int? FeedbackId { get; init; }
    public DateTime DataCriacao { get; init; }    
}

