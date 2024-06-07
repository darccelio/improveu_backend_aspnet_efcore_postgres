using ImproveU_backend.Models;
using ImproveU_backend.Models.Dtos;
using ImproveU_backend.Services.Interfaces.ITreino;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

namespace ImproveU_backend.Controllers;

[ApiController]
[Route("api/treinos")]
public class TreinoController : ControllerBase
{

    private readonly IExercicioService _exercicio;
    //private readonly ITreinoService _treino;

    public TreinoController(IExercicioService exercicio)
    {
        _exercicio = exercicio;
        //_treino = treino;
    }

    [HttpPost("exercicios")]
    [ProducesResponseType(typeof(ExercicioResponseDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Criar([FromBody] ExercicioCreateRequestDto exercicioDto)
    {

        try
        {
            ExercicioResponseDto exercicio = await _exercicio.CriarAsync(exercicioDto);
            return CreatedAtAction(nameof(TreinoController.BuscarPorId), new { id = exercicio.Id }, exercicio);
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
    public async Task<IActionResult> BuscarPorId(int id)
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


}

