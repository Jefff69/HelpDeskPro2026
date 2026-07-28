using HelpDeskPro2026.Data;
using HelpDeskPro2026.Models;
using Microsoft.EntityFrameworkCore;

namespace HelpDeskPro2026.Services
{
    public class EstadoService : IEstadoService
    {
        private readonly AppDbContext _context;

        public EstadoService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Estado>> ObtenerTodosAsync()
        {
            return await _context.Estados.ToListAsync();
        }


        public async Task<Estado?> ObtenerPorIdAsync(int id)
        {
            return await _context.Estados
                .FirstOrDefaultAsync(c => c.Id == id);
        }


        public async Task CrearAsync(Estado estado)
        {
            bool existeNombre = await _context.Estados
                .AnyAsync(c => c.Nombre == estado.Nombre);

            if (existeNombre)
            {
                throw new InvalidOperationException(
                    "Ya existe un estado con ese nombre.");
            }

            _context.Estados.Add(estado);

            await _context.SaveChangesAsync();
        }



        public async Task ActualizarAsync(Estado estado)
        {
            _context.Estados.Update(estado);

            await _context.SaveChangesAsync();
        }



        public async Task EliminarAsync(int id)
        {
            var estado = await _context.Estados.FindAsync(id);

            if (estado != null)
            {
                _context.Estados.Remove(estado);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<bool> ExisteAsync(int id)
        {
            return await _context.Estados.AnyAsync(c => c.Id == id);
        }





    }
}