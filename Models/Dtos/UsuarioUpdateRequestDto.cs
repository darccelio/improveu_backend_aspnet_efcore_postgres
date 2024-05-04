namespace ImproveU_backend.Models.Dtos;

public record struct UsuarioUpdateRequestDto(int? papel, string? email, string? senha);

