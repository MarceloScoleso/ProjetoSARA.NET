using Microsoft.EntityFrameworkCore;
using ProjetoSara.Data;
using ProjetoSara.Models;
using ProjetoSara.Utils;

namespace ProjetoSara.Services
{
    public class SensorService
    {
        private readonly ApplicationDbContext _context;

        public SensorService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<PagedResult<Sensor>> Listar(long? tipoSensorId, int pageNumber, int pageSize)
        {
            var query = _context.Sensores.AsQueryable();

            if (tipoSensorId.HasValue)
                query = query.Where(s => s.TipoSensorId == tipoSensorId.Value);

            query = query.OrderBy(s => s.Id);

            var totalItems = await query.CountAsync();
            var items = await query.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();

            return new PagedResult<Sensor>
            {
                Items = items,
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalItems = totalItems
            };
        }

        public async Task<Sensor?> FindById(long id)
        {
            return await _context.Sensores.FindAsync(id);
        }

        public async Task<Sensor> Save(Sensor sensor)
        {
            _context.Sensores.Add(sensor);
            await _context.SaveChangesAsync();
            return sensor;
        }

        public async Task<Sensor?> Update(long id, Sensor sensor)
        {
            var existe = await _context.Sensores.AnyAsync(s => s.Id == id);
            if (!existe)
                return null;

            _context.Entry(sensor).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return sensor;
        }

        public async Task<bool> Delete(long id)
        {
            var sensor = await _context.Sensores.FindAsync(id);
            if (sensor == null)
                return false;

            _context.Sensores.Remove(sensor);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<PagedResult<Sensor>> ListarPorTipo(long tipoSensorId, int pageNumber, int pageSize)
        {
            var query = _context.Sensores
                .Where(s => s.TipoSensorId == tipoSensorId)
                .OrderBy(s => s.Id);

            var totalItems = await query.CountAsync();
            var items = await query.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();

            return new PagedResult<Sensor>
            {
                Items = items,
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalItems = totalItems
            };
        }

        public async Task<long> ContarPorLocalizacao(long localizacaoId)
        {
            return await _context.Sensores.LongCountAsync(s => s.LocalizacaoId == localizacaoId);
        }
    }
}