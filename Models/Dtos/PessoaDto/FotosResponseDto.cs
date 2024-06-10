namespace ImproveU_backend.Models.Dtos.PessoaDto;

public record FotosResponseDto
{
    public int Id { get; set; }
    public string Path { get; set; }
    public string Extensao { get; set; }
    public int PessoaId { get; set; }
    //public IFormFile Foto { get; set; } = new FormFile(Stream.Null, 0, 0, null, null);
    public string FotoBase64 { get; set; }
}
