namespace ImproveU_backend.Models.Dtos.TreinoDto;

public record TreinoARealizarResponseDto
{
    public int Id { get; set; }
    public int EdFisicoId { get; set; }    
    public int AlunoId { get; set; }   
    public List<ItensTreinoARealizarResponseDto> ItemTreinoARealizarResponseDto { get; set; }
    public List<ItensTreinoARealizarResponseDto> ItemTreinoRealizadoResponseDto { get; set; }
    public DateOnly? DataInicioVigencia { get; set; }
    public DateOnly? DataFimVigencia { get; set; }
    public string DataCriacao { get; set; }

    public string UltimaAlteracao { get; set; }
}
