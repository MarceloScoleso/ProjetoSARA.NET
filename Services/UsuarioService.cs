using Microsoft.EntityFrameworkCore;
using ProjetoSara.Data;
using ProjetoSara.Models;
using ProjetoSara.Utils;
using Microsoft.AspNetCore.Identity; 

namespace ProjetoSara.Services
{
    public class UsuarioService
    {
        private readonly ApplicationDbContext _context;
        private readonly PasswordHasher<Usuario> _passwordHasher;

        public UsuarioService(ApplicationDbContext context)
        {
            _context = context;
            _passwordHasher = new PasswordHasher<Usuario>();
        }

        public async Task<PagedResult<Usuario>> ListarAsync(string? nomeOuEmail, int pageNumber, int pageSize)
        {
            var query = _context.Usuarios.AsQueryable();

            if (!string.IsNullOrWhiteSpace(nomeOuEmail))
            {
                query = query.Where(u =>
                    (u.Nome != null && u.Nome.Contains(nomeOuEmail)) ||
                    (u.Email != null && u.Email.Contains(nomeOuEmail))
                );
            }

            var totalItems = await query.CountAsync();
            var items = await query
                .OrderBy(u => u.Nome)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return new PagedResult<Usuario>(items, totalItems, pageNumber, pageSize);
        }

        public async Task<Usuario?> BuscarPorIdAsync(long id)
        {
            return await _context.Usuarios.FindAsync(id);
        }

        public async Task<Usuario> SalvarAsync(Usuario usuario)
        {
            // Criptografar senha antes de salvar
            usuario.Senha = _passwordHasher.HashPassword(usuario, usuario.Senha);

            _context.Usuarios.Add(usuario);
            await _context.SaveChangesAsync();
            return usuario;
        }

        public async Task<Usuario?> AtualizarAsync(long id, Usuario usuario)
        {
            var existente = await _context.Usuarios.FindAsync(id);
            if (existente == null) return null;

            existente.Nome = usuario.Nome;
            existente.Email = usuario.Email;

            // Criptografar senha antes de atualizar
            existente.Senha = _passwordHasher.HashPassword(existente, usuario.Senha);

            await _context.SaveChangesAsync();
            return existente;
        }

        public async Task<bool> DeletarAsync(long id)
        {
            var existente = await _context.Usuarios.FindAsync(id);
            if (existente == null) return false;

            _context.Usuarios.Remove(existente);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<Usuario?> BuscarPorEmailAsync(string email)
        {
            return await _context.Usuarios
                .FirstOrDefaultAsync(u => u.Email != null && u.Email == email);
        }

        public async Task<long> ContarUsuariosAsync()
        {
            return await _context.Usuarios.LongCountAsync();
        }
    }
}