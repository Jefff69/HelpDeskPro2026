using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HelpDeskPro2026.Models
{
    public class Ticket
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required]
        [StringLength(20)]
        public string CodigoTicket { get; set; } = string.Empty;

        [Required]
        [StringLength(200)]
        public string Asunto { get; set; } = string.Empty;

        [Required]
        public string Descripcion { get; set; } = string.Empty;

        public string? Justificacion { get; set; }

        // Catálogos
        public int SistemaId { get; set; }

        public int CategoriaId { get; set; }

        public int RiesgoId { get; set; }

        public int PrioridadId { get; set; }

        public int EstadoId { get; set; }

        // Usuarios
        public int SolicitanteId { get; set; }

        public int? TecnicoAsignadoId { get; set; }

        public DateTime FechaCreacion { get; set; } = DateTime.UtcNow;


        // Relaciones con catálogos

        [ForeignKey(nameof(SistemaId))]
        public Sistema? Sistema { get; set; }

        [ForeignKey(nameof(CategoriaId))]
        public Categoria? Categoria { get; set; }

        [ForeignKey(nameof(RiesgoId))]
        public Riesgo? Riesgo { get; set; }

        [ForeignKey(nameof(PrioridadId))]
        public Prioridad? Prioridad { get; set; }

        [ForeignKey(nameof(EstadoId))]
        public Estado? Estado { get; set; }


        // Relaciones con usuarios

        [ForeignKey(nameof(SolicitanteId))]
        public Usuario? Solicitante { get; set; }

        [ForeignKey(nameof(TecnicoAsignadoId))]
        public Usuario? TecnicoAsignado { get; set; }


        // Relaciones con comentarios y adjuntos

        public ICollection<Comentario> Comentarios { get; set; }
            = new List<Comentario>();

        public ICollection<Adjunto> ArchivosAdjuntos { get; set; }
            = new List<Adjunto>();
    }
}