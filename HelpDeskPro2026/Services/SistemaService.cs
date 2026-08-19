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


        public async Task<List<Sistema>> ObtenerTodosAsync()
        {
            return await _context.Sistemas
                .ToListAsync();
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


            bool existeNombre = await _context.Sistemas
                .AnyAsync(s => s.Nombre == sistema.Nombre);

            if (existeNombre)
            {
                throw new InvalidOperationException(
                    "Ya existe un sistema con ese nombre.");
            }


            _context.Sistemas.Add(sistema);

            await _context.SaveChangesAsync();
        }


        public async Task ActualizarAsync(Sistema sistema)
        {
            bool existeCodigo = await _context.Sistemas
                .AnyAsync(s =>
                    s.Codigo == sistema.Codigo &&
                    s.Id != sistema.Id);

            if (existeCodigo)
            {
                throw new InvalidOperationException(
                    "Ya existe otro sistema con ese código.");
            }


            bool existeNombre = await _context.Sistemas
                .AnyAsync(s =>
                    s.Nombre == sistema.Nombre &&
                    s.Id != sistema.Id);

            if (existeNombre)
            {
                throw new InvalidOperationException(
                    "Ya existe otro sistema con ese nombre.");
            }


            _context.Sistemas.Update(sistema);

            await _context.SaveChangesAsync();
        }


        public async Task EliminarAsync(int id)
        {
            var sistema = await _context.Sistemas
                .FindAsync(id);

            if (sistema == null)
                return;

            var tieneTickets = await _context.Tickets
                .AnyAsync(t => t.SistemaId == id);

            if (tieneTickets)
            {
                throw new InvalidOperationException(
                    $"No se puede eliminar el sistema \"{sistema.Nombre}\" porque está asociado a uno o más tickets.");
            }

            _context.Sistemas.Remove(sistema);

            await _context.SaveChangesAsync();
        }


        public async Task<bool> ExisteAsync(int id)
        {
            return await _context.Sistemas
                .AnyAsync(s => s.Id == id);
        }

    }
}