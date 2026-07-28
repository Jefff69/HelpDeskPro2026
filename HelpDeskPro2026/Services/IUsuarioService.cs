using HelpDeskPro2026.Models;

namespace HelpDeskPro2026.Services
{
    public interface IUsuarioService
    {
        Task<List<Usuario>> ObtenerTodosAsync();
        Task<Usuario?> ObtenerPorIdAsync(int id);
        Task CrearAsync(Usuario usuario);
        Task ActualizarAsync(Usuario usuario);
        Task EliminarAsync(int id);
        Task<bool> ExisteAsync(int id);
    }
}