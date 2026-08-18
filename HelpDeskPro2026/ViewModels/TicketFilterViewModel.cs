using Microsoft.AspNetCore.Mvc.Rendering;

namespace HelpDeskPro2026.Models.ViewModels
{
    public class TicketFilterViewModel
    {
        // Filtros
        public int? SistemaId { get; set; }

        public int? EstadoId { get; set; }

        public int? SolicitanteId { get; set; }

        public DateTime? FechaDesde { get; set; }

        public DateTime? FechaHasta { get; set; }


        // Listas para los filtros
        public IEnumerable<SelectListItem> Sistemas { get; set; }
            = new List<SelectListItem>();

        public IEnumerable<SelectListItem> Estados { get; set; }
            = new List<SelectListItem>();

        public IEnumerable<SelectListItem> Solicitantes { get; set; }
            = new List<SelectListItem>();


        // Resultados
        public List<TicketListItemViewModel> Tickets { get; set; }
            = new();
    }
}