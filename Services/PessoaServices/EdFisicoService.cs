using AutoMapper;
using ImproveU_backend.DatabaseConfiguration.Configuration;
using ImproveU_backend.Models;
using ImproveU_backend.Models.Dtos.PessoaDto;
using ImproveU_backend.Models.Dtos.UsuarioDto;
using ImproveU_backend.Services.Interfaces.IPessoaServices;
using ImproveU_backend.Services.Interfaces.IUsuarioServices;
using Microsoft.EntityFrameworkCore;

namespace ImproveU_backend.Services.PessoasServices;

public class EdFisicoService : IEdFisicoService
{
    private readonly ImproveuContext _context;
    private readonly IPessoaService _pessoaService;
    private readonly IMapper _mapper;
    private readonly IUser _user;

    public EdFisicoService(ImproveuContext context, IPessoaService pessoaService, IMapper mapper, IUser user)
    {
        _context = context;
        _pessoaService = pessoaService;
        _mapper = mapper;
        _user = user;
    }

    public async Task<EdFisicoResponseDto> CriarAsync(EdFisicoCreateRequestDto edFisicoRequest)
    {
        if (edFisicoRequest is null)
            throw new ArgumentNullException(nameof(edFisicoRequest), "O objeto EdFisicoRequestDto não pode ser nulo.");

        EdFisico? edFisico = await _context.EdFisicos
                                           .FirstOrDefaultAsync(ef => ef.RegistroConselho == edFisicoRequest.RegistroConselho ||
                                                                      ef.Pessoa.Cpf == edFisicoRequest.PessoaCreateRequestDto.Cpf);

        if (edFisico != null)
            throw new ArgumentException("Educador físico já cadastrado.", nameof(edFisicoRequest));

        PessoaResponseDto pessoaResponseDto = await _pessoaService.CriarAsync(edFisicoRequest.PessoaCreateRequestDto, new Tuple<string, string>("educador", "criar"));

        EdFisico novoEdFisico = new EdFisico
        {
            RegistroConselho = edFisicoRequest.RegistroConselho,
            PessoaId = pessoaResponseDto.Id
        };

        await _context.EdFisicos.AddAsync(novoEdFisico);
        await _context.SaveChangesAsync();
        
        EdFisicoResponseDto edFisicoResponseDto = _mapper.Map<EdFisicoResponseDto>(novoEdFisico);
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