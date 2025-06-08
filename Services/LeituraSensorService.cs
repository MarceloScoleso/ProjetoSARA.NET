using ProjetoSara.Models;
using ProjetoSara.Data; 
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Linq;
using ProjetoSara.Utils;

namespace ProjetoSara.Services
{
    public class LeituraSensorService
    {
        private readonly ApplicationDbContext _context;

        public LeituraSensorService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<PagedResult<LeituraSensor>> ListarAsync(long? sensorId, int pageNumber, int pageSize)
        {
            var query = _context.LeituraSensores.AsQueryable();

            if (sensorId.HasValue)
                query = query.Where(ls => ls.SensorId == sensorId.Value);

            query = query.OrderByDescending(ls => ls.DataHora);

            var totalItems = await query.CountAsync();

            var items = await query
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return new PagedResult<LeituraSensor>(items, totalItems, pageNumber, pageSize);
        }

        public async Task<LeituraSensor?> FindByIdAsync(long id)
        {
            return await _context.LeituraSensores.FindAsync(id);
        }

        public async Task<LeituraSensor?> BuscarUltimaLeituraAsync(long sensorId)
        {
            return await _context.LeituraSensores
                .Where(ls => ls.SensorId == sensorId)
                .OrderByDescending(ls => ls.DataHora)
                .FirstOrDefaultAsync();
        }

        public async Task<LeituraSensor> SaveAsync(LeituraSensor leitura)
        {
            _context.LeituraSensores.Add(leitura);
            await _context.SaveChangesAsync();
            return leitura;
        }

        public async Task<LeituraSensor?> UpdateAsync(long id, LeituraSensor leitura)
        {
            var existing = await _context.LeituraSensores.FindAsync(id);
            if (existing == null)
                return null;

            // Atualize os campos que quiser copiar:
            existing.SensorId = leitura.SensorId;
            existing.DataHora = leitura.DataHora;
            existing.Valor = leitura.Valor;
            // ... demais propriedades

            await _context.SaveChangesAsync();
            return existing;
        }

        public async Task DeleteAsync(long id)
        {
            var existing = await _context.LeituraSensores.FindAsync(id);
            if (existing != null)
            {
                _context.LeituraSensores.Remove(existing);
                await _context.SaveChangesAsync();
            }
        }
    }
}