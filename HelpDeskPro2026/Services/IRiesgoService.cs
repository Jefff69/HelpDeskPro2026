using HelpDeskPro2026.Models;

namespace HelpDeskPro2026.Services
{
    public interface IRiesgoService
    {
        Task<List<Riesgo>> ObtenerTodosAsync();

        Task<Riesgo?> ObtenerPorIdAsync(int id);

        Task CrearAsync(Riesgo riesgo);

        Task ActualizarAsync(Riesgo riesgo);

        Task EliminarAsync(int id);

        Task<bool> ExisteAsync(int id);
    }
}