using Microsoft.AspNetCore.Mvc;
using ProjetoSara.Models;
using ProjetoSara.Services;
using ProjetoSara.Utils;
using Microsoft.EntityFrameworkCore;

namespace ProjetoSara.Controllers
{
    [ApiController]
    [Route("api/sensores")]
    public class SensorController : ControllerBase
    {
        private readonly SensorService _service;

        public SensorController(SensorService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<PagedResult<Sensor>>> Listar(
            [FromQuery] long? tipoSensorId,
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 10)
        {
            var resultado = await _service.Listar(tipoSensorId, pageNumber, pageSize);
            return Ok(resultado);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Sensor>> BuscarPorId(long id)
        {
            var sensor = await _service.FindById(id);
            if (sensor == null)
                return NotFound();
            return Ok(sensor);
        }

        [HttpPost]
        public async Task<ActionResult<Sensor>> Salvar([FromBody] Sensor sensor)
        {
            var salvo = await _service.Save(sensor);
            return CreatedAtAction(nameof(BuscarPorId), new { id = salvo.Id }, salvo);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Sensor>> Atualizar(long id, [FromBody] Sensor sensor)
        {
            if (id != sensor.Id)
                return BadRequest();

            var atualizado = await _service.Update(id, sensor);
            if (atualizado == null)
                return NotFound();

            return Ok(atualizado);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Deletar(long id)
        {
            var deletado = await _service.Delete(id);
            if (!deletado)
                return NotFound();

            return NoContent();
        }

        [HttpGet("tipo/{tipoSensorId}")]
        public async Task<ActionResult<PagedResult<Sensor>>> ListarPorTipo(long tipoSensorId,
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 10)
        {
            var resultado = await _service.ListarPorTipo(tipoSensorId, pageNumber, pageSize);
            return Ok(resultado);
        }

        [HttpGet("count-by-localizacao")]
        public async Task<ActionResult<long>> ContarPorLocalizacao([FromQuery] long localizacaoId)
        {
            var count = await _service.ContarPorLocalizacao(localizacaoId);
            return Ok(count);
        }
    }
}