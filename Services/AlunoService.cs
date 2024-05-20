using ImproveU_backend.DatabaseConfiguration.Context;
using ImproveU_backend.Models;
using ImproveU_backend.Models.Dtos;
using ImproveU_backend.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ImproveU_backend.Services;

public class AlunoService : IAlunoService
{

    private readonly ImproveuContext _context;
    private readonly IPessoaService _pessoaService;



    public AlunoService(ImproveuContext context, IPessoaService pessoaService)
    {
        _context = context;
        _pessoaService = pessoaService;
    }

    public async Task<AlunoResponseDto> CriarAsync(AlunoCreateRequestDto req)
    {

        if (req is null)
            throw new ArgumentNullException(nameof(req), "O objeto EdFisicoRequestDto não pode ser nulo.");

        PessoaResponseDto? pessoaDto = await _pessoaService.BuscarPorCpfAsync(req.Pessoa.Cpf);

        if (pessoaDto is not null)
            throw new ArgumentException("Pessoa já cadastrada.");

        Pessoa pessoa = new Pessoa(req.Pessoa.Cpf,
                req.Pessoa.Nome,
                req.Pessoa.UsuarioId);

        _context.Pessoas.Add(pessoa);
        await _context.SaveChangesAsync();

        Aluno aluno = new Aluno()
        {
            Pessoa = pessoa,
            PessoaId = pessoa.Id
        };

        await _context.Alunos.AddAsync(aluno);
        await _context.SaveChangesAsync();
        AlunoResponseDto edFisicoResponseDto = new AlunoResponseDto(aluno);
        return edFisicoResponseDto;
    }


   

    public async Task<IEnumerable<AlunoResponseDto>> BuscarAsync(int skip, int take)
    {
        throw new NotImplementedException();
    }

    public async Task<AlunoResponseDto> BuscarPorIdAsync(int id)
    {
        Aluno? aluno = await _context.Alunos.FirstOrDefaultAsync(al => al.Id == id);

        if (aluno is null)
            return null;

        return new AlunoResponseDto(aluno);
    }

    public Task AtualizarAsync(int id, AlunoUpdateRequestDto alunoRequest)
    {
        throw new NotImplementedException();
    }

    public Task InativarPorIdAsync(int id)
    {
        throw new NotImplementedException();
    }
}
