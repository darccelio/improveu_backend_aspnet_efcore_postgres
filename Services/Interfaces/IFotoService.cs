using ImproveU_backend.Models.Dtos;

namespace ImproveU_backend.Services.Interfaces;

public interface IFotoService
{
    Task<FotosMetadadosResponseDto> CriarAsync(FotosCreateRequestDto dto);
    Task<FotosMetadadosResponseDto> SalvarFotoUpload(FotosCreateRequestDto dto);
    Task<FotosResponseDto> BuscarPorIdAsync(int id);
    Task<IEnumerable<FotosResponseDto>> BuscarAsync(int skip, int take);
    Task AtualizarAsync(int id, FotosUpdateRequestDto dto);
}
