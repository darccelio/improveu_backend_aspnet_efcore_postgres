//using ImproveU_backend.Models.Dtos.UsuarioDto;
//using ImproveU_backend.Services.Interfaces.IUsuarioService;
//using Microsoft.AspNetCore.Authorization;
//using Microsoft.AspNetCore.Mvc;

//namespace ImproveU_backend.Controllers;

//[Authorize]
//[ApiController]
//[Route("api/usuarios")]
//public class UsuarioController : ControllerBase
//{
//    private readonly IUsuarioService _usuarioService;

//    public UsuarioController(IUsuarioService usuarioService)
//    {
//        _usuarioService = usuarioService;
//    }


//    [HttpPost]
//    [ProducesResponseType(typeof(UsuarioResponseDto), StatusCodes.Status201Created)]
//    [ProducesResponseType(StatusCodes.Status400BadRequest)]
//    public async Task<IActionResult> Criar([FromBody] UsuarioCreateRequestDto usuarioRequestDto)
//    {
//        var usuario = await _usuarioService.CriarAsync(usuarioRequestDto);
//        return CreatedAtAction(nameof(UsuarioController.BuscarPorId),
//            new { id = usuario.Id },
//            usuario);
//    }

//    [HttpGet]
//    [ProducesResponseType(typeof(UsuarioResponseDto), StatusCodes.Status200OK)]
//    public async Task<IActionResult> Buscar(
//        [FromQuery] int skip = 0,
//        [FromQuery] int take = 10,
//        [FromQuery] string? email = null
//        )
//    {
//        if (email != null)
//        {
//            var usuario = await _usuarioService.BuscarPorEmailAsync(email);
//            if (usuario == null)
//            {
//                return NotFound();
//            }
//            return Ok(usuario);
//        }

//        var usuarios = await _usuarioService.BuscarAsync(skip, take);

//        return Ok(usuarios);
//    }

//    [HttpGet("{id}")]
//    [ProducesResponseType(typeof(UsuarioResponseDto), StatusCodes.Status200OK)]
//    [ProducesResponseType(StatusCodes.Status404NotFound)]
//    public async Task<IActionResult> BuscarPorId(int id)
//    {
//        if (id == null || id == 0)
//            return BadRequest();

//        var usuario = await _usuarioService.BuscarPorIdAsync(id);
//        if (usuario?.Id == null)
//        {
//            return NotFound();
//        }
//        return Ok(usuario);
//    }     
//}