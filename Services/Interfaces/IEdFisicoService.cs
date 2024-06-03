using ImproveU_backend.Models.Dtos;

namespace ImproveU_backend.Services.Interfaces;


public interface IEdFisicoService
{
    //public Task<UsuarioResponseDto> BuscarEdFisicoPorIdAsync(int id);
    public Task<EdFisicoResponseDto> CriarAsync(EdFisicoCreateRequestDto edFisicoRequest);
    public Task<IEnumerable<EdFisicoResponseDto>> BuscarAsync(int skip, int take);
    public Task<EdFisicoResponseDto> BuscarPorIdAsync(int id);

    public Task AtualizarAsync(int id, UsuarioUpdateRequestDto edFisicoRequest);
    public Task DeletarPorIdAsync(int id);

}
