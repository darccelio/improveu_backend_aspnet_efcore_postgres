//using AutoMapper;
//using ImproveU_backend.DatabaseConfiguration.Configuration;
//using ImproveU_backend.Models;
//using ImproveU_backend.Models.Dtos.UsuarioDto;
//using ImproveU_backend.Services.Interfaces.IUsuarioService;
//using Microsoft.EntityFrameworkCore;

//namespace ImproveU_backend.Services.UsuarioService;

//public class UsuarioService : IUsuarioService
//{
//    private readonly ImproveuContext _context;
//    private readonly IMapper _mapper;

//    public UsuarioService(ImproveuContext context, IMapper mapper)
//    {
//        _context = context;
//        _mapper = mapper;
//    }

//    public async Task<UsuarioResponseDto> CriarAsync(UsuarioCreateRequestDto usuarioRequest)
//    {
//        Usuario? usuario = await _context.Usuarios.AsNoTracking().FirstOrDefaultAsync(u => u.Email == usuarioRequest.Email);

//        if (usuario != null)
//        {
//            throw new ArgumentException("Usuário já cadastrado.");
//        }

//        Usuario novoUsuario = _mapper.Map<Usuario>(usuarioRequest);
//        _context.Usuarios.Add(novoUsuario);
//        _context.SaveChanges();

//        UsuarioResponseDto usuarioResponseDto = _mapper.Map<UsuarioResponseDto>(novoUsuario);
//        return usuarioResponseDto;
//    }


//    public async Task<IEnumerable<UsuarioResponseDto>> BuscarAsync(int skip, int take)
//    {
//        List<Usuario> usuarios = await _context.Usuarios.AsNoTracking().Skip(skip).Take(take).ToListAsync();
//        IEnumerable<UsuarioResponseDto> usuariosRespDto = _mapper.Map<IEnumerable<UsuarioResponseDto>>(usuarios);
//        //usuarios.Select(u => new UsuarioResponseDto(u));
//        return usuariosRespDto;
//    }

//    public async Task<UsuarioResponseDto> BuscarPorIdAsync(int id)
//    {
//        var usuario = await GetUsuarioPorIdAsync(id);
//        //return new UsuarioResponseDto(usuario);
//        return _mapper.Map<UsuarioResponseDto>(usuario);
//    }

//    private async Task<Usuario> GetUsuarioPorIdAsync(int id)
//    {
//        try
//        {
//            Usuario? usuario = await _context.Usuarios.FindAsync(id);
//            return usuario;
//        }
//        catch (IOException e)
//        {
//            throw new IOException(e.Message);
//        }
//    }

//    public async Task<UsuarioResponseDto> BuscarPorEmailAsync(string email)
//    {
//        Usuario? usuario = await _context.Usuarios.FirstOrDefaultAsync(u => u.Email == email);
//        if (usuario == null) throw new ArgumentException("Usuário não encontrado.");
//        return _mapper.Map<UsuarioResponseDto>(usuario);
//    }

//}