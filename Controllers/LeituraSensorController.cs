using Microsoft.AspNetCore.Mvc;
using ProjetoSara.Models;   
using ProjetoSara.Services;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using ProjetoSara.Utils;

namespace ProjetoSara.Controllers
{
    [ApiController]
    [Route("api/leitura-sensores")]
    public class LeituraSensorController : ControllerBase
    {
        private readonly LeituraSensorService _service;

        public LeituraSensorController(LeituraSensorService service)
        {
            _service = service;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<PagedResult<LeituraSensor>>> Listar(
            [FromQuery] long? sensorId,
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 10)
        {
            var result = await _service.ListarAsync(sensorId, pageNumber, pageSize);
            return Ok(result);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<LeituraSensor>> BuscarPorId(long id)
        {
            var leitura = await _service.FindByIdAsync(id);
            if (leitura == null)
                return NotFound();
            return Ok(leitura);
        }

        [HttpGet("ultima")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<LeituraSensor>> BuscarUltimaLeitura([FromQuery] long sensorId)
        {
            var leitura = await _service.BuscarUltimaLeituraAsync(sensorId);
            if (leitura == null)
                return NotFound();
            return Ok(leitura);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<ActionResult<LeituraSensor>> Salvar([FromBody] LeituraSensor leitura)
        {
            var created = await _service.SaveAsync(leitura);
            return CreatedAtAction(nameof(BuscarPorId), new { id = created.Id }, created);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<LeituraSensor>> Atualizar(long id, [FromBody] LeituraSensor leitura)
        {
            var updated = await _service.UpdateAsync(id, leitura);
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