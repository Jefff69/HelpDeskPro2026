
using HelpDeskPro2026.Data;
using HelpDeskPro2026.Models;
using HelpDeskPro2026.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[Authorize]
public class EstadoController : Controller
{
    private readonly IEstadoService _estadoService;

    public EstadoController(IEstadoService estadoService)
    {
        _estadoService = estadoService;
    }




    // GET: ESTADOS
    public async Task<IActionResult> Index()    
    {
        var estados = await _estadoService.ObtenerTodosAsync();
        return View(estados);
    }

    // GET: ESTADOS/Details/5
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var estado = await _estadoService.ObtenerPorIdAsync(id.Value);
        if (estado == null)
        {
            return NotFound();
        }

        return View(estado);
    }

    // GET: ESTADOS/Create
    public IActionResult Create()
    {
        return View();
    }

    // POST: ESTADOS/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Id,Nombre,Activo")] Estado estado)
    {
        if (ModelState.IsValid)
        {
            try
            {
                await _estadoService.CrearAsync(estado);

                TempData["Success"] = "El estado se creó correctamente.";

                return RedirectToAction(nameof(Index));
            }
            catch (InvalidOperationException ex)
            {
                ModelState.AddModelError("", ex.Message);
            }
        }

        return View(estado);
    }

    // GET: ESTADOS/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var estado = await _estadoService.ObtenerPorIdAsync(id.Value);
        if (estado == null)
        {
            return NotFound();
        }
        return View(estado);
    }

    // POST: ESTADOS/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int? id, [Bind("Id,Nombre,Activo")] Estado estado)
    {
        if (id != estado.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                await _estadoService.ActualizarAsync(estado);

                TempData["Success"] = "El estado se actualizó correctamente.";

                return RedirectToAction(nameof(Index));
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await _estadoService.ExisteAsync(estado.Id))
                {
                    return NotFound();
                }

                throw;
            }
            return RedirectToAction(nameof(Index));
        }
        return View(estado);
    }

    // GET: ESTADOS/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var estado = await _estadoService.ObtenerPorIdAsync(id.Value);
        if (estado == null)
        {
            return NotFound();
        }

        return View(estado);
    }

    // POST: ESTADOS/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        await _estadoService.EliminarAsync(id.Value);

        TempData["Success"] = "El estado se eliminó correctamente.";

        return RedirectToAction(nameof(Index));
    }

}
