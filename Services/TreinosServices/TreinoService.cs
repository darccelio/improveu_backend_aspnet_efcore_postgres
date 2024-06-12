using AutoMapper;
using ImproveU_backend.DatabaseConfiguration.Configuration;
using ImproveU_backend.Models;
using ImproveU_backend.Models.Dtos;
using ImproveU_backend.Models.Dtos.TreinoDto;
using ImproveU_backend.Services.Interfaces.IPessoaSerivce;
using ImproveU_backend.Services.Interfaces.ITreino;
using ImproveU_backend.Services.PessoasServices;
using Microsoft.EntityFrameworkCore;
using System.Drawing.Text;
using System.Globalization;

namespace ImproveU_backend.Services.TreinoService;

public class TreinoService : ITreinoService
{
    private readonly ImproveuContext _context;
    private readonly IAlunoService _alunoService;
    private readonly IEdFisicoService _edFisicoService;
    private readonly IExercicioService _exercicioService;
    private readonly IMapper _mapper;

    public TreinoService(ImproveuContext context,
                         IAlunoService alunoService,
                         IEdFisicoService edFisicoService,
                         IMapper mapper,
                         IExercicioService exercicioService)
    {
        _context = context;
        _alunoService = alunoService;
        _edFisicoService = edFisicoService;
        _mapper = mapper;
        _exercicioService = exercicioService;
    }

    public async Task<TreinoResponseDto> CriarAsync(TreinoCreateRequestDto treinoRequestDto)
    {
        await validaRequisitosTreino(treinoRequestDto);
        await validaRequisitosItemTreino(treinoRequestDto.ItemTreinoARealizarCreateRequestDto);

        var treino = new Treino
        {
            AlunoId = treinoRequestDto.AlunoId,
            EdFisicoId = treinoRequestDto.EdFisicoId,
            Status = 1,
            DataInicioVigencia = treinoRequestDto.DataInicioVigencia.HasValue ? treinoRequestDto.DataInicioVigencia.Value : (DateTime?)null,
            DataFimVigencia = treinoRequestDto.DataFimVigencia.HasValue ? treinoRequestDto.DataFimVigencia.Value : (DateTime?)null
        };

        await _context.Treinos.AddAsync(treino);
        await _context.SaveChangesAsync();

        ICollection<ItemTreinoARealizar> itemTreinos = _mapper.Map<ICollection<ItemTreinoARealizar>>(treinoRequestDto.ItemTreinoARealizarCreateRequestDto);

        foreach (var item in itemTreinos)
        {
            item.TreinoId = treino.Id;
        }

        await _context.ItensTreinos.AddRangeAsync(itemTreinos);

        await _context.SaveChangesAsync();

        return _mapper.Map<TreinoResponseDto>(treino);

    }

    private async Task validaRequisitosTreino(TreinoCreateRequestDto treinoRequestDto)
    {

        if (treinoRequestDto is null)
            throw new ArgumentNullException(nameof(treinoRequestDto), "O objeto TreinoCreateRequestDto não pode ser nulo.");

        var aluno = await _alunoService.BuscarPorIdAsync(treinoRequestDto.AlunoId) ?? throw new ArgumentException("Aluno não localizado.", nameof(treinoRequestDto));

        var edFisico = await _edFisicoService.BuscarPorIdAsync(treinoRequestDto.EdFisicoId) ?? throw new ArgumentException("EdFisico não localizado.", nameof(treinoRequestDto));

        if (treinoRequestDto.DataInicioVigencia.HasValue && treinoRequestDto.DataFimVigencia.HasValue)
        {
            if (treinoRequestDto.DataInicioVigencia > treinoRequestDto.DataFimVigencia)
                throw new ArgumentException("Data de início de vigência não pode ser maior que a data de fim de vigência.", nameof(treinoRequestDto));

            var treinoExistente = _context.Treinos.AsNoTracking()
                                 .FirstOrDefault(t => t.Status == 1 &&
                                                      t.DataInicioVigencia == treinoRequestDto.DataInicioVigencia &&
                                                      t.DataFimVigencia == treinoRequestDto.DataFimVigencia);

            if (treinoExistente != null)
                throw new ArgumentException("Já existe um treino cadastrado com status ativo com essas datas de vigência.", nameof(treinoRequestDto));
        }

        if (treinoRequestDto.ItemTreinoARealizarCreateRequestDto == null || treinoRequestDto.ItemTreinoARealizarCreateRequestDto.Count == 0)
            throw new ArgumentException("O treino deve conter ao menos um item.", nameof(treinoRequestDto));
    }

    private async Task validaRequisitosItemTreino(ICollection<ItemTreinoARealizarCreateRequestDto> itensTreino)
    {
        foreach (var item in itensTreino)
        {
            if (item is null)
                throw new ArgumentNullException(nameof(item), "O objeto ItemTreinoCreateRequestDto não pode ser nulo.");

            if (item.ExercicioId == 0)
                throw new ArgumentException("O campo ExercicioId é obrigatório.", nameof(item));

            ExercicioResponseDto ex = await _exercicioService.BuscarPorIdAsync(item.ExercicioId);

            if (ex is null)
                throw new ArgumentException("Exercicio não localizado.", nameof(item.ExercicioId));

        }
    }

