using Microsoft.AspNetCore.Mvc.Rendering;

namespace HelpDeskPro2026.Models.ViewModels
{
    public class TicketDetailViewModel
    {
        public Guid Id { get; set; }

        public string CodigoTicket { get; set; } = string.Empty;

        public string Asunto { get; set; } = string.Empty;

        public string Descripcion { get; set; } = string.Empty;

        public string? Justificacion { get; set; }


        // Información de los catálogos

        public string Sistema { get; set; } = string.Empty;

        public string Categoria { get; set; } = string.Empty;

        public string Prioridad { get; set; } = string.Empty;

        public string Riesgo { get; set; } = string.Empty;

        public string ColorRiesgo { get; set; } = string.Empty;

        public string Estado { get; set; } = string.Empty;


        // Usuarios relacionados

        public string Solicitante { get; set; } = string.Empty;

        public string? TecnicoAsignado { get; set; }

        public string? UrlFotoTecnico { get; set; }


        public DateTime FechaCreacion { get; set; }


        // Control de cambio de estado

        public bool PuedeCambiarEstado { get; set; }

        public List<SelectListItem> EstadosPermitidos { get; set; } = new();


        // Comentarios y archivos

        public List<ComentarioItemViewModel> Comentarios { get; set; } = new();

        public List<AdjuntoItemViewModel> Adjuntos { get; set; } = new();
    }


    public class ComentarioItemViewModel
    {
        public string Texto { get; set; } = string.Empty;

        public string NombreUsuario { get; set; } = string.Empty;

        public string? UrlFotoUsuario { get; set; }

        public DateTime Fecha { get; set; }
    }


    public class AdjuntoItemViewModel
    {
        public string NombreArchivo { get; set; } = string.Empty;

        public string UrlArchivo { get; set; } = string.Empty;
    }
}