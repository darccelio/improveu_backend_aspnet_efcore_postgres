using ImproveU_backend.Extensions;
using ImproveU_backend.Models.Dtos.UsuarioDto;
using ImproveU_backend.Services.Interfaces.INotificacoesServices;
using ImproveU_backend.Services.Interfaces.IUsuarioServices;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;


namespace ImproveU_backend.Controllers;

[Route("api/conta")]
public class AutenticacaoController : MainController
{

    private readonly SignInManager<IdentityUser> _signInManager;
    private readonly UserManager<IdentityUser> _userManager;
    private readonly AppSettings _appSettings;

    public AutenticacaoController(INotificadorService notificador,
                                  SignInManager<IdentityUser> signInManager,
                                  UserManager<IdentityUser> userManager,
                                  IOptions<AppSettings> appSettings,
                                  IUser user
                                  ) : base(notificador, user)
    {
        _signInManager = signInManager;
        _userManager = userManager;
        _appSettings = appSettings.Value;
    }

    [HttpPost("registrar")]
    [ProducesResponseType(typeof(UsuarioResponseDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Registrar([FromBody] UsuarioCreateRequestDto usuarioRequestDto)
    {
        try
        {
            var usuario = new IdentityUser
            {
                UserName = usuarioRequestDto.Email,
                Email = usuarioRequestDto.Email,
                EmailConfirmed = true //estrategia para confirmação da criação de conta através do email. Valor true demonstra que a confirmação já foi realizada
            };

            var result = await _userManager.CreateAsync(usuario, usuarioRequestDto.Senha);

            if (result.Succeeded)
            {
                if (usuarioRequestDto.Papel == 0)
                {
                    await _userManager.AddClaimAsync(usuario, new Claim("educador", "criar, ler, editar"));
                }
                else
                {
                    await _userManager.AddClaimAsync(usuario, new Claim("aluno", "criar, ler"));
                    //await _userManager.AddToRoleAsync(usuario, "aluno");
                }

                await _signInManager.SignInAsync(usuario, false);
                return CustomResponse(await GerarJwt(usuario.Email));

            }

            foreach (var error in result.Errors)
            {
                NotificarErro(error.Description);
            }

            return CustomResponse(usuarioRequestDto);

        }
        catch (Exception ex)
        {
            return BadRequest(new { message = $"Erro ao criar usuário {ex.InnerException?.Message ?? ex.Message}" });
        }
    }



    [HttpPost("autenticar")]
    [ProducesResponseType(typeof(UsuarioResponseDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Autenticar([FromBody] UsuarioLoginRequestDto usuarioRequestDto)
    {
        var result = await _signInManager.PasswordSignInAsync(usuarioRequestDto.Email,
                                                              usuarioRequestDto.Senha,
                                                              false,
                                                              true);
        if (result.Succeeded)
        {
            return CustomResponse(await GerarJwt(usuarioRequestDto.Email));

        }
        if (result.IsLockedOut)
        {
            NotificarErro("Usuário temporariamente bloqueado por tentativas inválidas");
            return CustomResponse(usuarioRequestDto);
        }

        NotificarErro("Usuário ou Senha incorretos");
        return CustomResponse(usuarioRequestDto);
    }

    private async Task<UsuarioLoginResponseDto> GerarJwt(string email)
    {
        var user = await _userManager.FindByEmailAsync(email);
        var claims = await _userManager.GetClaimsAsync(user);
        var userRoles = await _userManager.GetRolesAsync(user);

        //colecao completas que serão inseridas no token
        claims.Add(new Claim(JwtRegisteredClaimNames.Sub, user.Id));
        claims.Add(new Claim(JwtRegisteredClaimNames.Email, user.Email));
        claims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));
        claims.Add(new Claim(JwtRegisteredClaimNames.Nbf, ToUnixEpochDate(DateTime.UtcNow).ToString())); //não valido antes da data (nbf -> not valid before)
        claims.Add(new Claim(JwtRegisteredClaimNames.Iat, ToUnixEpochDate(DateTime.UtcNow).ToString(), ClaimValueTypes.Integer64)); //quando foi gerado

        foreach (var userRole in userRoles)
        {
            claims.Add(new Claim("role", userRole));
        }

        var identityClaims = new ClaimsIdentity();
        identityClaims.AddClaims(claims);

        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
        var token = tokenHandler.CreateToken(new SecurityTokenDescriptor
        {
            Issuer = _appSettings.Emissor,
            Audience = _appSettings.ValidoEm,
            Subject = identityClaims, //colecao de clains sendo inseridas no token
            Expires = DateTime.UtcNow.AddHours(_appSettings.ExpiracaoHoras),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)

        });
        var encodedToken = tokenHandler.WriteToken(token);

        var response = new UsuarioLoginResponseDto
        {
            AccessToken = encodedToken,
            ExpiresIn = TimeSpan.FromHours(_appSettings.ExpiracaoHoras).TotalSeconds,
            UserToken = new UsuarioTokenResponseDto
            {
                Id = user.Id,
                Email = user.Email,
                Claims = claims.Select(c => new ClaimResponseDto { Type = c.Type, Value = c.Value })
            }
        };
        return response;
    }

    private static long ToUnixEpochDate(DateTime date)
        => (long)Math.Round((date.ToUniversalTime() - new DateTimeOffset(1970, 1, 1, 0, 0, 0, TimeSpan.Zero)).TotalSeconds);

}

