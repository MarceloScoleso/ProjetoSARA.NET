using ProjetoSara.Models;
using ProjetoSara.Data; 
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Linq;
using ProjetoSara.Utils;

public class NivelAlertaService
{
    private readonly ApplicationDbContext _context;

    public NivelAlertaService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<PagedResult<NivelAlerta>> ListarAsync(string? codigo, int pageNumber, int pageSize)
    {
        var query = _context.NivelAlertas.AsQueryable();

        if (!string.IsNullOrEmpty(codigo))
        {
            query = query.Where(n => n.Codigo != null && n.Codigo.Contains(codigo));
        }

        var totalItems = await query.CountAsync();

        var items = await query
            .OrderBy(n => n.Descricao)
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return new PagedResult<NivelAlerta>
        {
            Items = items,
            PageNumber = pageNumber,
            PageSize = pageSize,
            TotalItems = totalItems
        };
    }

    public async Task<NivelAlerta?> FindByIdAsync(long id)
    {
        return await _context.NivelAlertas.FindAsync(id);
    }

    public async Task<NivelAlerta?> FindByCodigoExatoAsync(string codigo)
    {
        return await _context.NivelAlertas
            .FirstOrDefaultAsync(n => n.Codigo == codigo);
    }

    public async Task<long?> ContarAlertasAsync(long id)
    {
        var nivel = await _context.NivelAlertas
            .Include(n => n.Alertas)
            .FirstOrDefaultAsync(n => n.Id == id);

        return nivel == null ? (long?)null : nivel.Alertas?.Count ?? 0;
    }

    public async Task<NivelAlerta> SaveAsync(NivelAlerta entidade)
    {
        _context.NivelAlertas.Add(entidade);
        await _context.SaveChangesAsync();
        return entidade;
    }

    public async Task<NivelAlerta?> UpdateAsync(long id, NivelAlerta entidade)
    {
        var existing = await _context.NivelAlertas.FindAsync(id);
        if (existing == null) return null;

        existing.Codigo = entidade.Codigo;
        existing.Descricao = entidade.Descricao;
        // atualize outras propriedades aqui se existirem

        await _context.SaveChangesAsync();
        return existing;
    }

    public async Task DeleteAsync(long id)
    {
        var existing = await _context.NivelAlertas.FindAsync(id);
        if (existing != null)
        {
            _context.NivelAlertas.Remove(existing);
            await _context.SaveChangesAsync();
        }
    }
}