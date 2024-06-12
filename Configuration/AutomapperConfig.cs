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

        CreateMap<Treino, TreinoResponseDto>()
            .ForMember(dest => dest.ItemTreinoResponseDto, opt => opt.MapFrom(src => src.ItensTreinoARealizar));

        CreateMap<TreinoCreateRequestDto, Treino>()
            .ForMember(dest => dest.ItensTreinoARealizar, opt => opt.MapFrom(src => src.ItemTreinoARealizarCreateRequestDto));
        CreateMap<TreinoUpdateRequestDto, Treino>();

        CreateMap<ItemTreinoARealizar, ItemTreinoResponseDto>()
            .ForMember(dest => dest.Exercicio, opt => opt.MapFrom(src => src.Exercicio));

        CreateMap<ItemTreinoARealizarCreateRequestDto, ItemTreinoARealizar>();
        //CreateMap<ItemTreinoUpdateRequestDto, ItemTreino>();

        

    }
}
