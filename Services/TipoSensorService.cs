using Microsoft.EntityFrameworkCore;
using ProjetoSara.Data;
using ProjetoSara.Models;
using ProjetoSara.Utils;

namespace ProjetoSara.Services
{
    public class TipoSensorService
    {
        private readonly ApplicationDbContext _context;

        public TipoSensorService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<PagedResult<TipoSensor>> ListarAsync(string? descricao, int pageNumber, int pageSize)
        {
            var query = _context.TipoSensores.AsQueryable();

            if (!string.IsNullOrEmpty(descricao))
            {
                query = query.Where(t => t.Descricao != null && t.Descricao.Contains(descricao));
            }

            var totalItems = await query.CountAsync();
            var items = await query
                .OrderBy(t => t.Descricao)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return new PagedResult<TipoSensor>(items, totalItems, pageNumber, pageSize);
        }

        public async Task<TipoSensor?> BuscarPorIdAsync(long id)
        {
            return await _context.TipoSensores.FindAsync(id);
        }

        public async Task<TipoSensor> SalvarAsync(TipoSensor tipo)
        {
            _context.TipoSensores.Add(tipo);
            await _context.SaveChangesAsync();
            return tipo;
        }

        public async Task<TipoSensor?> AtualizarAsync(long id, TipoSensor tipo)
        {
            var existente = await _context.TipoSensores.FindAsync(id);
            if (existente == null) return null;

            existente.Descricao = tipo.Descricao;
            existente.Codigo = tipo.Codigo;

            await _context.SaveChangesAsync();
            return existente;
        }

        public async Task<bool> DeletarAsync(long id)
        {
            var existente = await _context.TipoSensores.FindAsync(id);
            if (existente == null) return false;

            _context.TipoSensores.Remove(existente);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<TipoSensor?> BuscarPorCodigoAsync(string codigo)
        {
            return await _context.TipoSensores.FirstOrDefaultAsync(t => t.Codigo != null && t.Codigo == codigo);
        }

        public async Task<long> ContarTodosAsync()
        {
            return await _context.TipoSensores.LongCountAsync();
        }
    }
}