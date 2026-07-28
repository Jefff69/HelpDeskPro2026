using HelpDeskPro2026.Data;
using HelpDeskPro2026.Models;
using Microsoft.EntityFrameworkCore;

namespace HelpDeskPro2026.Services
{
    public class SistemaService : ISistemaService
    {
        private readonly AppDbContext _context;

        public SistemaService(AppDbContext context)
        {
            _context = context;
        }

        // Aquí comienza el primer método
        public async Task<List<Sistema>> ObtenerTodosAsync()
        {
            return await _context.Sistemas.ToListAsync();
        }

        public async Task<Sistema?> ObtenerPorIdAsync(int id)
        {
            return await _context.Sistemas
                .FirstOrDefaultAsync(s => s.Id == id);
        }


        public async Task CrearAsync(Sistema sistema)
        {
            bool existeCodigo = await _context.Sistemas
                .AnyAsync(s => s.Codigo == sistema.Codigo);

            if (existeCodigo)
            {
                throw new InvalidOperationException(
                    "Ya existe un sistema con ese código.");
            }

            _context.Sistemas.Add(sistema);

            await _context.SaveChangesAsync();
        }


        public async Task ActualizarAsync(Sistema sistema)
        {
            _context.Sistemas.Update(sistema);
            await _context.SaveChangesAsync();
        }


        public async Task EliminarAsync(int id)
        {
            var sistema = await _context.Sistemas.FindAsync(id);

            if (sistema != null)
            {
                _context.Sistemas.Remove(sistema);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<bool> ExisteAsync(int id)
        {
            return await _context.Sistemas.AnyAsync(s => s.Id == id);
        }

    }
}