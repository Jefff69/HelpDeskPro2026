
using HelpDeskPro2026.Data;
using HelpDeskPro2026.Models;
using HelpDeskPro2026.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


[Authorize(Roles = "Super Usuario,Administrador,Técnico")]
public class RiesgoController : Controller
{
    private readonly IRiesgoService _riesgoService;

    public RiesgoController(IRiesgoService riesgoService)
    {
        _riesgoService = riesgoService;
    }



    // GET: RIESGOS
    public async Task<IActionResult> Index()    
    {
        var riesgos = await _riesgoService.ObtenerTodosAsync();
        return View(riesgos);
    }

    // GET: RIESGOS/Details/5
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var riesgo = await _riesgoService.ObtenerPorIdAsync(id.Value);
        if (riesgo == null)
        {
            return NotFound();
        }

        return View(riesgo);
    }

    // GET: RIESGOS/Create
    public IActionResult Create()
    {
        return View();
    }

    // POST: RIESGOS/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Color,Id,Nombre,Activo")] Riesgo riesgo)
    {
        if (ModelState.IsValid)
        {
            try
            {
                await _riesgoService.CrearAsync(riesgo);

                TempData["Success"] = "El riesgo se creó correctamente.";

                return RedirectToAction(nameof(Index));
            }
            catch (InvalidOperationException ex)
            {
                ModelState.AddModelError("", ex.Message);
            }
        }

        return View(riesgo);
    }

    // GET: RIESGOS/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var riesgo = await _riesgoService.ObtenerPorIdAsync(id.Value);
        if (riesgo == null)
        {
            return NotFound();
        }
        return View(riesgo);
    }

    // POST: RIESGOS/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int? id, [Bind("Color,Id,Nombre,Activo")] Riesgo riesgo)
    {
        if (id != riesgo.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                await _riesgoService.ActualizarAsync(riesgo);

                TempData["Success"] = "El riesgo se actualizó correctamente.";

                return RedirectToAction(nameof(Index));
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await _riesgoService.ExisteAsync(riesgo.Id))
                {
                    return NotFound();
                }

                throw;
            }
           
        }
        return View(riesgo);
    }

    // GET: RIESGOS/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var riesgo = await _riesgoService.ObtenerPorIdAsync(id.Value);
        if (riesgo == null)
        {
            return NotFound();
        }

        return View(riesgo);
    }

    // POST: RIESGOS/Delete/5
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
            await _riesgoService.EliminarAsync(id.Value);

            TempData["Success"] = "El riesgo se eliminó correctamente.";
        }
        catch (InvalidOperationException ex)
        {
            TempData["Error"] = ex.Message;
        }

        return RedirectToAction(nameof(Index));
    }


}
