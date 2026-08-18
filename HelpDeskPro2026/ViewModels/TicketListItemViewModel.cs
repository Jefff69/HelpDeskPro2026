namespace HelpDeskPro2026.Models.ViewModels
{
    public class TicketListItemViewModel
    {
        public Guid Id { get; set; }

        public string CodigoTicket { get; set; } = string.Empty;

        public string Asunto { get; set; } = string.Empty;

        public string NombreSistema { get; set; } = string.Empty;

        public string NombreEstado { get; set; } = string.Empty;

        public string NombrePrioridad { get; set; } = string.Empty;

        public string NombreRiesgo { get; set; } = string.Empty;

        public string ColorRiesgo { get; set; } = "#cccccc";

        public DateTime FechaCreacion { get; set; }

        public string NombreSolicitante { get; set; } = string.Empty;

        public string NombreTecnico { get; set; } = "Sin asignar";
    }
}