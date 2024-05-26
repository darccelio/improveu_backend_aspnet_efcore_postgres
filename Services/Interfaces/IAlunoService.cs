using ImproveU_backend.Models.Dtos;

namespace ImproveU_backend.Services.Interfaces;

public interface IAlunoService
{

    public Task<AlunoResponseDto> CriarAsync(AlunoCreateRequestDto alunoRequest);
    public Task<IEnumerable<AlunoResponseDto>> BuscarAsync(int skip, int take);
    public Task<AlunoResponseDto> BuscarPorIdAsync(int id);

    public Task AtualizarAsync(int id, AlunoUpdateRequestDto alunoRequest);
    public Task RemoverPorIdAsync(int id);
}