    public async Task<TreinoResponseDto> BuscarPorIdAsync(int id)
    {
        if (id <= 0)
        {
            throw new ArgumentException("Id do treino não pode ser menor ou igual a zero.");
        }

        Treino? treino = await _context.Treinos.AsNoTracking()
                                               .Include(it => it.ItensTreinoARealizar)
                                               .ThenInclude(e => e.Exercicio)
                                               .Include(a => a.Aluno)
                                               .Include(e => e.EdFisico)
                                               .FirstOrDefaultAsync(e => e.Id == id);

        if (treino is null)
            throw new ArgumentException("Treino não encontrado");

        TreinoResponseDto treinoResponseDto = _mapper.Map<TreinoResponseDto>(treino);

        return treinoResponseDto;
    }

    public async Task<IEnumerable<TreinoResponseDto>> BuscarAsync(int skip, int take)
    {
        List<Treino> treinos = await _context.Treinos.Include(i => i.ItensTreinoARealizar).ThenInclude(e => e.Exercicio).AsNoTracking().Skip(skip).Take(take).ToListAsync();
        return _mapper.Map<IEnumerable<TreinoResponseDto>>(treinos);
    }

    public async Task<IEnumerable<TreinoResponseDto>> BuscarPorEducadorFisicoIdAsync(int edFisicoId)
    {
        if (edFisicoId <= 0)
        {
            throw new ArgumentException("Id do educador físico não pode ser menor ou igual a zero.");
        }

        EdFisico? edFisico = await _context.EdFisicos.AsNoTracking()
                                                     .Include(t => t.Treinos)
                                                     .ThenInclude(it => it.ItensTreinoARealizar)
                                                     .ThenInclude(e => e.Exercicio)
                                                     .FirstOrDefaultAsync(e => e.Id == edFisicoId);

        if (edFisico is null)
        {
            throw new ArgumentException("Educador físico não encontrado.");
        }

        if (!edFisico.Treinos.Any())
        {
            throw new Exception("Não há treinos cadastrados para este educador físico.");
        }

        return _mapper.Map<IEnumerable<TreinoResponseDto>>(edFisico.Treinos);
    }

    public async Task<IEnumerable<TreinoResponseDto>> BuscarPorAlunoIdAsync(int alunoId)
    {
        if (alunoId <= 0)
        {
            throw new ArgumentException("Id do aluno não pode ser menor ou igual a zero.");
        }

        Aluno? aluno = await _context.Alunos.AsNoTracking()
                                                     .Include(t => t.Treinos)
                                                     .ThenInclude(it => it.ItensTreinoARealizar)
                                                     .ThenInclude(e => e.Exercicio)
                                                     .FirstOrDefaultAsync(e => e.Id == alunoId);

        if (aluno is null)
        {
            throw new ArgumentException("Aluno não encontrado.");
        }

        if (!aluno.Treinos.Any())
        {
            throw new Exception("Não há treinos cadastrados para este aluno.");
        }

        return _mapper.Map<IEnumerable<TreinoResponseDto>>(aluno.Treinos);
    }

    public async Task<TreinoRealizadoResponseDto> RealizarTreino(int id, TreinoRealizadoCreateRequestDto treinoRealizadoCreateReqDto)
    {
        if (treinoRealizadoCreateReqDto == null)
            throw new ArgumentNullException(nameof(treinoRealizadoCreateReqDto), "O objeto TreinoRealizadoResponseDto não pode ser nulo.");

        if(id != treinoRealizadoCreateReqDto.TreinoId)
            throw new ArgumentException($"Id enviado por parâmetro {id} não corresponde ao id do treino realizado.", nameof(treinoRealizadoCreateReqDto.TreinoId));

        if (treinoRealizadoCreateReqDto.TreinoId <= 0)
            throw new ArgumentException("Id do treino não pode ser menor ou igual a zero.", nameof(treinoRealizadoCreateReqDto.TreinoId));
        
        if(treinoRealizadoCreateReqDto.ItemTreinoRealizadoCreateReqDto == null || treinoRealizadoCreateReqDto.ItemTreinoRealizadoCreateReqDto.Count == 0)
            throw new ArgumentException("O treino realizado deve conter ao menos um item.", nameof(treinoRealizadoCreateReqDto.ItemTreinoRealizadoCreateReqDto));


        //Pode validar se os itens enviados correspondem a lista preparada pelo educador
        //Pode validar se o treino está com status ativo
        //Pode validar se o treino está dentro do período de vigência
        //Pode validar se o treino já foi realizado
        
        throw NotImplementedException();
    }

}

