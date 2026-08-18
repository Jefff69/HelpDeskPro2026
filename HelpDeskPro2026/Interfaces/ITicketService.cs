using HelpDeskPro2026.Models;

namespace HelpDeskPro2026.Interfaces
{
    public interface ITicketService
    {
        Task<string> GenerarCodigoTicketAsync(int sistemaId);

        Task<Usuario?> AsignarTecnicoMasLibreAsync();
    }
}