using HelpDeskPro2026.Models.Configuration;
using Microsoft.Extensions.Options;
using Supabase;

namespace HelpDeskPro2026.Services
{
    public class SupabaseClientService
    {
        private readonly Client _authClient;
        private readonly Client _storageClient;

        public SupabaseClientService(IOptions<SupabaseSettings> options)
        {
            var settings = options.Value;

            // Cliente para Authentication
            _authClient = new Client(
                settings.Url,
                settings.AnonKey);

            _authClient.InitializeAsync().Wait();

            // Cliente para Storage
            _storageClient = new Client(
                settings.Url,
                settings.ServiceRoleKey);

            _storageClient.InitializeAsync().Wait();
        }

        public Client GetAuthClient()
        {
            return _authClient;
        }

        public Client GetStorageClient()
        {
            return _storageClient;
        }
    }
}