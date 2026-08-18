namespace HelpDeskPro2026.Interfaces
{
    public interface IStorageService
    {
        Task<string?> UploadProfileImageAsync(
            IFormFile file,
            string fileName);
    }
}