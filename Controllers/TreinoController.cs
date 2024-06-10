using ImproveU_backend.Models;
using ImproveU_backend.Services.Interfaces.ITreino;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using ImproveU_backend.Models.Dtos.TreinoDto;
using ImproveU_backend.Models.Dtos;

namespace ImproveU_backend.Controllers;

[ApiController]
[Route("api")]
public class TreinoController : ControllerBase
{

    private readonly IExercicioService _exercicio;
    private readonly ITreinoService _treino;

    public TreinoController(IExercicioService exercicio, ITreinoService treino)
    {
        _exercicio = exercicio;
        _treino = treino;
    }

    [HttpPost("exercicios")]
    [ProducesResponseType(typeof(ExercicioResponseDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Criar([FromBody] ExercicioCreateRequestDto exercicioDto)
    {

        try
        {
            ExercicioResponseDto exercicio = await _exercicio.CriarAsync(exercicioDto);
            return CreatedAtAction(nameof(TreinoController.BuscarExecicioPorId), new { id = exercicio.Id }, exercicio);
        }
        catch (Exception e)
        {
            return BadRequest(e.InnerException?.Message);
        }
    }

    [HttpGet("exercicios/{id}")]
    [ProducesResponseType(typeof(ExercicioResponseDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> BuscarExecicioPorId(int id)
    {
        try
        {
            var exercicio = await _exercicio.BuscarPorIdAsync(id);
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

    [HttpGet("exercicios")]
    [ProducesResponseType(typeof(ExercicioResponseDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Buscar([FromQuery] int skip = 0,
                                            [FromQuery] int take = 10,
                                            [FromQuery] string? nome = "")
    {
        try
        {
            if(!string.IsNullOrWhiteSpace(nome))
            {
                var exercicio = await _exercicio.BuscarPorNomeAsync(nome);
                if (exercicio == null)
                {
                    return NotFound("Exercicio não localizado");
                }
                return Ok(exercicio);
            }
            var exercicios = await _exercicio.BuscarAsync(skip, take);
            return Ok(exercicios);
        }
        catch (Exception e)
        {
            return BadRequest($"Erro ao encontar o(s) exercício(s) pelo motivo: {e.Message}");
        }
    }

    [HttpPut("exercicios/{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Atualizar(int id, [FromBody] ExercicioUpdateRequestDto exercicioDto)
    {
        try
        {
            await _exercicio.Atualizar(id, exercicioDto);
            return NoContent();
        }
        catch (Exception e)
        {
            return BadRequest($"Erro ao atualizar o exercício pelo motivo: {e.Message}");
        }
    }


    [HttpPost("treino")]
    [ProducesResponseType(typeof(TreinoResponseDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CriarTreino([FromBody] TreinoCreateRequestDto treinoDto)
    {

        try
        {
            TreinoResponseDto treino = await _treino.CriarAsync(treinoDto);
            return CreatedAtAction(nameof(TreinoController.BuscarTreinoPorId), new { id = treino.Id }, treino);
        }
        catch (Exception e)
        {
            return BadRequest(e.InnerException?.Message);
        }
    }


    [HttpGet("treino/{id}")]
    [ProducesResponseType(typeof(TreinoResponseDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> BuscarTreinoPorId(int id)
    {
        try
        {
            var exercicio = await _treino.BuscarPorIdAsync(id);

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
}

