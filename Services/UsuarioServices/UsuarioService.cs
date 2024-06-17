using AutoMapper;
using ImproveU_backend.DatabaseConfiguration.Configuration;
using ImproveU_backend.Models;
using ImproveU_backend.Models.Dtos.UsuarioDto;
using ImproveU_backend.Services.Interfaces.IUsuarioServices;
using Microsoft.EntityFrameworkCore;

namespace ImproveU_backend.Services.UsuarioService;

public class UsuarioService : IUsuarioServices
{
    private readonly ImproveuContext _context;
    private readonly IMapper _mapper;

    public UsuarioService(ImproveuContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<UsuarioResponseDto> CriarAsync(UsuarioCreateRequestDto usuarioRequest)
    {
        Usuario? usuario = await _context.Usuarios.AsNoTracking().FirstOrDefaultAsync(u => u.Email == usuarioRequest.Email);

        if (usuario != null)
        {
            throw new ArgumentException("Usuário já cadastrado.");
        }

        Usuario novoUsuario = new Usuario(usuarioRequest.Email, usuarioRequest.Papel, usuarioRequest.Senha);

        _context.Usuarios.Add(novoUsuario);
        _context.SaveChanges();

        return _mapper.Map<UsuarioResponseDto>(usuario);
    }


    public async Task<IEnumerable<UsuarioResponseDto>> BuscarAsync(int skip, int take)
    {
        List<Usuario> usuarios = await _context.Usuarios.AsNoTracking().Skip(skip).Take(take).ToListAsync();

        return _mapper.Map<IEnumerable<UsuarioResponseDto>>(usuarios);
    }

    public async Task<UsuarioResponseDto> BuscarPorIdAsync(int id)
    {
        var usuario = await GetUsuarioPorIdAsync(id);
        return _mapper.Map<UsuarioResponseDto>(usuario);
    }

    private async Task<Usuario> GetUsuarioPorIdAsync(int id)
    {
        try
        {
            Usuario? usuario = await _context.Usuarios.FindAsync(id);
            return usuario;
        }
        catch (IOException e)
        {
            throw new IOException(e.Message);
        }
    }

    public async Task<UsuarioResponseDto> BuscarPorEmailAsync(string email)
    {
        Usuario? usuario = await _context.Usuarios.FirstOrDefaultAsync(u => u.Email == email);
        if (usuario == null) throw new ArgumentException("Usuário não encontrado.");
        return _mapper.Map<UsuarioResponseDto>(usuario);
    }

    public async Task<bool> AtualizarAsync(int id, UsuarioUpdateRequestDto usuarioRequest)
    {
        try
        {
            Usuario? usuarioRecuperado = await GetUsuarioPorIdAsync(id);

            if (usuarioRecuperado is Usuario usuario2)
            {
                usuarioRecuperado.Email = usuarioRequest.email;
                usuarioRecuperado.Papel = (int)usuarioRequest.papel;
                usuarioRecuperado.Senha = usuarioRequest.senha;
                usuarioRecuperado.AtualizaUltimaAlteracao(DateTime.Now);

                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }
        catch (IOException e)
        {
            throw new IOException(e.Message);
        }
    }

    public async Task InativarPorIdAsync(int id)
    {
        Usuario? usuario = await GetUsuarioPorIdAsync(id);

        if (usuario == null)
        {
            throw new KeyNotFoundException("Usuário não encontrado.");
        }

        usuario.Ativo = 0;
        usuario.AtualizaUltimaAlteracao(DateTime.Now);
        _context.Entry(usuario).State = EntityState.Modified;
        _context.Update(usuario);
        await _context.SaveChangesAsync();
    }



}