namespace ImproveU_backend.Models.Dtos.TreinoDto;

public record TreinoResponseDto
{
    public int Id { get; set; }
    public string Nome { get; set; }
    public int EdFisicoId { get; set; }    
    public int AlunoId { get; set; }   
    public List<ItemTreinoResponseDto> Itens { get; set; }
}
