using HelpDeskPro2026.Data;
using HelpDeskPro2026.Interfaces;
using HelpDeskPro2026.Models;
using Microsoft.EntityFrameworkCore;

namespace HelpDeskPro2026.Services
{
    public class TicketService : ITicketService
    {
        private readonly AppDbContext _context;

        public TicketService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<string> GenerarCodigoTicketAsync(int sistemaId)
        {
            var sistema = await _context.Sistemas.FindAsync(sistemaId);

            if (sistema == null)
                return "TICK-00001";

            var ultimo = await _context.Tickets
                .Where(t => t.SistemaId == sistemaId)
                .OrderByDescending(t => t.CodigoTicket)
                .Select(t => t.CodigoTicket)
                .FirstOrDefaultAsync();

            int numero = 1;

            if (!string.IsNullOrEmpty(ultimo) && ultimo.Contains("-"))
            {
                if (int.TryParse(ultimo.Split('-')[1], out int n))
                    numero = n + 1;
            }

            return $"{sistema.Codigo}-{numero:D5}";
        }

        public async Task<Usuario?> AsignarTecnicoMasLibreAsync()
        {
            return await _context.Usuarios
                .Where(u => u.RolId == 2 && u.Activo)
                .OrderBy(u => _context.Tickets
                    .Count(t => t.TecnicoAsignadoId == u.Id
                             && t.Estado != null
                             && t.Estado.Nombre != "Finalizado"
                             && t.Estado.Nombre != "Cancelado"))
                .FirstOrDefaultAsync();
        }
    }
}