using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjetoSara.Models;
using ProjetoSara.Utils;
using ProjetoSara.Services;

namespace ProjetoSara.Controllers
{
    [ApiController]
    [Route("api/notificacoes")]
    public class NotificacaoController : ControllerBase
    {
        private readonly NotificacaoService _service;

        public NotificacaoController(NotificacaoService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<PagedResult<Notificacao>>> Listar(
            [FromQuery] long? usuarioId,
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 10)
        {
            var paged = await _service.Listar(usuarioId, pageNumber, pageSize);
            return Ok(paged);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Notificacao>> BuscarPorId(long id)
        {
            var notificacao = await _service.FindById(id);
            if (notificacao == null)
                return NotFound();
            return Ok(notificacao);
        }

        [HttpPost]
        public async Task<ActionResult<Notificacao>> Salvar([FromBody] Notificacao notificacao)
        {
            var salva = await _service.Save(notificacao);
            return CreatedAtAction(nameof(BuscarPorId), new { id = salva.Id }, salva);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Notificacao>> Atualizar(long id, [FromBody] Notificacao notificacao)
        {
            if (id != notificacao.Id)
                return BadRequest();

            var atualizado = await _service.Update(id, notificacao);
            if (atualizado == null)
                return NotFound();

            return Ok(atualizado);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Deletar(long id)
        {
            var deleted = await _service.Delete(id);
            if (!deleted)
                return NotFound();
            return NoContent();
        }

        [HttpGet("status")]
        public async Task<ActionResult<PagedResult<Notificacao>>> ListarPorStatus(
            [FromQuery] long statusId,
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 10)
        {
            var paged = await _service.ListarPorStatus(statusId, pageNumber, pageSize);
            return Ok(paged);
        }

        [HttpGet("count-by-usuario")]
        public async Task<ActionResult<long>> ContarPorUsuario([FromQuery] long usuarioId)
        {
            var count = await _service.ContarPorUsuario(usuarioId);
            return Ok(count);
        }
    }
}