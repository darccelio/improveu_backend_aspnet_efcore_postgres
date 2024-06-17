using ImproveU_backend.Models;
using ImproveU_backend.Models.Dtos.TreinoDto;

namespace ImproveU_backend.Services.Interfaces.ITreinoServices;


public interface IExercicioService
{
    Task<ExercicioResponseDto> CriarAsync(ExercicioCreateRequestDto exericio);
    Task<ExercicioResponseDto> BuscarPorIdAsync(int id);
    Task<IEnumerable<ExercicioResponseDto>> BuscarAsync(int skip, int take);
    Task<IEnumerable<ExercicioResponseDto>> BuscarPorNomeAsync(string nome, int skip = 0, int take = 10);
    Task Atualizar(int id, ExercicioUpdateRequestDto ex);
}
