using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace HelpDeskPro2026.Models.ViewModels
{
    public class TicketEditViewModel
    {
        public Guid Id { get; set; }

        public string CodigoTicket { get; set; } = string.Empty;


        // Catálogos

        [Required(ErrorMessage = "Seleccione un sistema")]
        public int SistemaId { get; set; }

        [Required(ErrorMessage = "Seleccione una categoría")]
        public int CategoriaId { get; set; }

        [Required(ErrorMessage = "Seleccione un riesgo")]
        public int RiesgoId { get; set; }

        [Required(ErrorMessage = "Seleccione una prioridad")]
        public int PrioridadId { get; set; }

        [Required(ErrorMessage = "Seleccione el estado del ticket")]
        public int EstadoId { get; set; }


        // Información del ticket

        [Required(ErrorMessage = "El asunto es obligatorio")]
        [MaxLength(200)]
        public string Asunto { get; set; } = string.Empty;

        [Required(ErrorMessage = "La descripción es obligatoria")]
        public string Descripcion { get; set; } = string.Empty;

        public string? Justificacion { get; set; }


        // Listas para los controles de selección

        public IEnumerable<SelectListItem> Sistemas { get; set; }
            = new List<SelectListItem>();

        public IEnumerable<SelectListItem> Categorias { get; set; }
            = new List<SelectListItem>();

        public IEnumerable<SelectListItem> Riesgos { get; set; }
            = new List<SelectListItem>();

        public IEnumerable<SelectListItem> Prioridades { get; set; }
            = new List<SelectListItem>();

        public IEnumerable<SelectListItem> Estados { get; set; }
            = new List<SelectListItem>();
    }
}