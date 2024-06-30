using AutoMapper;
using ImproveU_backend.DatabaseConfiguration.Configuration;
using ImproveU_backend.Models;
using ImproveU_backend.Models.Dtos.PessoaDto;
using ImproveU_backend.Services.Interfaces.IPessoaServices;
using ImproveU_backend.Services.Interfaces.IUsuarioServices;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace ImproveU_backend.Services.PessoaServices;
public class AlunoService : IAlunoService
{

    private readonly ImproveuContext _context;
    private readonly IPessoaService _pessoaService;
    private readonly IMapper _mapper;
    private readonly IUser _user;

    public AlunoService(ImproveuContext context, IPessoaService pessoaService, IMapper mapper, IUser user)
    {
        _context = context;
        _pessoaService = pessoaService;
        _mapper = mapper;
        _user = user;
    }

    public async Task<AlunoResponseDto> CriarAsync(AlunoCreateRequestDto alunoRequest)
    {
        if (alunoRequest is null)
            throw new ArgumentNullException(nameof(alunoRequest), "O objeto AlunoCreateRequestDto não pode ser nulo.");

        PessoaResponseDto pessoaResponseDto = await _pessoaService.BuscarPorCpfAsync(alunoRequest.PessoaCreateRequest.Cpf);
        if(pessoaResponseDto is null)
            pessoaResponseDto = await _pessoaService.CriarAsync(alunoRequest.PessoaCreateRequest, new Tuple<string, string>("aluno", "criar"));

        Aluno novoAluno = new Aluno() { PessoaId = pessoaResponseDto.Id };

        await _context.Alunos.AddAsync(novoAluno);
        await _context.SaveChangesAsync();

        AlunoResponseDto alunoRespDto = _mapper.Map<AlunoResponseDto>(novoAluno);
        return alunoRespDto;
    }

    public async Task<IEnumerable<AlunoResponseDto>> BuscarAsync(int skip, int take)
    {
        List<Aluno> alunos = await _context.Alunos.Include(aluno => aluno.Pessoa).AsNoTracking().Skip(skip).Take(take).ToListAsync();

        List<AlunoResponseDto> alunoRespDto = _mapper.Map<List<AlunoResponseDto>>(alunos);        
        return alunoRespDto;
    }

    public async Task<AlunoResponseDto> BuscarPorIdAsync(int id)
    {
        Aluno? aluno = await _context.Alunos.FirstOrDefaultAsync(al => al.Id == id);

        if (aluno is null)
            return null;

        AlunoResponseDto alunoRespDto = _mapper.Map<AlunoResponseDto>(aluno);
        //return new AlunoResponseDto(aluno);
        return alunoRespDto;
    }

    public async Task AtualizarAsync(int id, AlunoUpdateRequestDto alunoRequest)
    {
        if (alunoRequest is null)
            throw new ArgumentNullException(nameof(alunoRequest), "O objeto AlunoUpdateRequestDto não pode ser nulo.");

        if (id != alunoRequest.Id)
            throw new ArgumentException("Id do objeto é diferente do id da rota.");

        Aluno? aluno = await _context.Alunos.Include(aluno => aluno.Pessoa).Where(predicate: al => al.Id == al.PessoaId && al.Id == id).FirstOrDefaultAsync();

        if (aluno is null)
            throw new ArgumentException("Não foi possível alterar pois o Aluno não foi encontrado.");

        aluno.Pessoa.Nome = alunoRequest.Nome;
        aluno.Pessoa.Cpf = alunoRequest.Cpf;

        _context.Alunos.Update(aluno);
        _context.Entry(aluno.Pessoa).State = EntityState.Modified;

        await _context.SaveChangesAsync();
    }

    public async Task RemoverPorIdAsync(int id)
    {
        Aluno? aluno = await _context.Alunos.Include(p => p.Pessoa).FirstOrDefaultAsync(p => p.Id == id && p.PessoaId == p.Pessoa.Id);

        if (aluno is null)
            throw new ArgumentException("Aluno não encontrado.");

        _context.Pessoas.Remove(aluno.Pessoa);

        _context.Alunos.Remove(aluno);
        await _context.SaveChangesAsync();
    }
}
