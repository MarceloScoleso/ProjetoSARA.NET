using Microsoft.AspNetCore.Mvc;
using ProjetoSara.Services;
using ProjetoSara.Models;
using ProjetoSara.Utils;
using System.Threading.Tasks;

namespace ProjetoSara.Controllers
{
    [ApiController]
    [Route("api/niveis-alerta")]
    public class NivelAlertaController : ControllerBase
    {
        private readonly NivelAlertaService _service;

        public NivelAlertaController(NivelAlertaService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<PagedResult<NivelAlerta>>> Listar(
            [FromQuery] string? codigo,
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 10)
        {
            var result = await _service.ListarAsync(codigo, pageNumber, pageSize);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<NivelAlerta>> BuscarPorId(long id)
        {
            var entidade = await _service.FindByIdAsync(id);
            if (entidade == null) return NotFound();
            return Ok(entidade);
        }

        [HttpPost]
        public async Task<ActionResult<NivelAlerta>> Salvar([FromBody] NivelAlerta entidade)
        {
            var criado = await _service.SaveAsync(entidade);
            return CreatedAtAction(nameof(BuscarPorId), new { id = criado.Id }, criado);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<NivelAlerta>> Atualizar(long id, [FromBody] NivelAlerta entidade)
        {
            var atualizado = await _service.UpdateAsync(id, entidade);
            if (atualizado == null) return NotFound();
            return Ok(atualizado);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Deletar(long id)
        {
            await _service.DeleteAsync(id);
            return NoContent();
        }

        [HttpGet("codigo-exato")]
        public async Task<ActionResult<NivelAlerta>> BuscarPorCodigoExato([FromQuery] string codigo)
        {
            var entidade = await _service.FindByCodigoExatoAsync(codigo);
            if (entidade == null) return NotFound();
            return Ok(entidade);
        }

        [HttpGet("contar-alertas")]
        public async Task<ActionResult<long>> ContarAlertas([FromQuery] long id)
        {
            var count = await _service.ContarAlertasAsync(id);
            if (count == null) return NotFound();
            return Ok(count);
        }
    }
}