using Microsoft.EntityFrameworkCore;
using ProjetoSara.Data;
using ProjetoSara.Models;
using ProjetoSara.Utils;
using System.Linq;
using System.Threading.Tasks;

namespace ProjetoSara.Services
{
    public class StatusNotificacaoService
    {
        private readonly ApplicationDbContext _context;

        public StatusNotificacaoService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<PagedResult<StatusNotificacao>> Listar(string? descricao, int pageNumber, int pageSize)
        {
            var query = _context.StatusNotificacoes.AsQueryable();

            if (!string.IsNullOrWhiteSpace(descricao))
            {
                query = query.Where(s => EF.Functions.Like((s.Descricao ?? "").ToLower(), $"%{descricao.ToLower()}%"));
            }

            query = query.OrderBy(s => s.Descricao);

            return await PagedResult<StatusNotificacao>.CreateAsync(query, pageNumber, pageSize);
        }

        public async Task<StatusNotificacao?> BuscarPorId(long id)
        {
            return await _context.StatusNotificacoes.FindAsync(id);
        }

        public async Task<StatusNotificacao?> BuscarPorCodigo(string codigo)
        {
            return await _context.StatusNotificacoes
                .FirstOrDefaultAsync(s => s.Codigo.ToLower() == codigo.ToLower());
        }

        public async Task<StatusNotificacao> Salvar(StatusNotificacao model)
        {
            _context.StatusNotificacoes.Add(model);
            await _context.SaveChangesAsync();
            return model;
        }

        public async Task<StatusNotificacao?> Atualizar(long id, StatusNotificacao model)
        {
            var entidade = await _context.StatusNotificacoes.FindAsync(id);
            if (entidade == null) return null;

            entidade.Codigo = model.Codigo;
            entidade.Descricao = model.Descricao;

            await _context.SaveChangesAsync();
            return entidade;
        }

        public async Task<bool> Deletar(long id)
        {
            var entidade = await _context.StatusNotificacoes.FindAsync(id);
            if (entidade == null) return false;

            _context.StatusNotificacoes.Remove(entidade);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<long> ContarTodos()
        {
            return await _context.StatusNotificacoes.LongCountAsync();
        }
    }
}