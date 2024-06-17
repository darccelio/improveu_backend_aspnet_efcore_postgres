using ImproveU_backend.Extensions;
using ImproveU_backend.Models.Dtos.PessoaDto;
using ImproveU_backend.Services.Interfaces.IPessoaServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ImproveU_backend.Controllers;

[Authorize]
[ApiController]
[Route("api/educadorfisico")]
public class EdFisicoController : ControllerBase
{
    private readonly IEdFisicoService _edFisicoService;

    public EdFisicoController(IEdFisicoService edFisicoService)
    {
        _edFisicoService = edFisicoService;
    }

    [ClaimsAuthorize("educador", "criar")]
    [HttpPost]
    [ProducesResponseType(typeof(EdFisicoResponseDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Criar([FromBody] EdFisicoCreateRequestDto edFisicoRequest)
    {
        if (edFisicoRequest == null)
            return BadRequest();
        try
        {
            EdFisicoResponseDto edFisicoResp = await _edFisicoService.CriarAsync(edFisicoRequest);
            return CreatedAtAction(nameof(EdFisicoController.BuscarPorId), new { id = edFisicoResp.Id }, edFisicoResp);
        }
        catch (ArgumentException e)
        {
            return BadRequest(e.Message);
        }
    }

    [ClaimsAuthorize("admin", "ler")]
    [HttpGet]
    [ProducesResponseType(typeof(EdFisicoResponseDto), StatusCodes.Status200OK)]
    public async Task<IActionResult> Buscar(
        [FromQuery] int skip = 0,
        [FromQuery] int take = 10)
    {
        return Ok(await _edFisicoService.BuscarAsync(skip, take));
    }

    [ClaimsAuthorize("admin", "ler")]
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(EdFisicoResponseDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> BuscarPorId(int id)
    {
        EdFisicoResponseDto edFisico = await _edFisicoService.BuscarPorIdAsync(id);
        if (edFisico?.Id == null)
            return NotFound();

        return Ok(edFisico);
    }
}
