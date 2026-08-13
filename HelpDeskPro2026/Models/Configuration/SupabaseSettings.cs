namespace HelpDeskPro2026.Models.Configuration
{
    public class SupabaseSettings
    {
        public string Url { get; set; } = string.Empty;

        // Para Authentication
        public string AnonKey { get; set; } = string.Empty;

        // Para Storage (Service Role)
        public string ServiceRoleKey { get; set; } = string.Empty;
    }
}