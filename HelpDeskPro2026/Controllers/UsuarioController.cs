
using HelpDeskPro2026.Data;
using HelpDeskPro2026.Models;
using HelpDeskPro2026.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

public class UsuarioController : Controller
{
    private readonly IUsuarioService _usuarioService;
    private readonly AppDbContext _context;

    public UsuarioController(IUsuarioService usuarioService, AppDbContext context)
    {
        _usuarioService = usuarioService;
        _context = context;
    }

    // GET: USUARIOS
    public async Task<IActionResult> Index()    
    {
        var usuarios = await _usuarioService.ObtenerTodosAsync();
        return View(usuarios);
    }

    // GET: USUARIOS/Details/5
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var usuario = await _usuarioService.ObtenerPorIdAsync(id.Value);
        if (usuario == null)
        {
            return NotFound();
        }

        return View(usuario);
    }

    // GET: USUARIOS/Create
    public IActionResult Create()
    {
        return View();
    }

    // POST: USUARIOS/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Id,SupabaseUserId,Nombre,Apellidos,Correo,FotoUrl,Activo,FechaCreacion,FechaActualizacion,UltimoAcceso,RolId,Rol,NombreCompleto")] Usuario usuario)
    {
        if (ModelState.IsValid)
        {
            try
            {
                await _usuarioService.CrearAsync(usuario);

                TempData["Success"] = "El usuario se creó correctamente.";

                return RedirectToAction(nameof(Index));
            }
            catch (InvalidOperationException ex)
            {
                ModelState.AddModelError("", ex.Message);
            }
        }

        ViewData["RolId"] = new SelectList(_context.Roles, "Id", "Nombre", usuario.RolId);
        return View(usuario);
    }

    // GET: USUARIOS/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var usuario = await _usuarioService.ObtenerPorIdAsync(id.Value);
        if (usuario == null)
        {
            return NotFound();
        }
        return View(usuario);
    }

    // POST: USUARIOS/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int? id, [Bind("Id,SupabaseUserId,Nombre,Apellidos,Correo,FotoUrl,Activo,FechaCreacion,FechaActualizacion,UltimoAcceso,RolId,Rol,NombreCompleto")] Usuario usuario)
    {
        if (id != usuario.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                await _usuarioService.ActualizarAsync(usuario);

                TempData["Success"] = "El usuario se actualizó correctamente.";

                return RedirectToAction(nameof(Index));
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await _usuarioService.ExisteAsync(usuario.Id))
                {
                    return NotFound();
                }

                throw;
            }
            return RedirectToAction(nameof(Index));
        }
        return View(usuario);
    }

    // GET: USUARIOS/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var usuario = await _usuarioService.ObtenerPorIdAsync(id.Value);
        if (usuario == null)
        {
            return NotFound();
        }

        return View(usuario);
    }

    // POST: USUARIOS/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        await _usuarioService.EliminarAsync(id.Value);

        TempData["Success"] = "El usuario se eliminó correctamente.";

        return RedirectToAction(nameof(Index));
    }


}
