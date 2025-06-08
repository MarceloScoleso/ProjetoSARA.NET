using Microsoft.EntityFrameworkCore;
using ProjetoSara.Data;
using ProjetoSara.Models; 
using ProjetoSara.Utils;  
using System;
using System.Linq;
using System.Threading.Tasks;

namespace ProjetoSara.Services
{
    public class AlertaService
    {
        private readonly ApplicationDbContext _context;

        public AlertaService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<PagedResult<Alerta>> ListarAsync(string? titulo, int pageNumber, int pageSize)
        {
            var query = _context.Alertas.AsQueryable();

            if (!string.IsNullOrEmpty(titulo))
            {
                query = query.Where(a => a.Mensagem != null && a.Mensagem.Contains(titulo));
            }

            int totalItems = await query.CountAsync();

            var items = await query
                .OrderByDescending(a => a.DataHora)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return new PagedResult<Alerta>
            {
                Items = items,
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalItems = totalItems
            };
        }

        public async Task<PagedResult<Alerta>> BuscarPorNivelAlertaAsync(long nivelAlertaId, int pageNumber, int pageSize)
        {
            var query = _context.Alertas
                .Where(a => a.NivelAlertaId == nivelAlertaId);

            int totalItems = await query.CountAsync();

            var items = await query
                .OrderByDescending(a => a.DataHora)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return new PagedResult<Alerta>
            {
                Items = items,
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalItems = totalItems
            };
        }

        public async Task<Alerta?> FindByIdAsync(long id)
        {
            return await _context.Alertas.FindAsync(id);
        }

        public async Task<Alerta> SaveAsync(Alerta alerta)
        {
            _context.Alertas.Add(alerta);
            await _context.SaveChangesAsync();
            return alerta;
        }

        public async Task<Alerta?> UpdateAsync(long id, Alerta alerta)
        {
            var existing = await _context.Alertas.FindAsync(id);
            if (existing == null) return null;

            // Atualize os campos manualmente, exceto o Id
            existing.Mensagem = alerta.Mensagem;
            existing.DataHora = alerta.DataHora;
            existing.UsuarioId = alerta.UsuarioId;
            existing.NivelAlertaId = alerta.NivelAlertaId;
            existing.LocalizacaoId = alerta.LocalizacaoId;

            await _context.SaveChangesAsync();
            return existing;
        }

        public async Task DeleteAsync(long id)
        {
            var alerta = await _context.Alertas.FindAsync(id);
            if (alerta != null)
            {
                _context.Alertas.Remove(alerta);
                await _context.SaveChangesAsync();
            }
        }
    }
}