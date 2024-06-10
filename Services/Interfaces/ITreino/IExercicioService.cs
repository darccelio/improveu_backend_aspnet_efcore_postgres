using ImproveU_backend.Models;
using ImproveU_backend.Models.Dtos.TreinoDto;

namespace ImproveU_backend.Services.Interfaces.ITreino;


public interface IExercicioService
{
    Task<ExercicioResponseDto> CriarAsync(ExercicioCreateRequestDto exericio);
    Task<ExercicioResponseDto> BuscarPorIdAsync(int id);
    Task<IEnumerable<ExercicioResponseDto>> BuscarAsync(int skip, int take);
    Task<ExercicioResponseDto> BuscarPorNomeAsync(string nome);
    Task Atualizar(int id, ExercicioUpdateRequestDto ex);
}
