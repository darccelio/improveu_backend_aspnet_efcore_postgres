using ImproveU_backend.Models;
using ImproveU_backend.Models.Dtos.PessoaDto;
using ImproveU_backend.Models.Dtos.TreinoDto;
using ImproveU_backend.Services.Interfaces.ITreino;
using Microsoft.AspNetCore.Mvc;

namespace ImproveU_backend.Controllers;

[ApiController]
[Route("api/treinos")]
public class TreinoController : ControllerBase
{

    private readonly ITreinoService _treinoService;

    public TreinoController(ITreinoService service)
    {
        _treinoService = service;
    }


    [HttpPost]
    [ProducesResponseType(typeof(TreinoResponseDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CriarTreino([FromBody] TreinoCreateRequestDto treinoDto)
    {

        try
        {
            TreinoResponseDto treino = await _treinoService.CriarAsync(treinoDto);
            return CreatedAtAction(nameof(TreinoController.BuscarTreinoPorId), new { id = treino.Id }, treino);
        }
        catch (Exception e)
        {
            return BadRequest(e.InnerException?.Message);
        }
    }


    [HttpGet("{id}")]
    [ProducesResponseType(typeof(TreinoResponseDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> BuscarTreinoPorId(int id)
    {
        try
        {
            var exercicio = await _treinoService.BuscarPorIdAsync(id);

            if (exercicio == null)
            {
                return NotFound();
            }
            return Ok(exercicio);
        }
        catch (Exception e)
        {
            return BadRequest($"Erro ao encontar o exercício pelo motivo: {e.Message}");
        }
    }

    [HttpGet]
    [ProducesResponseType(typeof(EdFisicoResponseDto), StatusCodes.Status200OK)]
    public async Task<IActionResult> BuscarTreinos(
        [FromQuery] int skip = 0,
        [FromQuery] int take = 10,
        [FromQuery] int? educadorId = null,
        [FromQuery] int? alunoId = null)
    {

        if (educadorId != null)
        {
            var treino = await _treinoService.BuscarPorEducadorFisicoIdAsync((int)educadorId);
            if (treino == null)
            {
                return NotFound();
            }
            return Ok(treino);
        }
        if (alunoId != null)
        {
            var treino = await _treinoService.BuscarPorAlunoIdAsync((int)alunoId);
            if (treino == null)
            {
                return NotFound();
            }
            return Ok(treino);
        }
        return Ok(await _treinoService.BuscarAsync(skip, take));
    }

    [HttpPost("realizar/{id}")]
    [ProducesResponseType(typeof(TreinoResponseDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> RealizarTreino(int id,  [FromBody] TreinoRealizadoResponseDto treinoRealizadoDto)
    {

        try
        {
            TreinoRealizadoResponseDto treino = await _treinoService.RealizarTreino(id, treinoRealizadoDto);
            return CreatedAtAction(nameof(TreinoController.BuscarTreinoPorId), new { id = treino.Id }, treino);
        }
        catch (Exception e)
        {
            return BadRequest(e.InnerException?.Message);
        }
    }
}

