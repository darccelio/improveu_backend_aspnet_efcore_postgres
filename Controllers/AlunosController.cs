using ImproveU_backend.Models.Dtos;
using ImproveU_backend.Services;
using ImproveU_backend.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ImproveU_backend.Controllers
{
    [Route("api/alunos")]
    [ApiController]
    public class AlunosController : ControllerBase
    {

        private readonly IAlunoService _alunoService;

        public AlunosController(IAlunoService alunoService)
        {
            _alunoService = alunoService;
        }

        // POST api/<AlunoController>
        [HttpPost]
        [ProducesResponseType(typeof(AlunoResponseDto), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Criar([FromBody] AlunoCreateRequestDto dto)
        {

            if (dto == null)
                return BadRequest();
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


        // GET: api/<AlunoController>
        [HttpGet]
        [ProducesResponseType(typeof(AlunoResponseDto), StatusCodes.Status200OK)]
        public async Task<IActionResult> Buscar(
        [FromQuery] int skip = 0,
        [FromQuery] int take = 10)
        {
            return Ok(await _alunoService.BuscarAsync(skip, take));
        }

        // GET api/<AlunoController>/5
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



        // PUT api/<AlunoController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] string value)
        {
            return Ok();

        }

        // DELETE api/<AlunoController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            return NoContent();
        }
    }
}
