namespace ImproveU_backend.Models.Dtos.TreinoDto;

public class TreinoResponseDto
{
    public int Id { get; set; }
    public string Nome { get; set; }
    public List<ItemTreinoResponseDto> Itens { get; set; }
}
