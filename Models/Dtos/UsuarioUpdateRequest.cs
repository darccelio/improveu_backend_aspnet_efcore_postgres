namespace ImproveU_backend.Models.Dtos;

public record struct UsuarioUpdateRequest(int? papel, string? email, string? senha);

