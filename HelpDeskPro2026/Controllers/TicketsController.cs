using HelpDeskPro2026.Data;
using HelpDeskPro2026.Models;
using HelpDeskPro2026.Models.ViewModels;
using HelpDeskPro2026.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace HelpDeskPro2026.Controllers
{
    [Authorize]
    public class TicketsController : Controller
    {
        private readonly AppDbContext _context;
        private readonly ITicketService _ticketService;

        public TicketsController(
            AppDbContext context,
            ITicketService ticketService)
        {
            _context = context;
            _ticketService = ticketService;
        }


        private string ObtenerColorRiesgo(string? riesgo)
        {
            if (string.IsNullOrWhiteSpace(riesgo))
                return "#6c757d";

            return riesgo.Trim().ToLower() switch
            {
                "bajo" => "#198754",
                "medio" => "#ffc107",
                "alto" => "#dc3545",
                "crítico" => "#8b0000",
                "critico" => "#8b0000",
                _ => "#6c757d"
            };
        }


        public async Task<IActionResult> Index(TicketFilterViewModel filtro)
        {
            var query = _context.Tickets
                .Include(t => t.Sistema)
                .Include(t => t.Categoria)
                .Include(t => t.Riesgo)
                .Include(t => t.Prioridad)
                .Include(t => t.Estado)
                .Include(t => t.Solicitante)
                .Include(t => t.TecnicoAsignado)
                .AsQueryable();

            // Filtro por sistema
            if (filtro.SistemaId.HasValue)
            {
                query = query.Where(t => t.SistemaId == filtro.SistemaId.Value);
            }

            // Filtro por estado
            if (filtro.EstadoId.HasValue)
            {
                query = query.Where(t => t.EstadoId == filtro.EstadoId.Value);
            }

            // Filtro por solicitante
            if (filtro.SolicitanteId.HasValue)
            {
                query = query.Where(t =>
                    t.SolicitanteId == filtro.SolicitanteId.Value);
            }

            // Filtro por fecha desde
            if (filtro.FechaDesde.HasValue)
            {
                var fechaDesde = filtro.FechaDesde.Value.Date;

                query = query.Where(t =>
                    t.FechaCreacion >= fechaDesde);
            }

            // Filtro por fecha hasta
            if (filtro.FechaHasta.HasValue)
            {
                var fechaHasta = filtro.FechaHasta.Value.Date.AddDays(1);

                query = query.Where(t =>
                    t.FechaCreacion < fechaHasta);
            }

            filtro.Tickets = await query
                .OrderByDescending(t => t.FechaCreacion)
                .Select(t => new TicketListItemViewModel
                {
                    Id = t.Id,
                    CodigoTicket = t.CodigoTicket,
                    Asunto = t.Asunto,

                    NombreSistema = t.Sistema != null
                        ? t.Sistema.Nombre
                        : "Sin sistema",

                    NombreEstado = t.Estado != null
                        ? t.Estado.Nombre
                        : "Sin estado",

                    NombrePrioridad = t.Prioridad != null
                        ? t.Prioridad.Nombre
                        : "Sin prioridad",

                    NombreRiesgo = t.Riesgo != null
                        ? t.Riesgo.Nombre
                        : "Sin riesgo",

                    // Temporalmente se asigna después
                    ColorRiesgo = "#6c757d",

                    FechaCreacion = t.FechaCreacion,

                    NombreSolicitante = t.Solicitante != null
                        ? t.Solicitante.NombreCompleto
                        : "Sin solicitante",

                    NombreTecnico = t.TecnicoAsignado != null
                        ? t.TecnicoAsignado.NombreCompleto
                        : "Sin asignar"
                })
                .ToListAsync();

            foreach (var ticket in filtro.Tickets)
            {
                ticket.ColorRiesgo = ObtenerColorRiesgo(ticket.NombreRiesgo);
            }


            // Cargar filtros
            filtro.Sistemas = new SelectList(
                await _context.Sistemas
                    .Where(s => s.Activo)
                    .OrderBy(s => s.Nombre)
                    .ToListAsync(),
                "Id",
                "Nombre");

            filtro.Estados = new SelectList(
                await _context.Estados
                    .OrderBy(e => e.Nombre)
                    .ToListAsync(),
                "Id",
                "Nombre");

       


            filtro.Solicitantes = new SelectList(
                await _context.Usuarios
                    .Where(u => u.Activo)
                    .OrderBy(u => u.Nombre)
                    .ThenBy(u => u.Apellidos)
                    .Select(u => new
                    {
                        Id = u.Id,
                        NombreCompleto = u.Nombre + " " + u.Apellidos
                    })
                    .ToListAsync(),
                "Id",
                "NombreCompleto");

            return View(filtro);


        }



        [HttpGet]
        public async Task<IActionResult> Ver(Guid id)
        {
            var ticket = await _context.Tickets
                .Include(t => t.Sistema)
                .Include(t => t.Categoria)
                .Include(t => t.Riesgo)
                .Include(t => t.Prioridad)
                .Include(t => t.Estado)
                .Include(t => t.Solicitante)
                .Include(t => t.TecnicoAsignado)
                .Include(t => t.ArchivosAdjuntos)
                .Include(t => t.Comentarios)
                    .ThenInclude(c => c.Usuario)
                .FirstOrDefaultAsync(t => t.Id == id);

            if (ticket == null)
            {
                return NotFound();
            }

            var vm = new TicketDetailViewModel
            {
                Id = ticket.Id,
                CodigoTicket = ticket.CodigoTicket,
                Asunto = ticket.Asunto,
                Descripcion = ticket.Descripcion,
                Justificacion = ticket.Justificacion,

                Sistema = ticket.Sistema?.Nombre ?? "Sin sistema",
                Categoria = ticket.Categoria?.Nombre ?? "Sin categoría",
                Prioridad = ticket.Prioridad?.Nombre ?? "Sin prioridad",
                Riesgo = ticket.Riesgo?.Nombre ?? "Sin riesgo",

                ColorRiesgo = ObtenerColorRiesgo(
                    ticket.Riesgo?.Nombre
                ),

                Estado = ticket.Estado?.Nombre ?? "Sin estado",

                Solicitante = ticket.Solicitante?.NombreCompleto ?? "Sin solicitante",

                TecnicoAsignado = ticket.TecnicoAsignado?.NombreCompleto
                    ?? "Sin asignar",

                FechaCreacion = ticket.FechaCreacion,

                Adjuntos = ticket.ArchivosAdjuntos
                    .Select(a => new AdjuntoItemViewModel
                    {
                        NombreArchivo = a.NombreArchivo,
                        UrlArchivo = "/" + a.RutaArchivo
                    })
                    .ToList(),

                Comentarios = ticket.Comentarios
                    .OrderBy(c => c.Fecha)
                    .Select(c => new ComentarioItemViewModel
                    {
                        NombreUsuario = c.Usuario?.NombreCompleto
                            ?? "Usuario",

                        UrlFotoUsuario = string.IsNullOrWhiteSpace(c.Usuario?.FotoUrl)
                        ? "/img/default-user.png"
                        : c.Usuario.FotoUrl,

                        Texto = c.Texto,
                        Fecha = c.Fecha
                    })
                    .ToList()
            };

            return View("Details", vm);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AgregarComentario(
    Guid id,
    string TextoComentario)
        {
            if (string.IsNullOrWhiteSpace(TextoComentario))
            {
                return RedirectToAction(nameof(Ver), new { id });
            }

            var usuarioIdClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (!int.TryParse(usuarioIdClaim, out int usuarioId))
            {
                return Unauthorized();
            }

            var ticketExiste = await _context.Tickets
                .AnyAsync(t => t.Id == id);

            if (!ticketExiste)
            {
                return NotFound();
            }

            var comentario = new Comentario
            {
                TicketId = id,
                UsuarioId = usuarioId,
                Texto = TextoComentario.Trim(),
                Fecha = DateTime.UtcNow
            };

            _context.Comentarios.Add(comentario);

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Ver), new { id });
        }





        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var model = new TicketCreateViewModel();

            await CargarCombosCrear(model);

            return View(model);
        }


        private async Task CargarCombosCrear(TicketCreateViewModel model)
        {
            model.Sistemas = new SelectList(
                await _context.Sistemas
                    .Where(s => s.Activo)
                    .OrderBy(s => s.Nombre)
                    .ToListAsync(),
                "Id",
                "Nombre");

            model.Categorias = new SelectList(
                await _context.Categorias
                    .OrderBy(c => c.Nombre)
                    .ToListAsync(),
                "Id",
                "Nombre");

            model.Riesgos = new SelectList(
                await _context.Riesgos
                    .OrderBy(r => r.Nombre)
                    .ToListAsync(),
                "Id",
                "Nombre");

            model.Prioridades = new SelectList(
                await _context.Prioridades
                    .OrderBy(p => p.Nombre)
                    .ToListAsync(),
                "Id",
                "Nombre");
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(TicketCreateViewModel model)
        {
            if (!ModelState.IsValid)
            {
                await CargarCombosCrear(model);
                return View(model);
            }

            try
            {
                // ==========================================
                // 1. OBTENER USUARIO AUTENTICADO
                // ==========================================

                var usuarioIdClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);

                if (!int.TryParse(usuarioIdClaim, out int usuarioId))
                {
                    ModelState.AddModelError(
                        "",
                        "No se pudo identificar al usuario autenticado.");

                    await CargarCombosCrear(model);
                    return View(model);
                }

                var usuario = await _context.Usuarios
                    .FirstOrDefaultAsync(u => u.Id == usuarioId && u.Activo);

                if (usuario == null)
                {
                    ModelState.AddModelError(
                        "",
                        "El usuario actual no existe o se encuentra inactivo.");

                    await CargarCombosCrear(model);
                    return View(model);
                }


                // ==========================================
                // 2. VALIDAR SISTEMA
                // ==========================================

                var sistema = await _context.Sistemas
                    .FirstOrDefaultAsync(s =>
                        s.Id == model.SistemaId &&
                        s.Activo);

                if (sistema == null)
                {
                    ModelState.AddModelError(
                        nameof(model.SistemaId),
                        "El sistema seleccionado no es válido.");

                    await CargarCombosCrear(model);
                    return View(model);
                }


                // ==========================================
                // 3. OBTENER ESTADO INICIAL
                // ==========================================

                var estado = await _context.Estados
                    .OrderBy(e => e.Id)
                    .FirstOrDefaultAsync();

                if (estado == null)
                {
                    ModelState.AddModelError(
                        "",
                        "No existen estados configurados en el sistema.");

                    await CargarCombosCrear(model);
                    return View(model);
                }


                // ==========================================
                // 4. GENERAR CÓDIGO DEL TICKET
                // ==========================================

                var codigo = await _ticketService
                    .GenerarCodigoTicketAsync(model.SistemaId);


                // ==========================================
                // 5. ASIGNAR TÉCNICO AUTOMÁTICAMENTE
                // ==========================================

                var tecnico = await _ticketService
                    .AsignarTecnicoMasLibreAsync();


                // ==========================================
                // 6. CREAR TICKET
                // ==========================================

                var nuevoTicket = new Ticket
                {
                    CodigoTicket = codigo,
                    Asunto = model.Asunto,
                    Descripcion = model.Descripcion,
                    Justificacion = model.Justificacion,

                    SistemaId = model.SistemaId,
                    CategoriaId = model.CategoriaId,
                    RiesgoId = model.RiesgoId,
                    PrioridadId = model.PrioridadId,
                    EstadoId = estado.Id,

                    SolicitanteId = usuario.Id,
                    TecnicoAsignadoId = tecnico?.Id,

                    FechaCreacion = DateTime.UtcNow
                };

                _context.Tickets.Add(nuevoTicket);

                await _context.SaveChangesAsync();


                // ==========================================
                // 7. GUARDAR ARCHIVOS ADJUNTOS
                // ==========================================

                if (model.ArchivosAdjuntos != null &&
                    model.ArchivosAdjuntos.Any())
                {
                    var rutaBase = Path.Combine(
                        Directory.GetCurrentDirectory(),
                        "wwwroot",
                        "archivos",
                        "tickets");

                    if (!Directory.Exists(rutaBase))
                    {
                        Directory.CreateDirectory(rutaBase);
                    }

                    foreach (var archivo in model.ArchivosAdjuntos)
                    {
                        if (archivo == null || archivo.Length <= 0)
                            continue;

                        var nombreUnico =
                            $"{Guid.NewGuid()}_{Path.GetFileName(archivo.FileName)}";

                        var rutaCompleta =
                            Path.Combine(rutaBase, nombreUnico);

                        await using var flujo =
                            new FileStream(
                                rutaCompleta,
                                FileMode.Create);

                        await archivo.CopyToAsync(flujo);

                        var adjunto = new Adjunto
                        {
                            NombreArchivo = archivo.FileName,
                            RutaArchivo = Path.Combine(
                                "archivos",
                                "tickets",
                                nombreUnico),
                            TipoArchivo = archivo.ContentType,
                            TamanoBytes = archivo.Length,
                            TicketId = nuevoTicket.Id
                        };

                        _context.Adjuntos.Add(adjunto);
                    }

                    await _context.SaveChangesAsync();
                }


                // ==========================================
                // 8. REGRESAR AL LISTADO
                // ==========================================

                return RedirectToAction(nameof(Index));
            }
            catch (DbUpdateException ex)
            {
                var detalle = ex.InnerException?.Message ?? ex.Message;

                ModelState.AddModelError(
                    "",
                    $"Error al guardar el ticket: {detalle}");

                await CargarCombosCrear(model);
                return View(model);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(
                    "",
                    $"Ocurrió un error: {ex.Message}");

                await CargarCombosCrear(model);
                return View(model);
            }
        }



        [HttpGet]
        [Authorize(Roles = "Super Usuario,Administrador,Técnico")]
        public async Task<IActionResult> Editar(Guid id)
        {
            var ticket = await _context.Tickets
                .FirstOrDefaultAsync(t => t.Id == id);

            if (ticket == null)
                return NotFound();

            var model = new TicketEditViewModel
            {
                Id = ticket.Id,
                CodigoTicket = ticket.CodigoTicket,
                Asunto = ticket.Asunto,
                Descripcion = ticket.Descripcion,
                Justificacion = ticket.Justificacion,
                SistemaId = ticket.SistemaId,
                CategoriaId = ticket.CategoriaId,
                RiesgoId = ticket.RiesgoId,
                PrioridadId = ticket.PrioridadId,
                EstadoId = ticket.EstadoId
            };

            await CargarCombosEditar(model);

            return View("Edit", model);
        }

        [HttpPost]
        [Authorize(Roles = "Super Usuario,Administrador,Técnico")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Editar(TicketEditViewModel model)
        {
            if (!ModelState.IsValid)
            {
                await CargarCombosEditar(model);
                return View("Edit", model);
            }

            var ticket = await _context.Tickets
                .FirstOrDefaultAsync(t => t.Id == model.Id);

            if (ticket == null)
            {
                return NotFound();
            }

            ticket.Asunto = model.Asunto;
            ticket.Descripcion = model.Descripcion;
            ticket.Justificacion = model.Justificacion;

            ticket.SistemaId = model.SistemaId;
            ticket.CategoriaId = model.CategoriaId;
            ticket.RiesgoId = model.RiesgoId;
            ticket.PrioridadId = model.PrioridadId;
            ticket.EstadoId = model.EstadoId;

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Ver), new { id = ticket.Id });
        }



        private async Task CargarCombosEditar(TicketEditViewModel model)
        {
            model.Sistemas = new SelectList(
                await _context.Sistemas
                    .Where(s => s.Activo)
                    .OrderBy(s => s.Nombre)
                    .ToListAsync(),
                "Id",
                "Nombre");

            model.Categorias = new SelectList(
                await _context.Categorias
                    .OrderBy(c => c.Nombre)
                    .ToListAsync(),
                "Id",
                "Nombre");

            model.Riesgos = new SelectList(
                await _context.Riesgos
                    .OrderBy(r => r.Nombre)
                    .ToListAsync(),
                "Id",
                "Nombre");

            model.Prioridades = new SelectList(
                await _context.Prioridades
                    .OrderBy(p => p.Nombre)
                    .ToListAsync(),
                "Id",
                "Nombre");

            model.Estados = new SelectList(
                await _context.Estados
                    .OrderBy(e => e.Nombre)
                    .ToListAsync(),
                "Id",
                "Nombre");
        }



    }
}