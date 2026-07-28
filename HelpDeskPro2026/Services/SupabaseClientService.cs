using HelpDeskPro2026.Models.Configuration;
using Microsoft.Extensions.Options;
using Supabase;

namespace HelpDeskPro2026.Services
{
    public class SupabaseClientService
    {
        private readonly Client _client;

        public SupabaseClientService(IOptions<SupabaseSettings> options)
        {
            var settings = options.Value;

            _client = new Client(
                settings.Url,
                settings.Key);

            _client.InitializeAsync().Wait();
        }

        public Client GetClient()
        {
            return _client;
        }
    }
}