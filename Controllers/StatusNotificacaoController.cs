using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjetoSara.Models;
using ProjetoSara.Services;
using ProjetoSara.Utils;
using System.Linq;
using System.Threading.Tasks;

namespace SaraApi.Controllers
{
    [ApiController]
    [Route("api/status-notificacoes")]
    public class StatusNotificacaoController : ControllerBase
    {
        private readonly StatusNotificacaoService _service;

        public StatusNotificacaoController(StatusNotificacaoService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<PagedResult<StatusNotificacao>>> Listar(
            [FromQuery] string? descricao,
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 10)
        {
            var resultado = await _service.Listar(descricao, pageNumber, pageSize);
            return Ok(resultado);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<StatusNotificacao>> BuscarPorId(long id)
        {
            var entidade = await _service.BuscarPorId(id);
            if (entidade == null) return NotFound();
            return Ok(entidade);
        }

        [HttpPost]
        public async Task<ActionResult<StatusNotificacao>> Salvar(StatusNotificacao model)
        {
            var salvo = await _service.Salvar(model);
            return CreatedAtAction(nameof(BuscarPorId), new { id = salvo.Id }, salvo);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<StatusNotificacao>> Atualizar(long id, StatusNotificacao model)
        {
            var atualizado = await _service.Atualizar(id, model);
            if (atualizado == null) return NotFound();
            return Ok(atualizado);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Deletar(long id)
        {
            var excluido = await _service.Deletar(id);
            if (!excluido) return NotFound();
            return NoContent();
        }

        [HttpGet("codigo/{codigo}")]
        public async Task<ActionResult<StatusNotificacao>> BuscarPorCodigo(string codigo)
        {
            var entidade = await _service.BuscarPorCodigo(codigo);
            if (entidade == null) return NotFound();
            return Ok(entidade);
        }

        [HttpGet("count")]
        public async Task<ActionResult<long>> ContarTodos()
        {
            var total = await _service.ContarTodos();
            return Ok(total);
        }
    }
}