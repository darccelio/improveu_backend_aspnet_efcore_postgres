using ImproveU_backend.Extensions;
using ImproveU_backend.Models.Dtos.PessoaDto;
using ImproveU_backend.Services.Interfaces.IPessoaSerivce;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ImproveU_backend.Controllers
{
    [Authorize]
    [Route("api/alunos")]
    [ApiController]
    public class AlunosController : ControllerBase
    {

        private readonly IAlunoService _alunoService;

        public AlunosController(IAlunoService alunoService)
        {
            _alunoService = alunoService;
        }

        [ClaimsAuthorize("aluno", "criar")]
        [HttpPost]
        [ProducesResponseType(typeof(AlunoResponseDto), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Criar([FromBody] AlunoCreateRequestDto dto)
        {
            if (dto == null)
                return BadRequest($"{nameof(dto)}, O objeto AlunoCreateRequestDto não pode ser nulo.");
            try
            {
                AlunoResponseDto resp = await _alunoService.CriarAsync(dto);
                return CreatedAtAction(nameof(AlunosController.BuscarPorId), new { id = resp.Id }, resp);
            }
            catch (ArgumentException e)
            {
                return BadRequest(e.Message);
            }

        }

        [ClaimsAuthorize("educador", "ler")]
        [HttpGet]
        [ProducesResponseType(typeof(AlunoResponseDto), StatusCodes.Status200OK)]
        public async Task<IActionResult> Buscar(
        [FromQuery] int skip = 0,
        [FromQuery] int take = 10)
        {
            return Ok(await _alunoService.BuscarAsync(skip, take));
        }

        [ClaimsAuthorize("educador", "ler")]
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(AlunoResponseDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> BuscarPorId(int id)
        {
            AlunoResponseDto edFisico = await _alunoService.BuscarPorIdAsync(id);
            if (edFisico?.Id == null)
                return NotFound();

            return Ok(edFisico);
        }

        [ClaimsAuthorize("aluno", "ler")]
        [HttpPut("{id}")]
        [ProducesResponseType(typeof(AlunoResponseDto), StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Put(int id, [FromBody] AlunoUpdateRequestDto value)
        {
            try
            {
                await _alunoService.AtualizarAsync(id, value);
                return NoContent();
            }
            catch (ArgumentException e)
            {
                return BadRequest(e.Message);
            }

        }

        [ClaimsAuthorize("educador", "ler")]
        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(AlunoResponseDto), StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _alunoService.RemoverPorIdAsync(id);
                return NoContent();
            }
            catch (ArgumentException ex)
            {
                return NotFound(ex.Message); // Retorna 404 com a mensagem da exceção
            }
            catch (DbUpdateException ex)
            {
                // Trate exceções específicas do Entity Framework Core, por exemplo, violações de chave estrangeira
                return BadRequest("Erro ao tentar atualizar o banco de dados.");
            }
            catch (Exception ex)
            {
                // Lidar com outras exceções de forma genérica
                // Registrar o erro em um log, retornar uma mensagem de erro genérica, etc.
                return StatusCode(500, "Ocorreu um erro interno no servidor.");
            }
        }
    }
}