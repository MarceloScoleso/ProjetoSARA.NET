using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjetoSara.Models;
using ProjetoSara.Services;
using ProjetoSara.Utils;

namespace ProjetoSara.Controllers
{
    [ApiController]
    [Route("api/tipos-sensor")]
    public class TipoSensorController : ControllerBase
    {
        private readonly TipoSensorService _service;

        public TipoSensorController(TipoSensorService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<PagedResult<TipoSensor>>> Listar(
            [FromQuery] string? descricao,
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 10)
        {
            var resultado = await _service.ListarAsync(descricao, pageNumber, pageSize);
            return Ok(resultado);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TipoSensor>> BuscarPorId(long id)
        {
            var tipo = await _service.BuscarPorIdAsync(id);
            if (tipo == null) return NotFound();
            return Ok(tipo);
        }

        [HttpPost]
        public async Task<ActionResult<TipoSensor>> Salvar([FromBody] TipoSensor tipoSensor)
        {
            var criado = await _service.SalvarAsync(tipoSensor);
            return CreatedAtAction(nameof(BuscarPorId), new { id = criado.Id }, criado);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<TipoSensor>> Atualizar(long id, [FromBody] TipoSensor tipoSensor)
        {
            var atualizado = await _service.AtualizarAsync(id, tipoSensor);
            if (atualizado == null) return NotFound();
            return Ok(atualizado);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Deletar(long id)
        {
            var sucesso = await _service.DeletarAsync(id);
            if (!sucesso) return NotFound();
            return NoContent();
        }

        [HttpGet("codigo/{codigo}")]
        public async Task<ActionResult<TipoSensor>> BuscarPorCodigo(string codigo)
        {
            var tipo = await _service.BuscarPorCodigoAsync(codigo);
            if (tipo == null) return NotFound();
            return Ok(tipo);
        }

        [HttpGet("count")]
        public async Task<ActionResult<long>> ContarTodos()
        {
            var total = await _service.ContarTodosAsync();
            return Ok(total);
        }
    }
}