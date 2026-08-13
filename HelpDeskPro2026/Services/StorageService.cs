using HelpDeskPro2026.Interfaces;

namespace HelpDeskPro2026.Services
{
    public class StorageService : IStorageService
    {
        private readonly SupabaseClientService _supabaseClient;

        public StorageService(SupabaseClientService supabaseClient)
        {
            _supabaseClient = supabaseClient;
        }

        public async Task<string?> UploadProfileImageAsync(
            IFormFile file,
            string fileName)
        {
        var client = _supabaseClient.GetStorageClient();

            using var memoryStream = new MemoryStream();

            await file.CopyToAsync(memoryStream);

            byte[] bytes = memoryStream.ToArray();

            await client.Storage
                .From("profile-images")
                .Upload(
                    bytes,
                    fileName,
                    new Supabase.Storage.FileOptions
                    {
                        Upsert = true
                    });

            return client.Storage
                .From("profile-images")
                .GetPublicUrl(fileName);
        }


    }
}