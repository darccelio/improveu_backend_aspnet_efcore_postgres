using AutoMapper;
using ImproveU_backend.DatabaseConfiguration.Configuration;
using ImproveU_backend.Models;
using ImproveU_backend.Models.Dtos.TreinoDto;
using ImproveU_backend.Services.Interfaces.ITreino;
using Microsoft.EntityFrameworkCore;
using System.Collections.Immutable;
using System.Runtime.CompilerServices;

namespace ImproveU_backend.Services.TreinoService;

public class ExercicioService : IExercicioService
{

    private readonly ImproveuContext _context;
    private readonly IMapper _mapper;

    public ExercicioService(ImproveuContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<ExercicioResponseDto> CriarAsync(ExercicioCreateRequestDto ex)
    {
        if (string.IsNullOrWhiteSpace(ex.Nome))
        {
            throw new ArgumentException("Nome do exercicio não pode ser vazio.");
        }

        Exercicio? exercicio = await _context.Exercicios.FirstOrDefaultAsync(e => e.Nome == ex.Nome);

        if (exercicio != null)
        {
            throw new ArgumentException("Exercicio já cadastrado.");
        }

        Exercicio novoExercicio = new Exercicio() { Nome = ex.Nome };
        _context.Exercicios.Add(novoExercicio);
        _context.SaveChanges();

        return _mapper.Map<ExercicioResponseDto>(novoExercicio);
    }

    public async Task<IEnumerable<ExercicioResponseDto>> BuscarAsync(int skip, int take)
    {
        List<Exercicio> exercicios = await _context.Exercicios.AsNoTracking().Skip(skip).Take(take).ToListAsync();
        return _mapper.Map<IEnumerable<ExercicioResponseDto>>(exercicios);
    }

    public async Task<ExercicioResponseDto> BuscarPorIdAsync(int id)
    {
        if (id <= 0)
        {
            throw new ArgumentException("Id do exercicio não pode ser menor ou igual a zero.");
        }

        Exercicio exercicio = await _context.Exercicios.AsNoTracking().FirstAsync(e => e.Id == id);
        return _mapper.Map<ExercicioResponseDto>(exercicio);
    }

    public async Task<IEnumerable<ExercicioResponseDto>> BuscarPorNomeAsync(string nome, int skip = 0, int take = 10)
    {

        if (string.IsNullOrWhiteSpace(nome))
        {
            throw new ArgumentException("Nome do exercicio não pode ser vazio.");
        }

        IEnumerable<Exercicio> exercicios = await _context.Exercicios.AsNoTracking()
                                                                    .Where(e => e.Nome.ToLower().Contains(nome.ToLower()))
                                                                    .Skip(skip).Take(take).ToListAsync();

        return _mapper.Map<IEnumerable<ExercicioResponseDto>>(exercicios);
    }

    public async Task Atualizar(int id, ExercicioUpdateRequestDto ex)
    {
        if (id <= 0)
        {
            throw new ArgumentException("Id do exercicio não pode ser menor ou igual a zero.");
        }

        if (id != ex.Id)
        {
            throw new ArgumentException("Id do exercicio não corresponde ao id informado.");
        }

        Exercicio? exercicio = _context.Exercicios.Find(id) ?? throw new ArgumentException("Exercicio não encontrado.");
        exercicio.Nome = ex.Nome;
        await _context.SaveChangesAsync();
    }
}
