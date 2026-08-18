using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HelpDeskPro2026.Models
{
    public class Adjunto
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(255)]
        public string NombreArchivo { get; set; } = string.Empty;

        [Required]
        [StringLength(500)]
        public string RutaArchivo { get; set; } = string.Empty;

        [StringLength(100)]
        public string TipoArchivo { get; set; } = string.Empty;

        public long TamanoBytes { get; set; }

        public Guid TicketId { get; set; }

        [ForeignKey(nameof(TicketId))]
        public Ticket? Ticket { get; set; }
    }
}