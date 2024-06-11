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
        
        CreateMap<Aluno, AlunoResponseDto>();
        CreateMap<AlunoCreateRequestDto, Aluno>();
        CreateMap<AlunoUpdateRequestDto, Aluno>();

        CreateMap<EdFisico, EdFisicoResponseDto>();
        CreateMap<EdFisicoCreateRequestDto, EdFisico>();

        CreateMap<Exercicio, ExercicioResponseDto>();
        CreateMap<ExercicioCreateRequestDto, Exercicio>();
        CreateMap<ExercicioUpdateRequestDto, Exercicio>();

        //CreateMap<Treino, TreinoResponseDto>();
        //CreateMap<TreinoCreateRequestDto, Treino>();
        //CreateMap<TreinoUpdateRequestDto, Treino>();

        //CreateMap<ItemTreino, ItemTreinoResponseDto>();
        //CreateMap<ItemTreinoCreateRequestDto, ItemTreino>();

        CreateMap<Treino, TreinoResponseDto>()
            .ForMember(dest => dest.Itens, opt => opt.MapFrom(src => src.ItensTreino));

        CreateMap<TreinoCreateRequestDto, Treino>();
        CreateMap<TreinoUpdateRequestDto, Treino>();

        CreateMap<ItemTreino, ItemTreinoResponseDto>()
            .ForMember(dest => dest.Exercicio, opt => opt.MapFrom(src => src.Exercicio));

        CreateMap<ItemTreinoCreateRequestDto, ItemTreino>();
        //CreateMap<ItemTreinoUpdateRequestDto, ItemTreino>();

    }
}
