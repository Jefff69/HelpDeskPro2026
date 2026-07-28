using HelpDeskPro2026.Data;
using HelpDeskPro2026.Models;
using Microsoft.EntityFrameworkCore;

namespace HelpDeskPro2026.Services
{
    public class UsuarioService : IUsuarioService
    {
        private readonly AppDbContext _context;

        public UsuarioService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Usuario>> ObtenerTodosAsync()
        {
            return await _context.Usuarios
                .Include(u => u.Rol)
                .ToListAsync();
        }


        public async Task<Usuario?> ObtenerPorIdAsync(int id)
        {
            return await _context.Usuarios
                .Include(u => u.Rol)
                .FirstOrDefaultAsync(u => u.Id == id);
        }


        public async Task CrearAsync(Usuario usuario)
        {
            bool existeCorreo = await _context.Usuarios
                .AnyAsync(u => u.Correo == usuario.Correo);

            if (existeCorreo)
            {
                throw new InvalidOperationException(
                    "Ya existe un usuario con ese correo electrónico.");
            }

            _context.Usuarios.Add(usuario);

            await _context.SaveChangesAsync();
        }


        public async Task ActualizarAsync(Usuario usuario)
        {
            _context.Usuarios.Update(usuario);

            await _context.SaveChangesAsync();
        }



        public async Task EliminarAsync(int id)
        {
            var usuario = await _context.Usuarios.FindAsync(id);

            if (usuario != null)
            {
                _context.Usuarios.Remove(usuario);

                await _context.SaveChangesAsync();
            }
        }


        public async Task<bool> ExisteAsync(int id)
        {
            return await _context.Usuarios
                .AnyAsync(u => u.Id == id);
        }
    }
}