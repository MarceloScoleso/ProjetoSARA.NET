using Microsoft.EntityFrameworkCore;
using ProjetoSara.Data;
using ProjetoSara.Models;
using ProjetoSara.Utils;

namespace ProjetoSara.Services
{
    public class TipoUsuarioService
    {
        private readonly ApplicationDbContext _context;

        public TipoUsuarioService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<PagedResult<TipoUsuario>> ListarAsync(string? codigo, int pageNumber, int pageSize)
        {
            var query = _context.TipoUsuarios.AsQueryable();

            if (!string.IsNullOrEmpty(codigo))
            {
                query = query.Where(t => t.Codigo != null && t.Codigo.Contains(codigo));
            }

            var totalItems = await query.CountAsync();
            var items = await query
                .OrderBy(t => t.Descricao)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return new PagedResult<TipoUsuario>(items, totalItems, pageNumber, pageSize);
        }

        public async Task<TipoUsuario?> BuscarPorIdAsync(long id)
        {
            return await _context.TipoUsuarios.FindAsync(id);
        }

        public async Task<TipoUsuario> SalvarAsync(TipoUsuario tipo)
        {
            _context.TipoUsuarios.Add(tipo);
            await _context.SaveChangesAsync();
            return tipo;
        }

        public async Task<TipoUsuario?> AtualizarAsync(long id, TipoUsuario tipo)
        {
            var existente = await _context.TipoUsuarios.FindAsync(id);
            if (existente == null) return null;

            existente.Descricao = tipo.Descricao;
            existente.Codigo = tipo.Codigo;

            await _context.SaveChangesAsync();
            return existente;
        }

        public async Task<bool> DeletarAsync(long id)
        {
            var existente = await _context.TipoUsuarios.FindAsync(id);
            if (existente == null) return false;

            _context.TipoUsuarios.Remove(existente);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<TipoUsuario?> BuscarPorCodigoAsync(string codigo)
        {
            return await _context.TipoUsuarios
                .FirstOrDefaultAsync(t => t.Codigo != null && t.Codigo == codigo);
        }

        public async Task<long> ContarTodosAsync()
        {
            return await _context.TipoUsuarios.LongCountAsync();
        }
    }
}