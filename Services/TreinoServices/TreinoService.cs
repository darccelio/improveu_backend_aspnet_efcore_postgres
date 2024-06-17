using AutoMapper;
using ImproveU_backend.DatabaseConfiguration.Configuration;
using ImproveU_backend.Models;
using ImproveU_backend.Models.Dtos;
using ImproveU_backend.Models.Dtos.TreinoDto;
using ImproveU_backend.Services.Interfaces.IPessoaServices;
using ImproveU_backend.Services.Interfaces.ITreinoServices;
using ImproveU_backend.Services.PessoasServices;
using Microsoft.EntityFrameworkCore;
using System.Drawing.Text;
using System.Globalization;

namespace ImproveU_backend.Services.TreinoServices;

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

    public async Task<TreinoARealizarResponseDto> CriarPlanoDeTreinoAsync(TreinoARealizarCreateRequestDto treinoARealizarCreateReqDto)
    {
        await validaRequisitosTreino(treinoARealizarCreateReqDto);
        await validaRequisitosItemTreino(treinoARealizarCreateReqDto.ItemTreinoARealizarCreateRequestDto);

        var treino = new Treino
        {
            AlunoId = treinoARealizarCreateReqDto.AlunoId,
            EdFisicoId = treinoARealizarCreateReqDto.EdFisicoId,
            Status = 1,
            DataInicioVigencia = treinoARealizarCreateReqDto.DataInicioVigencia.HasValue ? treinoARealizarCreateReqDto.DataInicioVigencia.Value : (DateOnly?)null,
            DataFimVigencia = treinoARealizarCreateReqDto.DataFimVigencia.HasValue ? treinoARealizarCreateReqDto.DataFimVigencia.Value : (DateOnly?)null
        };

        await _context.Treinos.AddAsync(treino);
        await _context.SaveChangesAsync();

        ICollection<ItemTreinoARealizar> itemTreinos = _mapper.Map<ICollection<ItemTreinoARealizar>>(treinoARealizarCreateReqDto.ItemTreinoARealizarCreateRequestDto);

        foreach (var item in itemTreinos)
        {
            item.TreinoId = treino.Id;
        }

        await _context.ItensTreinosARealizar.AddRangeAsync(itemTreinos);

        await _context.SaveChangesAsync();

        return _mapper.Map<TreinoARealizarResponseDto>(treino);

    }

    public async Task<TreinoARealizarResponseDto> BuscarPorIdAsync(int id)
    {
        if (id <= 0)
        {
            throw new ArgumentException("Id do treino não pode ser menor ou igual a zero.");
        }

        Treino? treino = await _context.Treinos.AsNoTracking()
                                               .Include(a => a.Aluno)
                                               .Include(e => e.EdFisico)
                                               .Include(it => it.ItensTreinoARealizar)
                                               .ThenInclude(e => e.ExercicioARealizar)
                                               .Include(it => it.ItensTreinoRealizados)
                                               .ThenInclude(e => e.ExercicioRealizado)
                                               .FirstOrDefaultAsync(e => e.Id == id);

        if (treino is null)
            throw new ArgumentException("Treino não encontrado");

        TreinoARealizarResponseDto treinoResponseDto = _mapper.Map<TreinoARealizarResponseDto>(treino);

        return treinoResponseDto;
    }

    public async Task<IEnumerable<TreinoARealizarResponseDto>> BuscarAsync(int skip, int take)
    {
        List<Treino> treinos = await _context.Treinos.Include(i => i.ItensTreinoARealizar)
                                                        .ThenInclude(e => e.ExercicioARealizar)
                                                     .Include(itrdo => itrdo.ItensTreinoRealizados)
                                                        .ThenInclude(erdo => erdo.ExercicioRealizado)
                                                     .AsNoTracking().Skip(skip).Take(take).ToListAsync();
        return _mapper.Map<IEnumerable<TreinoARealizarResponseDto>>(treinos);
    }

    public async Task<IEnumerable<TreinoARealizarResponseDto>> BuscarPlanoAtivoPorEducadorFisicoIdAsync(int edFisicoId, int skip, int take)
    {
        if (edFisicoId <= 0)
        {
            throw new ArgumentException("Id do educador físico não pode ser menor ou igual a zero.");
        }

        EdFisico? edFisico = await _context.EdFisicos.AsNoTracking()
                                                    .Include(t => t.Treinos)
                                                        .ThenInclude(it => it.ItensTreinoARealizar)
                                                            .ThenInclude(e => e.ExercicioARealizar)
                                                    .AsNoTracking()
                                                    .Where(e => e.Treinos.Any(t => t.Status == 1) && 
                                                                e.Id == edFisicoId)
                                                    .Skip(skip).Take(take)
                                                    .FirstOrDefaultAsync();
        if (edFisico is null)
        {
            throw new ArgumentException("Educador físico não encontrado.");
        }

        if (!edFisico.Treinos.Any())
        {
            throw new Exception("Não há treinos cadastrados para este educador físico.");
        }

        return _mapper.Map<IEnumerable<TreinoARealizarResponseDto>>(edFisico.Treinos);
    }

    public async Task<IEnumerable<TreinoARealizarResponseDto>> BuscarPlanoAtivoPorAlunoIdAsync(int alunoId, int skip, int take)
    {
        if (alunoId <= 0)
        {
            throw new ArgumentException("Id do aluno não pode ser menor ou igual a zero.");
        }

        Aluno? aluno = await _context.Alunos.AsNoTracking().AsNoTracking()
                                                     .Include(t => t.Treinos)
                                                        .ThenInclude(it => it.ItensTreinoARealizar)
                                                            .ThenInclude(e => e.ExercicioARealizar)
                                                     .Include(p => p.Pessoa)
                                                        .ThenInclude(u => u.Usuario)
                                                     .Where(e => e.Id == alunoId)
                                                     .Skip(skip).Take(take)
                                                     .FirstOrDefaultAsync();

        if (aluno is null)
        {
            throw new ArgumentException("Aluno não encontrado.");
        }

        if (!aluno.Treinos.Any())
        {
            throw new ArgumentException("Não há treinos cadastrados para este aluno.");
        }

        return _mapper.Map<IEnumerable<TreinoARealizarResponseDto>>(aluno.Treinos);
    }

    public async Task<TreinoARealizarResponseDto> RealizarTreinoAsync(TreinoRealizadoCreateRequestDto treinoRealizadoCreateReqDto)
    {
        if (treinoRealizadoCreateReqDto == null)
            throw new ArgumentNullException(nameof(treinoRealizadoCreateReqDto), "O objeto TreinoRealizadoResponseDto não pode ser nulo.");

        if (treinoRealizadoCreateReqDto.TreinoId <= 0)
            throw new ArgumentException("Id do treino não pode ser menor ou igual a zero.", nameof(treinoRealizadoCreateReqDto.TreinoId));

        if (treinoRealizadoCreateReqDto.ItemTreinoRealizadoCreateReqDto == null || treinoRealizadoCreateReqDto.ItemTreinoRealizadoCreateReqDto.Count == 0)
            throw new ArgumentException("O treino realizado deve conter ao menos um item.", nameof(treinoRealizadoCreateReqDto.ItemTreinoRealizadoCreateReqDto));


        //Pode validar se os itens enviados correspondem a lista preparada pelo educador
        Treino treino = await validaRequisitosTreinoRealizado(treinoRealizadoCreateReqDto);

        IEnumerable<ItemTreinoRealizados> itemTreinoRealizados = _mapper.Map<IEnumerable<ItemTreinoRealizados>>(treinoRealizadoCreateReqDto.ItemTreinoRealizadoCreateReqDto);

        _context.ItensTreinosRealizados.AddRange(itemTreinoRealizados);
        _context.SaveChanges();

        return _mapper.Map<TreinoARealizarResponseDto>(treino);
    }


    private async Task<Treino> validaRequisitosTreinoRealizado(TreinoRealizadoCreateRequestDto treinoRealizadoDto)
    {
        Treino? treino = _context.Treinos.Include(i => i.ItensTreinoARealizar)
                                            .ThenInclude(e => e.ExercicioARealizar)
                                         .FirstOrDefault(t => t.Id == treinoRealizadoDto.TreinoId);

        if (treino is null)
            throw new ArgumentException("Treino não localizado.", nameof(treinoRealizadoDto.TreinoId));

        if (!treino.DataInicioVigencia.HasValue && !treino.DataFimVigencia.HasValue)
            throw new ArgumentException("Treino não possui período de vigência cadastrado.", nameof(treinoRealizadoDto.TreinoId));

        if (treino.Status != 1 || treino.DataFimVigencia.HasValue && treino.DataFimVigencia < DateOnly.FromDateTime(DateTime.Now))
            throw new ArgumentException("Treino não está ativo.", nameof(treinoRealizadoDto.TreinoId));
        

        if (treino.DataInicioVigencia > DateOnly.FromDateTime(DateTime.Now) || treino.DataFimVigencia < DateOnly.FromDateTime(DateTime.Now))
            throw new ArgumentException("Periodo de início da vigência é posterior a data fim da vigência do treino.", nameof(treinoRealizadoDto.TreinoId));

        return treino;
        //TODO VALIDAR O REQUISITO (SE O ALUNO SÓ PODE FAZER OS EXERCÍCIOS QUE ESTÃO NO PLANO DE TREINAMENTO)
        //foreach (var item in treinoRealizadoDto.ItemTreinoRealizadoCreateReqDto)
        //{
        //    if (treino.ItensTreinoARealizar.FirstOrDefault(i => i.ExercicioId == item.ExercicioId) == null)
        //        throw new ArgumentException($"Id {item.ExercicioId} do Execicio treino não corresponde ao treino planejado.", nameof(treinoRealizadoDto.TreinoId));
        //}

    }

    private async Task validaRequisitosTreino(TreinoARealizarCreateRequestDto treinoRequestDto)
    {

        if (treinoRequestDto is null)
            throw new ArgumentNullException(nameof(treinoRequestDto), "O objeto TreinoCreateRequestDto não pode ser nulo.");

        var aluno = await _alunoService.BuscarPorIdAsync(treinoRequestDto.AlunoId) ?? throw new ArgumentException("Aluno não localizado.", nameof(treinoRequestDto));

        var edFisico = await _edFisicoService.BuscarPorIdAsync(treinoRequestDto.EdFisicoId) ?? throw new ArgumentException("EdFisico não localizado.", nameof(treinoRequestDto));

        if (treinoRequestDto.DataInicioVigencia.HasValue &&
            treinoRequestDto.DataFimVigencia.HasValue)
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

    private async Task validaRequisitosItemTreino(ICollection<ItensTreinoARealizarCreateRequestDto> itensTreino)
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


}

