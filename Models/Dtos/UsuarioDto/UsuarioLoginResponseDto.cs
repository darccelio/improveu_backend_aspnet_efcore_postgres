namespace ImproveU_backend.Models.Dtos.UsuarioDto;

public record UsuarioLoginResponseDto
{
    public string AccessToken { get; init; }
    public double ExpiresIn { get; init; }
    public UsuarioTokenResponseDto UserToken { get; init; }
}