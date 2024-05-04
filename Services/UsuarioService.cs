using ImproveU_backend.Models.Dtos;
using ImproveU_backend.Models;
using ImproveU_backend.Services.Interfaces;
using ImproveU_backend.DatabaseConfiguration.Context;
using Microsoft.EntityFrameworkCore;

namespace ImproveU_backend.Services;

public class UsuarioService : IUsuarioService
{
    private readonly ImproveuContext _context;

    public UsuarioService(ImproveuContext context)
    {
        _context = context;
    }

    private async Task<Usuario> GetUsuarioPorIdAsync(int id)
    {
        Usuario? usuario = await _context.Usuarios.FindAsync(id);
        return usuario;
    }

    public async Task<UsuarioResponseDto> BuscarUsuarioPorIdAsync(int id)
    {

        var usuario = await GetUsuarioPorIdAsync(id);
        return new UsuarioResponseDto(usuario);


    }

    public async Task<UsuarioResponseDto> BuscarUsuarioPorEmailAsync(string email)
    {
        Usuario? usuario = await _context.Usuarios.FirstOrDefaultAsync(u => u.Email == email);

        if (usuario == null) throw new Exception("Usuário não encontrado.");

        return new UsuarioResponseDto(usuario);
    }

    public async Task<IEnumerable<UsuarioResponseDto>> BuscarUsuariosAsync(int skip, int take)
    {
        List<Usuario> usuarios = await _context.Usuarios.AsNoTracking().Skip(skip).Take(take).ToListAsync();

        return usuarios.Select(u => new UsuarioResponseDto(u));
    }


    public async Task AtualizarUsuarioAsync(int id, UsuarioUpdateRequest usuarioRequest)
    {
        try
        {
            Usuario? usuario = await GetUsuarioPorIdAsync(id);

            if (usuarioRequest.email != null) usuario.Email = usuarioRequest.email;
            if (usuarioRequest.papel != null) usuario.Papel = (int)usuarioRequest.papel;
            if (usuarioRequest.senha != null) usuario.Senha = usuarioRequest.senha;

            usuario.UltimaAlteracao = DateTime.Now;

            _context.Usuarios.Update(usuario);
            _context.SaveChanges();
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }

    public async Task DeletarUsuarioPorIdAsync(int id)
    {
        try
        {
            Usuario? usuario = await GetUsuarioPorIdAsync(id);
            usuario.Ativo = 0;

            usuario.UltimaAlteracao = DateTime.Now;

            //_context.Usuarios.Update(usuario);
            _context.SaveChanges();
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }


    public async Task<UsuarioResponseDto> CriarUsuarioAsync(UsuarioCreateRequestDto usuarioRequest)
    {
        Usuario? usuario = await _context.Usuarios.AsNoTracking().FirstOrDefaultAsync(u => u.Email == usuarioRequest.Email);

        if (usuario != null)
        {
            throw new Exception("Usuário já cadastrado.");
        }

        Usuario novoUsuario = new Usuario(usuarioRequest.Email, usuarioRequest.Papel, usuarioRequest.Senha);

        _context.Usuarios.Add(novoUsuario);
        _context.SaveChanges();

        UsuarioResponseDto usuarioResponseDto = new UsuarioResponseDto(novoUsuario);
        return usuarioResponseDto;
    }
}