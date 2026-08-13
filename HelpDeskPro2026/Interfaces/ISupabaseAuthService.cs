using Supabase.Gotrue;

namespace HelpDeskPro2026.Interfaces
{
    public interface ISupabaseAuthService
    {
        Task<User?> SignInAsync(string email, string password);

        Task SignOutAsync();

        User? GetCurrentUser();

        Task<string?> CreateUserAsync(string email, string password);
    }
}