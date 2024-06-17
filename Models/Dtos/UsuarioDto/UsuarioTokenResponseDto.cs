namespace ImproveU_backend.Models.Dtos.UsuarioDto;

public record UsuarioTokenResponseDto
{
    public string Id { get; set; }
    public string Email { get; set; }
    public IEnumerable<ClaimResponseDto> Claims { get; set; }
}

public class ClaimResponseDto
{
    public string Value { get; set; }
    public string Type { get; set; }
}
