using HelpDeskPro2026.Data;
using HelpDeskPro2026.Models;
using Microsoft.EntityFrameworkCore;

namespace HelpDeskPro2026.Services
{
    public class CategoriaService : ICategoriaService
    {
        private readonly AppDbContext _context;

        public CategoriaService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Categoria>> ObtenerTodosAsync()
        {
            return await _context.Categorias.ToListAsync();
        }


        public async Task<Categoria?> ObtenerPorIdAsync(int id)
        {
            return await _context.Categorias
                .FirstOrDefaultAsync(c => c.Id == id);
        }


        public async Task CrearAsync(Categoria categoria)
        {
            bool existeNombre = await _context.Categorias
                .AnyAsync(c => c.Nombre == categoria.Nombre);

            if (existeNombre)
            {
                throw new InvalidOperationException(
                    "Ya existe una categoría con ese nombre.");
            }

            _context.Categorias.Add(categoria);

            await _context.SaveChangesAsync();
        }



        public async Task ActualizarAsync(Categoria categoria)
        {
            _context.Categorias.Update(categoria);

            await _context.SaveChangesAsync();
        }



        public async Task EliminarAsync(int id)
        {
            var categoria = await _context.Categorias.FindAsync(id);

            if (categoria != null)
            {
                _context.Categorias.Remove(categoria);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<bool> ExisteAsync(int id)
        {
            return await _context.Categorias.AnyAsync(c => c.Id == id);
        }





    }
}