using Microsoft.AspNetCore.Mvc;
using ProjetoSara.Models;
using ProjetoSara.Services;
using ProjetoSara.Utils;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace ProjetoSara.Controllers
{
    [ApiController]
    [Route("api/localizacoes")]
    public class LocalizacaoController : ControllerBase
    {
        private readonly LocalizacaoService _service;

        public LocalizacaoController(LocalizacaoService service)
        {
            _service = service;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<PagedResult<Localizacao>>> Listar(
            [FromQuery] string? cidade,
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 10)
        {
            var result = await _service.ListarAsync(cidade, pageNumber, pageSize);
            return Ok(result);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Localizacao>> BuscarPorId(long id)
        {
            var localizacao = await _service.FindByIdAsync(id);
            if (localizacao == null) return NotFound();
            return Ok(localizacao);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<ActionResult<Localizacao>> Salvar([FromBody] Localizacao localizacao)
        {
            var created = await _service.SaveAsync(localizacao);
            return CreatedAtAction(nameof(BuscarPorId), new { id = created.Id }, created);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Localizacao>> Atualizar(long id, [FromBody] Localizacao localizacao)
        {
            var updated = await _service.UpdateAsync(id, localizacao);
            if (updated == null) return NotFound();
            return Ok(updated);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> Deletar(long id)
        {
            await _service.DeleteAsync(id);
            return NoContent();
        }

        [HttpGet("estado")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<PagedResult<Localizacao>>> ListarPorEstado(
            [FromQuery] string estado,
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 10)
        {
            var result = await _service.ListarPorEstadoAsync(estado, pageNumber, pageSize);
            return Ok(result);
        }
    }
}