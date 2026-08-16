
using HelpDeskPro2026.Data;
using HelpDeskPro2026.Models;
using HelpDeskPro2026.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[Authorize(Roles = "Super Usuario,Administrador,Técnico")]
public class SistemaController : Controller
{
    private readonly ISistemaService _sistemaService;

    public SistemaController(ISistemaService sistemaService)
    {
        _sistemaService = sistemaService;
    }

    // GET: SISTEMAS
    public async Task<IActionResult> Index()
    {
        var sistemas = await _sistemaService.ObtenerTodosAsync();
        return View(sistemas);
    }

    // GET: SISTEMAS/Details/5
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var sistema = await _sistemaService.ObtenerPorIdAsync(id.Value);

        if (sistema == null)
        {
            return NotFound();
        }

        return View(sistema);
    }
    // GET: SISTEMAS/Create
    public IActionResult Create()
    {
        return View();
    }

    // POST: SISTEMAS/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Codigo,Id,Nombre,Activo")] Sistema sistema)
    {
        if (ModelState.IsValid)
        {
            try
            {
                await _sistemaService.CrearAsync(sistema);

                TempData["Success"] = "El sistema se creó correctamente.";

                return RedirectToAction(nameof(Index));
            }
            catch (InvalidOperationException ex)
            {
                ModelState.AddModelError("", ex.Message);
            }
        }

        return View(sistema);
    }

    // GET: SISTEMAS/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var sistema = await _sistemaService.ObtenerPorIdAsync(id.Value);

        if (sistema == null)
        {
            return NotFound();
        }

        return View(sistema);
    }

    // POST: SISTEMAS/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int? id, [Bind("Codigo,Id,Nombre,Activo")] Sistema sistema)
    {
        if (id != sistema.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                await _sistemaService.ActualizarAsync(sistema);

                TempData["Success"] = "El sistema se actualizó correctamente.";

                return RedirectToAction(nameof(Index));
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await _sistemaService.ExisteAsync(sistema.Id))
                {
                    return NotFound();
                }

                throw;
            }
        }

        return View(sistema);
    }




    // GET: SISTEMAS/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var sistema = await _sistemaService.ObtenerPorIdAsync(id.Value);
        if (sistema == null)
        {
            return NotFound();
        }

        return View(sistema);
    }

    // POST: SISTEMAS/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        await _sistemaService.EliminarAsync(id.Value);

        TempData["Success"] = "El sistema se eliminó correctamente.";

        return RedirectToAction(nameof(Index));
    }
}
