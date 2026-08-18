using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HelpDeskPro2026.Models
{
    public class Comentario
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        public Guid TicketId { get; set; }

        public int UsuarioId { get; set; }

        [Required]
        public string Texto { get; set; } = string.Empty;

        public DateTime Fecha { get; set; } = DateTime.UtcNow;


        [ForeignKey(nameof(TicketId))]
        public Ticket? Ticket { get; set; }

        [ForeignKey(nameof(UsuarioId))]
        public Usuario? Usuario { get; set; }
    }
}