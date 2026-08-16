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
        Task<Usuario?> ObtenerPorSupabaseUserIdAsync(string supabaseUserId);
        Task ActualizarFotoAsync(int usuarioId, string fotoUrl);
        Task<string?> ObtenerFotoAsync(int usuarioId);
        Task ActualizarPerfilAsync(int usuarioId, string nombre, string apellidos);
    }
}