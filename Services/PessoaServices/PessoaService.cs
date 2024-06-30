using AutoMapper;
using ImproveU_backend.DatabaseConfiguration.Configuration;
using ImproveU_backend.Models;
using ImproveU_backend.Models.Dtos.PessoaDto;
using ImproveU_backend.Services.Interfaces.IPessoaServices;
using ImproveU_backend.Services.Interfaces.IUsuarioServices;
using Microsoft.EntityFrameworkCore;

namespace ImproveU_backend.Services.PessoasServices;

public class PessoaService : IPessoaService
{

    private readonly ImproveuContext _context;
    private readonly IMapper _mapper;
    private readonly IUser _user;

    public PessoaService(ImproveuContext context, IMapper mapper, IUser user)
    {
        _context = context;
        _mapper = mapper;
        _user = user;
    }

    private bool VerificaSePossuiClaims(string tipo, string operacao)
    {
        IEnumerable<System.Security.Claims.Claim> claims = _user.GetClaimsIdentity();
        System.Security.Claims.Claim? claim = claims.FirstOrDefault(c => c.Type == "tipo" && c.ValueType.Contains(operacao));

        if (claim is not null) return true;
        return false;
        //return claims.Contains(new System.Security.Claims.Claim(tipo, operacao));
    }

    

    private Guid CompararUserEmail(string email)
    {
        if (!string.IsNullOrWhiteSpace(email) &&
            (email != _user.GetUserEmail()))
        {
            throw new ArgumentException("E-mail do cadastro da pessoa nao corresponde ao username do usuário.");
        }
        return _user.GetUserId();
    }


    public async Task<PessoaResponseDto> CriarAsync(PessoaCreateRequestDto pessoaRequest,
                                                    Tuple<string, string> claim)
    {
        if(string.IsNullOrWhiteSpace(pessoaRequest.EmailUsuario))
            throw new ArgumentException("Email da pessoa não pode ser nulo.");

        Guid identityUserId = CompararUserEmail(pessoaRequest.EmailUsuario);

        if(VerificaSePossuiClaims(claim.Item1, claim.Item2))
            throw new ArgumentException($"Usuário não possui perfil para criar {claim.Item1}.");

        Pessoa? pessoa = await _context.Pessoas.FirstOrDefaultAsync(p => p.Cpf == pessoaRequest.Cpf);        
        if (pessoa != null)
        {
            throw new ArgumentException("Pessoa já cadastrada.");
        }

        pessoa = new Pessoa
        {
            Cpf = pessoaRequest.Cpf,
            Nome = pessoaRequest.Nome,
            IdentityUserId = identityUserId.ToString()
        };

        await _context.Pessoas.AddAsync(pessoa);
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
