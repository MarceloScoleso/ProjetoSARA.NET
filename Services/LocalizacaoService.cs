using Microsoft.EntityFrameworkCore;
using ProjetoSara.Data;  
using ProjetoSara.Models;
using ProjetoSara.Utils;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjetoSara.Services
{
    public class LocalizacaoService
    {
        private readonly ApplicationDbContext _context;

        public LocalizacaoService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<PagedResult<Localizacao>> ListarAsync(string? cidade, int pageNumber, int pageSize)
        {
            var query = _context.Localizacoes.AsQueryable();

            if (!string.IsNullOrWhiteSpace(cidade))
            query = query.Where(l => l.Cidade != null && l.Cidade.Contains(cidade));
            var totalItems = await query.CountAsync();

            var items = await query
                .OrderBy(l => l.Cidade)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return new PagedResult<Localizacao>(items, totalItems, pageNumber, pageSize);
        }

        public async Task<Localizacao?> FindByIdAsync(long id)
        {
            return await _context.Localizacoes.FindAsync(id);
        }

        public async Task<Localizacao> SaveAsync(Localizacao localizacao)
        {
            _context.Localizacoes.Add(localizacao);
            await _context.SaveChangesAsync();
            return localizacao;
        }

        public async Task<Localizacao?> UpdateAsync(long id, Localizacao localizacao)
        {
            var existing = await _context.Localizacoes.FindAsync(id);
            if (existing == null) return null;

            // Atualiza campos
            existing.Cidade = localizacao.Cidade;
            existing.Estado = localizacao.Estado;
        

            await _context.SaveChangesAsync();
            return existing;
        }

        public async Task DeleteAsync(long id)
        {
            var existing = await _context.Localizacoes.FindAsync(id);
            if (existing != null)
            {
                _context.Localizacoes.Remove(existing);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<PagedResult<Localizacao>> ListarPorEstadoAsync(string estado, int pageNumber, int pageSize)
        {
            var query = _context.Localizacoes.Where(l => l.Estado == estado);

            var totalItems = await query.CountAsync();

            var items = await query
                .OrderBy(l => l.Estado)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return new PagedResult<Localizacao>(items, totalItems, pageNumber, pageSize);
        }
    }
}