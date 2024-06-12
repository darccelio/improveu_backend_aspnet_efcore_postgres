using ImproveU_backend.Models.Dtos;
using ImproveU_backend.Models.Dtos.TreinoDto;

namespace ImproveU_backend.Services.Interfaces.ITreino;

public interface ITreinoService
{
    Task<TreinoResponseDto> CriarAsync(TreinoCreateRequestDto treinoRequestDto);
    Task<TreinoResponseDto> BuscarPorIdAsync(int id);
    Task<IEnumerable<TreinoResponseDto>> BuscarAsync(int skip, int take);
    Task<IEnumerable<TreinoResponseDto>> BuscarPorEducadorFisicoIdAsync(int edFisicoId);
    Task<IEnumerable<TreinoResponseDto>> BuscarPorAlunoIdAsync(int edFisicoId);
    Task<TreinoRealizadoResponseDto> RealizarTreino(int id, TreinoRealizadoCreateRequestDto treinoRealizadoCreateReqDto);
}
