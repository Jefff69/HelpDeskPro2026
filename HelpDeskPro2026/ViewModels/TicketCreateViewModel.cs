using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace HelpDeskPro2026.Models.ViewModels
{
    public class TicketCreateViewModel
    {
        [Required(ErrorMessage = "El asunto es obligatorio.")]
        public string Asunto { get; set; } = string.Empty;

        [Required(ErrorMessage = "La descripción es obligatoria.")]
        public string Descripcion { get; set; } = string.Empty;

        public string? Justificacion { get; set; }

        [Required(ErrorMessage = "Debe seleccionar un sistema.")]
        public int SistemaId { get; set; }

        [Required(ErrorMessage = "Debe seleccionar una categoría.")]
        public int CategoriaId { get; set; }

        [Required(ErrorMessage = "Debe seleccionar un riesgo.")]
        public int RiesgoId { get; set; }

        [Required(ErrorMessage = "Debe seleccionar una prioridad.")]
        public int PrioridadId { get; set; }

        public List<IFormFile>? ArchivosAdjuntos { get; set; }

        public SelectList? Sistemas { get; set; }

        public SelectList? Categorias { get; set; }

        public SelectList? Riesgos { get; set; }

        public SelectList? Prioridades { get; set; }
    }
}