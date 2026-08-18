
using HelpDeskPro2026.Data;
using HelpDeskPro2026.Models;
using HelpDeskPro2026.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[Authorize(Roles = "Super Usuario,Administrador,Técnico")]
public class CategoriaController : Controller
{
    private readonly ICategoriaService _categoriaService;

    public CategoriaController(ICategoriaService categoriaService)
    {
        _categoriaService = categoriaService;
    }

    // GET: CATEGORIAS
    public async Task<IActionResult> Index()
    {
        var categorias = await _categoriaService.ObtenerTodosAsync();
        return View(categorias);
    }

    // GET: CATEGORIAS/Details/5
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var categoria = await _categoriaService.ObtenerPorIdAsync(id.Value);

        if (categoria == null)
        {
            return NotFound();
        }

        return View(categoria);
    }

    // GET: CATEGORIAS/Create
    public IActionResult Create()
    {
        return View();
    }

    // POST: CATEGORIAS/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Id,Nombre,Activo")] Categoria categoria)
    {
        if (ModelState.IsValid)
        {
            try
            {
                await _categoriaService.CrearAsync(categoria);

                TempData["Success"] = "La categoría se creó correctamente.";

                return RedirectToAction(nameof(Index));
            }
            catch (InvalidOperationException ex)
            {
                ModelState.AddModelError("", ex.Message);
            }
        }

        return View(categoria);
    }

    // GET: CATEGORIAS/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var categoria = await _categoriaService.ObtenerPorIdAsync(id.Value);

        if (categoria == null)
        {
            return NotFound();
        }

        return View(categoria);
    }

    // POST: CATEGORIAS/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int? id, [Bind("Id,Nombre,Activo")] Categoria categoria)
    {
        if (id != categoria.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                await _categoriaService.ActualizarAsync(categoria);

                TempData["Success"] = "La categoría se actualizó correctamente.";

                return RedirectToAction(nameof(Index));
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await _categoriaService.ExisteAsync(categoria.Id))
                {
                    return NotFound();
                }

                throw;
            }
        }
        return View(categoria);
    }

    // GET: CATEGORIAS/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var categoria = await _categoriaService.ObtenerPorIdAsync(id.Value);

        if (categoria == null)
        {
            return NotFound();
        }

        return View(categoria);
    }

    // POST: CATEGORIAS/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        await _categoriaService.EliminarAsync(id.Value);

        TempData["Success"] = "La categoría se eliminó correctamente.";

        return RedirectToAction(nameof(Index));
    }


}
