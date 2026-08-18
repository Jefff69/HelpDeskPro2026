using Microsoft.AspNetCore.Mvc.Rendering;

namespace HelpDeskPro2026.Models.ViewModels
{
    public class TicketFilterViewModel
    {
        public int? SistemaId { get; set; }

        public int? EstadoId { get; set; }

        public int? SolicitanteId { get; set; }


        public IEnumerable<SelectListItem> Sistemas { get; set; }
            = new List<SelectListItem>();

        public IEnumerable<SelectListItem> Estados { get; set; }
            = new List<SelectListItem>();


        public List<TicketListItemViewModel> Tickets { get; set; }
            = new();
    }
}