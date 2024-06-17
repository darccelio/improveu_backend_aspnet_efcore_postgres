using ImproveU_backend.Models.Dtos.PessoaDto;
using ImproveU_backend.Models.Dtos.UsuarioDto;

namespace ImproveU_backend.Services.Interfaces.IPessoaSerivce;


public interface IEdFisicoService
{

    public Task<EdFisicoResponseDto> CriarAsync(EdFisicoCreateRequestDto edFisicoRequest);
    public Task<IEnumerable<EdFisicoResponseDto>> BuscarAsync(int skip, int take);
    public Task<EdFisicoResponseDto> BuscarPorIdAsync(int id);

}
