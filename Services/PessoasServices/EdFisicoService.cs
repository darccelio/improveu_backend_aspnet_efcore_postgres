using AutoMapper;
using ImproveU_backend.DatabaseConfiguration.Configuration;
using ImproveU_backend.Models;
using ImproveU_backend.Models.Dtos.PessoaDto;
using ImproveU_backend.Models.Dtos.UsuarioDto;
using ImproveU_backend.Services.Interfaces.IPessoaSerivce;
using Microsoft.EntityFrameworkCore;

namespace ImproveU_backend.Services.PessoasServices;

public class EdFisicoService : IEdFisicoService
{
    private readonly ImproveuContext _context;
    private readonly IPessoaService _pessoaService;
    private readonly IMapper _mapper;

    public EdFisicoService(ImproveuContext context, IPessoaService pessoaService, IMapper mapper)
    {
        _context = context;
        _pessoaService = pessoaService;
        _mapper = mapper;
    }

    public async Task<EdFisicoResponseDto> CriarAsync(EdFisicoCreateRequestDto edFisicoRequest)
    {
        if (edFisicoRequest is null)
            throw new ArgumentNullException(nameof(edFisicoRequest), "O objeto EdFisicoRequestDto não pode ser nulo.");

        EdFisico? edFisico = await _context.EdFisicos.FirstOrDefaultAsync(ef => ef.RegistroConselho == edFisicoRequest.RegistroConselho || ef.Pessoa.Cpf == edFisicoRequest.Pessoa.Cpf);

        if (edFisico != null)
            throw new ArgumentException("Educador físico já cadastrado.", nameof(edFisicoRequest));

        PessoaResponseDto? pessoaDto = await _pessoaService.BuscarPorCpfAsync(edFisicoRequest.Pessoa.Cpf);

        if (pessoaDto is not null)
            throw new ArgumentException("Pessoa já cadastrada.");

        Pessoa pessoa = new Pessoa(edFisicoRequest.Pessoa.Cpf,
                edFisicoRequest.Pessoa.Nome,
                edFisicoRequest.Pessoa.UsuarioId);

        _context.Pessoas.Add(pessoa);
        await _context.SaveChangesAsync();

        EdFisico novoEdFisico = new EdFisico(edFisicoRequest.RegistroConselho, pessoa);

        await _context.EdFisicos.AddAsync(novoEdFisico);
        await _context.SaveChangesAsync();
        //EdFisicoResponseDto edFisicoResponseDto = new EdFisicoResponseDto(novoEdFisico);
        EdFisicoResponseDto edFisicoResponseDto = _mapper.Map<EdFisicoResponseDto>(edFisico);
        return edFisicoResponseDto;
    }

    public async Task<IEnumerable<EdFisicoResponseDto>> BuscarAsync(int skip, int take)
    {
        List<EdFisico> edFisicos = await _context.EdFisicos.Include(edFisicos => edFisicos.Pessoa).AsNoTracking().Skip(skip).Take(take).ToListAsync();

        List<EdFisicoResponseDto> edFisicoResponseDtos = _mapper.Map<List<EdFisicoResponseDto>>(edFisicos);
        //return edFisicos.Select(ef => new EdFisicoResponseDto(ef));
        return edFisicoResponseDtos;
    }

    public async Task<EdFisicoResponseDto> BuscarPorIdAsync(int id)
    {
        EdFisico? edFisico = await _context.EdFisicos.Include(p => p.Pessoa).FirstOrDefaultAsync(ef => ef.Id == id);
        if (edFisico is null)
            return null;

        EdFisicoResponseDto edFisicoResponseDto = _mapper.Map<EdFisicoResponseDto>(edFisico);

        return edFisicoResponseDto;
    }

    public Task AtualizarAsync(int id, UsuarioUpdateRequestDto edFisicoRequest)
    {
        throw new NotImplementedException();
    }

    public Task DeletarPorIdAsync(int id)
    {
        throw new NotImplementedException();
    }
}