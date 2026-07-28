using HelpDeskPro2026.Models;

namespace HelpDeskPro2026.Services
{
    public interface IPrioridadService
    {
        Task<List<Prioridad>> ObtenerTodosAsync();

        Task<Prioridad?> ObtenerPorIdAsync(int id);

        Task CrearAsync(Prioridad prioridad);

        Task ActualizarAsync(Prioridad prioridad);

        Task EliminarAsync(int id);

        Task<bool> ExisteAsync(int id);
    }
}