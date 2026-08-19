
using HelpDeskPro2026.Data;
using HelpDeskPro2026.Models;
using HelpDeskPro2026.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[Authorize(Roles = "Super Usuario,Administrador,Técnico")]
public class PrioridadController : Controller
{
    private readonly IPrioridadService _prioridadService;

    public PrioridadController(IPrioridadService prioridadService)
    {
        _prioridadService = prioridadService;
    }




    // GET: PRIORIDADS
    public async Task<IActionResult> Index()    
    {
        var prioridades = await _prioridadService.ObtenerTodosAsync();
        return View(prioridades);
    }

    // GET: PRIORIDADS/Details/5
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var prioridad = await _prioridadService.ObtenerPorIdAsync(id.Value);
        if (prioridad == null)
        {
            return NotFound();
        }

        return View(prioridad);
    }

    // GET: PRIORIDADS/Create
    public IActionResult Create()
    {
        return View();
    }

    // POST: PRIORIDADS/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Color,Id,Nombre,Activo")] Prioridad prioridad)
    {
        if (ModelState.IsValid)
        {
            try
            {
                await _prioridadService.CrearAsync(prioridad);

                TempData["Success"] = "La prioridad se creó correctamente.";

                return RedirectToAction(nameof(Index));
            }
            catch (InvalidOperationException ex)
            {
                ModelState.AddModelError("", ex.Message);
            }
        }

        return View(prioridad);
    }

    // GET: PRIORIDADS/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var prioridad = await _prioridadService.ObtenerPorIdAsync(id.Value);
        if (prioridad == null)
        {
            return NotFound();
        }
        return View(prioridad);
    }

    // POST: PRIORIDADS/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int? id, [Bind("Color,Id,Nombre,Activo")] Prioridad prioridad)
    {
        if (id != prioridad.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                await _prioridadService.ActualizarAsync(prioridad);

                TempData["Success"] = "La prioridad se actualizó correctamente.";

                return RedirectToAction(nameof(Index));
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await _prioridadService.ExisteAsync(prioridad.Id))
                {
                    return NotFound();
                }

                throw;
            }

        }
        return View(prioridad);
    }

    // GET: PRIORIDADS/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var prioridad = await _prioridadService.ObtenerPorIdAsync(id.Value);
        if (prioridad == null)
        {
            return NotFound();
        }

        return View(prioridad);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        try
        {
            await _prioridadService.EliminarAsync(id.Value);

            TempData["Success"] = "La prioridad se eliminó correctamente.";
        }
        catch (InvalidOperationException ex)
        {
            TempData["Error"] = ex.Message;
        }

        return RedirectToAction(nameof(Index));
    }


}
