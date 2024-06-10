namespace ImproveU_backend.Models.Dtos.UsuarioDto;

public record struct UsuarioUpdateRequestDto(int? papel, string? email, string? senha);

