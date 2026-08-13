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
            var client = _supabaseClient.GetAuthClient();

            var session = await client.Auth.SignIn(email, password);

            return session?.User;
        }

        public async Task SignOutAsync()
        {
            var client = _supabaseClient.GetAuthClient();

            await client.Auth.SignOut();
        }


        public User? GetCurrentUser()
        {
            var client = _supabaseClient.GetAuthClient();

            return client.Auth.CurrentUser;
        }

        public async Task<string?> CreateUserAsync(
            string email,
            string password)
        {
            try
            {
                var client = _supabaseClient.GetAuthClient();

                var session = await client.Auth.SignUp(
                    Constants.SignUpType.Email,
                    email,
                    password);

                return session?.User?.Id;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException(
                    $"No fue posible crear el usuario en Supabase. {ex.Message}");
            }
        }




    }
}