using HelpDeskPro2026.Models;

namespace HelpDeskPro2026.Services
{
    public interface IEstadoService
    {
        Task<List<Estado>> ObtenerTodosAsync();

        Task<Estado?> ObtenerPorIdAsync(int id);

        Task CrearAsync(Estado estado);

        Task ActualizarAsync(Estado estado);

        Task EliminarAsync(int id);

        Task<bool> ExisteAsync(int id);
    }
}