using AutoMapper;
using ImproveU_backend.DatabaseConfiguration.Configuration;
using ImproveU_backend.Models;
using ImproveU_backend.Models.Dtos;
using ImproveU_backend.Models.Dtos.TreinoDto;
using ImproveU_backend.Services.Interfaces.IPessoaSerivce;
using ImproveU_backend.Services.Interfaces.ITreino;
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
        await validaRequisitosItemTreino(treinoRequestDto.ItensTreino);

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

        ICollection<ItemTreino> itemTreinos = _mapper.Map<ICollection<ItemTreino>>(treinoRequestDto.ItensTreino);
        //ICollection<ItemTreino> itemTreinos = _mapper.Map(treinoRequestDto.ItensTreino, treino.ItensTreino);
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

        if (treinoRequestDto.ItensTreino == null || treinoRequestDto.ItensTreino.Count == 0)
            throw new ArgumentException("O treino deve conter ao menos um item.", nameof(treinoRequestDto));



    }
    private async Task validaRequisitosItemTreino(ICollection<ItemTreinoCreateRequestDto> itensTreino)
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


            //if (item.Series == 0)
            //    throw new ArgumentException("O campo Series é obrigatório.", nameof(item));

            //if (item.Repeticoes == 0)
            //    throw new ArgumentException("O campo Repetições é obrigatório.", nameof(item));

            //if (item.CargaEmKg == 0)
            //    throw new ArgumentException("O campo Carga é obrigatório.", nameof(item));

            //if (item.IntervaloDescanso == 0)
            //    throw new ArgumentException("O campo Descanso é obrigatório.", nameof(item));
        }
    }

    public async Task<TreinoResponseDto> BuscarPorIdAsync(int id)
    {
        if (id <= 0)
        {
            throw new ArgumentException("Id do treino não pode ser menor ou igual a zero.");
        }

        Treino treino = await _context.Treinos.AsNoTracking().FirstAsync(e => e.Id == id);
        return _mapper.Map<TreinoResponseDto>(treino);
    }

    public async Task<IEnumerable<TreinoResponseDto>> BuscarAsync(int skip, int take)
    {
        List<Treino> treinos = await _context.Treinos.AsNoTracking().Skip(skip).Take(take).ToListAsync();
        return _mapper.Map<IEnumerable<TreinoResponseDto>>(treinos);
    }


}
