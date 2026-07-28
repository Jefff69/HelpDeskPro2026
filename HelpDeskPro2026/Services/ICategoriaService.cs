using HelpDeskPro2026.Models;

namespace HelpDeskPro2026.Services
{
    public interface ICategoriaService
    {
        Task<List<Categoria>> ObtenerTodosAsync();

        Task<Categoria?> ObtenerPorIdAsync(int id);

        Task CrearAsync(Categoria categoria);

        Task ActualizarAsync(Categoria categoria);

        Task EliminarAsync(int id);

        Task<bool> ExisteAsync(int id);
    }
}