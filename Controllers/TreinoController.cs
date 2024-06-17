using ImproveU_backend.Extensions;
using ImproveU_backend.Models;
using ImproveU_backend.Models.Dtos.PessoaDto;
using ImproveU_backend.Models.Dtos.TreinoDto;
using ImproveU_backend.Services.Interfaces.ITreino;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ImproveU_backend.Controllers;

[Authorize]
[ApiController]
[Route("api/treinos")]
public class TreinoController : ControllerBase
{
    private readonly ITreinoService _treinoService;

    public TreinoController(ITreinoService service)
    {
        _treinoService = service;
    }

    [ClaimsAuthorize("educador", "criar")]
    [HttpPost("plano")]
    [ProducesResponseType(typeof(TreinoARealizarResponseDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CriarPlanoDeTreino([FromBody] TreinoARealizarCreateRequestDto treinoDto)
    {

        try
        {
            TreinoARealizarResponseDto treino = await _treinoService.CriarPlanoDeTreinoAsync(treinoDto);
            return CreatedAtAction(nameof(TreinoController.BuscarTreinoPorId), new { id = treino.Id }, treino);
        }
        catch (Exception e)
        {
            return BadRequest(e.InnerException?.Message);
        }
    }


    [ClaimsAuthorize("educador", "ler")]
    [HttpGet("planos-por-id")]
    [ProducesResponseType(typeof(TreinoARealizarResponseDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> BuscarPlanosTreinosPor(
        [FromQuery] int skip = 0,
        [FromQuery] int take = 10,
        [FromQuery] int? educadorId = null,
        [FromQuery] int? alunoId = null)
    {
        if (educadorId != null)
        {
            var treino = await _treinoService.BuscarPlanoAtivoPorEducadorFisicoIdAsync((int)educadorId, skip, take);
            if (treino == null)
            {
                return NotFound();
            }
            return Ok(treino);
        }
        if (alunoId != null)
        {
            var treino = await _treinoService.BuscarPlanoAtivoPorAlunoIdAsync((int)alunoId, skip, take);
            if (treino == null)
            {
                return NotFound();
            }
            return Ok(treino);
        }
        return BadRequest("Nenhum Id foi enviado por parâmetro para realizar a busca");
    }

    [HttpGet("{id}")]
    [ProducesResponseType(typeof(TreinoARealizarResponseDto), StatusCodes.Status200OK)]
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
    [ProducesResponseType(typeof(TreinoARealizarResponseDto), StatusCodes.Status200OK)]
    public async Task<IActionResult> BuscarTreinos(
        [FromQuery] int skip = 0,
        [FromQuery] int take = 10)
    { 
        return Ok(await _treinoService.BuscarAsync(skip, take));
    }

    [ClaimsAuthorize("aluno", "criar")]
    [HttpPost("realizar")]
    [ProducesResponseType(typeof(TreinoARealizarResponseDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> RealizarTreino([FromBody] TreinoRealizadoCreateRequestDto treinoRealizadoDto)
    {

        try
        {
            TreinoARealizarResponseDto treino = await _treinoService.RealizarTreinoAsync(treinoRealizadoDto);
            return CreatedAtAction(nameof(TreinoController.BuscarTreinoPorId), new { id = treino.Id }, treino);
        }
        catch (Exception e)
        {
            return BadRequest(e.InnerException?.Message);
        }
    }
}

