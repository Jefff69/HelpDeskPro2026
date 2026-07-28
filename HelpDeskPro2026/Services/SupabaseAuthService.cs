using HelpDeskPro2026.Interfaces;
using Supabase.Gotrue;

namespace HelpDeskPro2026.Services
{
    public class SupabaseAuthService : ISupabaseAuthService
    {
        private readonly SupabaseClientService _supabaseClient;

        public SupabaseAuthService(SupabaseClientService supabaseClient)
        {
            _supabaseClient = supabaseClient;
        }

        public async Task<User?> SignInAsync(string email, string password)
        {
            var client = _supabaseClient.GetClient();

            var session = await client.Auth.SignIn(email, password);

            return session?.User;
        }

        public async Task SignOutAsync()
        {
            var client = _supabaseClient.GetClient();

            await client.Auth.SignOut();
        }


        public User? GetCurrentUser()
        {
            var client = _supabaseClient.GetClient();

            return client.Auth.CurrentUser;
        }
    }
}