using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HelpDeskPro2026.Models
{
    public class Usuario
    {
        [Key]
        public int Id { get; set; }

        // Identificador del usuario en Supabase Authentication
        public string? SupabaseUserId { get; set; }

        [Required(ErrorMessage = "El nombre es obligatorio.")]
        [StringLength(50, ErrorMessage = "El nombre no puede superar los 50 caracteres.")]
        public string Nombre { get; set; } = string.Empty;

        [Required(ErrorMessage = "Los apellidos son obligatorios.")]
        [StringLength(50, ErrorMessage = "Los apellidos no pueden superar los 50 caracteres.")]
        public string Apellidos { get; set; } = string.Empty;

        [Required(ErrorMessage = "El correo electrónico es obligatorio.")]
        [EmailAddress(ErrorMessage = "Debe ingresar un correo electrónico válido.")]
        [StringLength(100, ErrorMessage = "El correo no puede superar los 100 caracteres.")]
        public string Correo { get; set; } = string.Empty;

        [StringLength(255)]
        public string? FotoUrl { get; set; }

        public bool Activo { get; set; } = true;

        public DateTime FechaCreacion { get; set; } = DateTime.UtcNow;

        public DateTime? FechaActualizacion { get; set; }

        public DateTime? UltimoAcceso { get; set; }

        // Llave foránea del rol
        public int RolId { get; set; }

        [ForeignKey("RolId")]
        public Rol? Rol { get; set; }

        // Propiedad calculada
        [NotMapped]
        public string NombreCompleto =>
            $"{Nombre} {Apellidos}";

        // Tickets creados por el usuario
        public ICollection<Ticket> TicketsCreados { get; set; }
            = new List<Ticket>();

        // Tickets asignados al usuario como técnico
        public ICollection<Ticket> TicketsAsignados { get; set; }
            = new List<Ticket>();

        // Comentarios realizados por el usuario
        public ICollection<Comentario> Comentarios { get; set; }
            = new List<Comentario>();
    }
}