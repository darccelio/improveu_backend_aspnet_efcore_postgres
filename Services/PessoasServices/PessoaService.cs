using AutoMapper;
using ImproveU_backend.DatabaseConfiguration.Configuration;
using ImproveU_backend.Models;
using ImproveU_backend.Models.Dtos.PessoaDto;
using ImproveU_backend.Services.Interfaces.IPessoaSerivce;
using Microsoft.EntityFrameworkCore;

namespace ImproveU_backend.Services.PessoasServices;

public class PessoaService : IPessoaService
{

    private readonly ImproveuContext _context;
    private readonly IMapper _mapper;

    public PessoaService(ImproveuContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<PessoaResponseDto> CriarAsync(PessoaCreateRequestDto pessoaRequest)
    {
        Pessoa? pessoa = await _context.Pessoas.FirstOrDefaultAsync(p => p.Cpf == pessoaRequest.Cpf && p.IdentityUserId == pessoaRequest.IdentityUserId);

        if (pessoa != null)
        {
            throw new ArgumentException("Pessoa já cadastrada.");
        }

        pessoa = _mapper.Map<Pessoa>(pessoaRequest);

        if (pessoa is null)
        {
            throw new ArgumentException("Erro de validação.");
        }

        _context.Pessoas.Add(pessoa);

        await _context.SaveChangesAsync();

        PessoaResponseDto pessoaDtoResp = _mapper.Map<PessoaResponseDto>(pessoa);
        return pessoaDtoResp;
    }

    public async Task<PessoaResponseDto> BuscarPorCpfAsync(string cpf)
    {
        if (string.IsNullOrWhiteSpace(cpf))
        {
            throw new ArgumentException("Cpf não pode ser nulo ou vazio.");
        }

        Pessoa? pessoa = await _context.Pessoas.FirstOrDefaultAsync(p => p.Cpf == cpf);

        if (pessoa == null)
        {
            return null;
        }
        PessoaResponseDto pessoaDtoResp = _mapper.Map<PessoaResponseDto>(pessoa);
        return pessoaDtoResp;
    }
}
