using Microsoft.EntityFrameworkCore;
using ProjetoSara.Data;
using ProjetoSara.Models;
using ProjetoSara.Utils;

namespace ProjetoSara.Services
{
    public class NotificacaoService
    {
        private readonly ApplicationDbContext _context;

        public NotificacaoService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<PagedResult<Notificacao>> Listar(long? usuarioId, int pageNumber, int pageSize)
        {
            var query = _context.Notificacoes.AsQueryable();

            if (usuarioId.HasValue)
                query = query.Where(n => n.UsuarioId == usuarioId.Value);

            query = query.OrderByDescending(n => n.DataEnvio);

            var totalItems = await query.CountAsync();
            var items = await query.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();

            return new PagedResult<Notificacao>
            {
                Items = items,
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalItems = totalItems
            };
        }

        public async Task<Notificacao?> FindById(long id)
        {
            return await _context.Notificacoes.FindAsync(id);
        }

        public async Task<Notificacao> Save(Notificacao notificacao)
        {
            _context.Notificacoes.Add(notificacao);
            await _context.SaveChangesAsync();
            return notificacao;
        }

        public async Task<Notificacao?> Update(long id, Notificacao notificacao)
        {
            var exists = await _context.Notificacoes.AnyAsync(n => n.Id == id);
            if (!exists) return null;

            _context.Entry(notificacao).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return notificacao;
        }

        public async Task<bool> Delete(long id)
        {
            var notificacao = await _context.Notificacoes.FindAsync(id);
            if (notificacao == null)
                return false;

            _context.Notificacoes.Remove(notificacao);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<PagedResult<Notificacao>> ListarPorStatus(long statusId, int pageNumber, int pageSize)
        {
            var query = _context.Notificacoes.Where(n => n.StatusId == statusId)
                                            .OrderByDescending(n => n.DataEnvio);

            var totalItems = await query.CountAsync();
            var items = await query.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();

            return new PagedResult<Notificacao>
            {
                Items = items,
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalItems = totalItems
            };
        }

        public async Task<long> ContarPorUsuario(long usuarioId)
        {
            return await _context.Notificacoes.LongCountAsync(n => n.UsuarioId == usuarioId);
        }
    }
}