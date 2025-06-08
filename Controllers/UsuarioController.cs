using Microsoft.AspNetCore.Mvc;
using ProjetoSara.Models;
using ProjetoSara.Services;
using ProjetoSara.Utils;

namespace ProjetoSara.Controllers
{
    [ApiController]
    [Route("api/usuarios")]
    public class UsuarioController : ControllerBase
    {
        private readonly UsuarioService _service;

        public UsuarioController(UsuarioService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<PagedResult<Usuario>>> Listar(
            [FromQuery] string? nome,
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 10)
        {
            var resultado = await _service.ListarAsync(nome, pageNumber, pageSize);
            return Ok(resultado);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Usuario>> BuscarPorId(long id)
        {
            var usuario = await _service.BuscarPorIdAsync(id);
            if (usuario == null) return NotFound();
            return Ok(usuario);
        }

        [HttpPost]
        public async Task<ActionResult<Usuario>> Salvar([FromBody] Usuario usuario)
        {
            var criado = await _service.SalvarAsync(usuario);
            return CreatedAtAction(nameof(BuscarPorId), new { id = criado.Id }, criado);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Usuario>> Atualizar(long id, [FromBody] Usuario usuario)
        {
            var atualizado = await _service.AtualizarAsync(id, usuario);
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

        [HttpGet("email/{email}")]
        public async Task<ActionResult<Usuario>> BuscarPorEmail(string email)
        {
            var usuario = await _service.BuscarPorEmailAsync(email);
            if (usuario == null) return NotFound();
            return Ok(usuario);
        }

        [HttpGet("count")]
        public async Task<ActionResult<long>> ContarUsuarios()
        {
            var total = await _service.ContarUsuariosAsync();
            return Ok(total);
        }
    }
}