using HelpDeskPro2026.Data;
using HelpDeskPro2026.Models;
using Microsoft.EntityFrameworkCore;

namespace HelpDeskPro2026.Services
{
    public class RiesgoService : IRiesgoService
    {
        private readonly AppDbContext _context;

        public RiesgoService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Riesgo>> ObtenerTodosAsync()
        {
            return await _context.Riesgos.ToListAsync();
        }

        public async Task<Riesgo?> ObtenerPorIdAsync(int id)
        {
            return await _context.Riesgos
                .FirstOrDefaultAsync(r => r.Id == id);
        }

        public async Task CrearAsync(Riesgo riesgo)
        {
            bool existeNombre = await _context.Riesgos
                .AnyAsync(r => r.Nombre == riesgo.Nombre);

            if (existeNombre)
            {
                throw new InvalidOperationException(
                    "Ya existe un riesgo con ese nombre.");
            }

            _context.Riesgos.Add(riesgo);

            await _context.SaveChangesAsync();
        }

        public async Task ActualizarAsync(Riesgo riesgo)
        {
            _context.Riesgos.Update(riesgo);

            await _context.SaveChangesAsync();
        }



        public async Task EliminarAsync(int id)
        {
            var riesgo = await _context.Riesgos.FindAsync(id);

            if (riesgo == null)
                return;

            var tieneTickets = await _context.Tickets
                .AnyAsync(t => t.RiesgoId == id);

            if (tieneTickets)
            {
                throw new InvalidOperationException(
                    $"No se puede eliminar el riesgo \"{riesgo.Nombre}\" porque está asociado a uno o más tickets.");
            }

            _context.Riesgos.Remove(riesgo);

            await _context.SaveChangesAsync();
        }



        public async Task<bool> ExisteAsync(int id)
        {
            return await _context.Riesgos.AnyAsync(r => r.Id == id);
        }
    }
}