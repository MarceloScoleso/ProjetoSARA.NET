using Microsoft.AspNetCore.Mvc;
using ProjetoSara.Models; 
using ProjetoSara.Services;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using ProjetoSara.Utils;

namespace ProjetoSara.Controllers
{
    [ApiController]
    [Route("api/alertas")]
    public class AlertaController : ControllerBase
    {
        private readonly AlertaService _service;

        public AlertaController(AlertaService service)
        {
            _service = service;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<PagedResult<Alerta>>> Listar(
            [FromQuery] string? titulo,
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 10)
        {
            var result = await _service.ListarAsync(titulo, pageNumber, pageSize);
            return Ok(result);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Alerta>> BuscarPorId(long id)
        {
            var alerta = await _service.FindByIdAsync(id);
            if (alerta == null)
                return NotFound();
            return Ok(alerta);
        }

        [HttpGet("por-nivel")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<PagedResult<Alerta>>> BuscarPorNivelAlerta(
            [FromQuery] long nivelAlertaId,
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 10)
        {
            var result = await _service.BuscarPorNivelAlertaAsync(nivelAlertaId, pageNumber, pageSize);
            return Ok(result);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<ActionResult<Alerta>> Salvar([FromBody] Alerta alerta)
        {
            var created = await _service.SaveAsync(alerta);
            return CreatedAtAction(nameof(BuscarPorId), new { id = created.Id }, created);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Alerta>> Atualizar(long id, [FromBody] Alerta alerta)
        {
            var updated = await _service.UpdateAsync(id, alerta);
            if (updated == null)
                return NotFound();
            return Ok(updated);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> Deletar(long id)
        {
            await _service.DeleteAsync(id);
            return NoContent();
        }
    }
}