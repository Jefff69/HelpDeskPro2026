using HelpDeskPro2026.Data;
using HelpDeskPro2026.Models;
using Microsoft.EntityFrameworkCore;

namespace HelpDeskPro2026.Services
{
    public class PrioridadService : IPrioridadService
    {
        private readonly AppDbContext _context;

        public PrioridadService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Prioridad>> ObtenerTodosAsync()
        {
            return await _context.Prioridades.ToListAsync();
        }

        public async Task<Prioridad?> ObtenerPorIdAsync(int id)
        {
            return await _context.Prioridades
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task CrearAsync(Prioridad prioridad)
        {
            bool existeNombre = await _context.Prioridades
                .AnyAsync(p => p.Nombre == prioridad.Nombre);

            if (existeNombre)
            {
                throw new InvalidOperationException(
                    "Ya existe una prioridad con ese nombre.");
            }

            _context.Prioridades.Add(prioridad);

            await _context.SaveChangesAsync();
        }

        public async Task ActualizarAsync(Prioridad prioridad)
        {
            _context.Prioridades.Update(prioridad);

            await _context.SaveChangesAsync();
        }



        public async Task EliminarAsync(int id)
        {
            var prioridad = await _context.Prioridades.FindAsync(id);

            if (prioridad == null)
                return;

            var tieneTickets = await _context.Tickets
                .AnyAsync(t => t.PrioridadId == id);

            if (tieneTickets)
            {
                throw new InvalidOperationException(
                    $"No se puede eliminar la prioridad \"{prioridad.Nombre}\" porque está asociada a uno o más tickets.");
            }

            _context.Prioridades.Remove(prioridad);

            await _context.SaveChangesAsync();
        }




        public async Task<bool> ExisteAsync(int id)
        {
            return await _context.Prioridades.AnyAsync(p => p.Id == id);
        }
    }
}