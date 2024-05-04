using ImproveU_backend.Models.Dtos;

namespace ImproveU_backend.Services.Interfaces;

public interface IUsuarioService
{

    public Task<UsuarioResponseDto> BuscarUsuarioPorIdAsync(int id);
    public Task<UsuarioResponseDto> CriarUsuarioAsync(UsuarioCreateRequestDto usuarioRequest);
    public Task<IEnumerable<UsuarioResponseDto>> BuscarUsuariosAsync(int skip, int take);

    public Task AtualizarUsuarioAsync(int id, UsuarioUpdateRequestDto usuarioRequest);
    public Task DeletarUsuarioPorIdAsync(int id);

    public Task<UsuarioResponseDto> BuscarUsuarioPorEmailAsync(string email);


}
