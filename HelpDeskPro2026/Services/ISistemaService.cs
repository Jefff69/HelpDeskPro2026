using HelpDeskPro2026.Models;

namespace HelpDeskPro2026.Services
{
    public interface ISistemaService
    {
        Task<List<Sistema>> ObtenerTodosAsync();

        Task<Sistema?> ObtenerPorIdAsync(int id);

        Task CrearAsync(Sistema sistema);

        Task ActualizarAsync(Sistema sistema);

        Task EliminarAsync(int id);

        Task<bool> ExisteAsync(int id);
    }
}