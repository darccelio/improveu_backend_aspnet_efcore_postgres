using ImproveU_backend.DatabaseConfiguration.Context;
using ImproveU_backend.Models;
using ImproveU_backend.Models.Dtos;
using ImproveU_backend.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ImproveU_backend.Services;

public class EdFisicoService : IEdFisicoService
{
    private readonly ImproveuContext _context;
    private readonly IPessoaService _pessoaService;

    public EdFisicoService(ImproveuContext context, IPessoaService pessoaService)
    {
        _context = context;
        _pessoaService = pessoaService;
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
        EdFisicoResponseDto edFisicoResponseDto = new EdFisicoResponseDto(novoEdFisico);
        return edFisicoResponseDto;
    }

    public async Task<IEnumerable<EdFisicoResponseDto>> BuscarAsync(int skip, int take)
    {
        List<EdFisico> edFisicos = await _context.EdFisicos.Include(edFisicos => edFisicos.Pessoa).AsNoTracking().Skip(skip).Take(take).ToListAsync();

        return edFisicos.Select(ef => new EdFisicoResponseDto(ef));
    }

    public async Task<EdFisicoResponseDto> BuscarPorIdAsync(int id)
    {
        EdFisico? edFisico = await _context.EdFisicos.Include(p => p.Pessoa).FirstOrDefaultAsync(ef => ef.Id == id);
        if (edFisico is null)
            return null;

        return new EdFisicoResponseDto(edFisico);
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