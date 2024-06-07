using AutoMapper;
using ImproveU_backend.Models;
using ImproveU_backend.Models.Dtos;

namespace ImproveU_backend.Configuration;

public class AutomapperConfig : Profile
{
    public AutomapperConfig()
    {
        CreateMap<Exercicio, ExercicioResponseDto>();
        CreateMap<ExercicioCreateRequestDto, Exercicio>();
    }
}
