using ImproveU_backend.Models.Dtos;
using ImproveU_backend.Models.Dtos.TreinoDto;

namespace ImproveU_backend.Services.Interfaces.ITreino;

public interface ITreinoService
{
    Task<TreinoARealizarResponseDto> CriarPlanoDeTreinoAsync(TreinoARealizarCreateRequestDto treinoRequestDto);
    Task<TreinoARealizarResponseDto> BuscarPorIdAsync(int id);
    Task<IEnumerable<TreinoARealizarResponseDto>> BuscarAsync(int skip, int take);
    Task<IEnumerable<TreinoARealizarResponseDto>> BuscarPlanoAtivoPorEducadorFisicoIdAsync(int edFisicoId, int skip, int take);
    Task<IEnumerable<TreinoARealizarResponseDto>> BuscarPlanoAtivoPorAlunoIdAsync(int edFisicoId, int skip, int take);
    Task<TreinoARealizarResponseDto> RealizarTreinoAsync(TreinoRealizadoCreateRequestDto treinoRealizadoCreateReqDto);

}
