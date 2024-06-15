using AutoMapper;
using ImproveU_backend.Models;
using ImproveU_backend.Models.Dtos;
using ImproveU_backend.Models.Dtos.PessoaDto;
using ImproveU_backend.Models.Dtos.TreinoDto;
using ImproveU_backend.Models.Dtos.UsuarioDto;

namespace ImproveU_backend.Configuration;

public class AutomapperConfig : Profile
{
    public AutomapperConfig()
    {
        CreateMap<Usuario, UsuarioResponseDto>();
        CreateMap<UsuarioCreateRequestDto, Usuario>();
        CreateMap<UsuarioUpdateRequestDto, Usuario>();

        CreateMap<Pessoa, PessoaResponseDto>();
        CreateMap<PessoaCreateRequestDto, Pessoa>();

        CreateMap<Pessoa, Aluno>().ReverseMap();


        CreateMap<Aluno, AlunoResponseDto>()
            .ForMember(dest => dest.PessoaRequest, opt => opt.MapFrom(src => src.Pessoa));
        CreateMap<AlunoCreateRequestDto, Aluno>();
        CreateMap<AlunoUpdateRequestDto, Aluno>();

        CreateMap<EdFisico, EdFisicoResponseDto>()
            .ForMember(dest => dest.PessoaRequest, opt => opt.MapFrom(src => src.Pessoa));

        CreateMap<EdFisicoCreateRequestDto, EdFisico>()
            .ForMember(dest => dest.Pessoa, opt => opt.MapFrom(src => src.Pessoa));

        CreateMap<Exercicio, ExercicioResponseDto>();
        CreateMap<ExercicioCreateRequestDto, Exercicio>();
        CreateMap<ExercicioUpdateRequestDto, Exercicio>();

        //Plano de Treino
        CreateMap<TreinoARealizarCreateRequestDto, Treino>()
            .ForMember(dest => dest.ItensTreinoARealizar, opt => opt.MapFrom(src => src.ItemTreinoARealizarCreateRequestDto));

        CreateMap<Treino, TreinoARealizarResponseDto>()
            .ForMember(dest => dest.ItemTreinoARealizarResponseDto, opt => opt.MapFrom(src => src.ItensTreinoARealizar));
                
        CreateMap<ItensTreinoARealizarCreateRequestDto, ItemTreinoARealizar>();

        CreateMap<ItemTreinoARealizar, ItensTreinoARealizarResponseDto>()
            .ForMember(dest => dest.Exercicio, opt => opt.MapFrom(src => src.ExercicioARealizar));
                
        //Treino Realizado
        CreateMap<ItemTreinoRealizadoCreateRequestDto, ItemTreinoRealizados>();

        CreateMap<Treino, TreinoRealizadoResponseDto>()
            .ForMember(dest => dest.ItemTreinoRealizadoResponseDto, opt => opt.MapFrom(src => src.ItensTreinoRealizados));

        CreateMap<TreinoUpdateRequestDto, Treino>();


    }
}
