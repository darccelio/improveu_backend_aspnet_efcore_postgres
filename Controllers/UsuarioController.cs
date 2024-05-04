using ImproveU_backend.Models;
using ImproveU_backend.Models.Dtos;
using ImproveU_backend.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ImproveU_backend.Controllers;


[ApiController]
[Route("api/usuarios")]
public class UsuarioController : ControllerBase
{

    private readonly IUsuarioService _usuarioService;


    public UsuarioController(IUsuarioService usuarioService)
    {
        _usuarioService = usuarioService;
    }

    [HttpGet]
    [ProducesResponseType(typeof(Usuario), StatusCodes.Status200OK)]
    public async Task<IActionResult> Buscar(
        [FromQuery] int skip = 0,
        [FromQuery] int take = 10,
        [FromQuery] string? email = null
        )
    {

        if (email != null)
        {
            var usuario = await _usuarioService.BuscarUsuarioPorEmailAsync(email);
            if (usuario == null)
            {
                return NotFound();
            }
            return Ok(usuario);
        }

        var usuarios = await _usuarioService.BuscarUsuariosAsync(skip, take);

        return Ok(usuarios);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(typeof(Usuario), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> BuscarPorId(int id)
    {

        var usuario = await _usuarioService.BuscarUsuarioPorIdAsync(id);
        if (usuario?.Id == null )
        {
            return NotFound();
        }
        return Ok(usuario);
    }

    [HttpPost]
    [ProducesResponseType(typeof(Usuario), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Criar([FromBody] UsuarioCreateRequestDto usuarioRequestDto)
    {
        var usuario = await _usuarioService.CriarUsuarioAsync(usuarioRequestDto);
        return CreatedAtAction(nameof(Buscar),
            new { email = usuario.Email },
            usuario);
    }


    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    // public IActionResult CadastraUsuario([FromBody] CriarUsuarioRequestDto usuarioRequestDto)
    public async Task<IActionResult> Atualizar(int id,
        [FromBody] UsuarioUpdateRequestDto atualizarDto)
    {
        await _usuarioService.AtualizarUsuarioAsync(id, atualizarDto);
        return NoContent();
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Deletar(int id)
    {
        var usuario = await _usuarioService.BuscarUsuarioPorIdAsync(id);
        if (usuario == null)
        {
            return NotFound();
        }
        await _usuarioService.DeletarUsuarioPorIdAsync(id);
        return NoContent();
    }
}