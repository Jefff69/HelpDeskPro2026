using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace HelpDeskPro2026.ViewModels
{
    public class PerfilViewModel
    {
        public int Id { get; set; }

        [Required]
        public string Nombre { get; set; } = string.Empty;

        [Required]
        public string Apellidos { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        public string Correo { get; set; } = string.Empty;

        public string? Rol { get; set; }

        public bool Activo { get; set; }

        public string? FotoUrl { get; set; }

        public IFormFile? Foto { get; set; }
    }
}