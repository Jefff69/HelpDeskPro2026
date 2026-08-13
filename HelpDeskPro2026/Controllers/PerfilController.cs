using HelpDeskPro2026.Interfaces;
using HelpDeskPro2026.Services;
using HelpDeskPro2026.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace HelpDeskPro2026.Controllers
{
    [Authorize]
    public class PerfilController : Controller
    {
        private readonly IUsuarioService _usuarioService;
        private readonly IStorageService _storageService;

        public PerfilController(
            IUsuarioService usuarioService,
            IStorageService storageService)
        {
            _usuarioService = usuarioService;
            _storageService = storageService;
        }



        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var idClaim = User.FindFirst(ClaimTypes.NameIdentifier);

            if (idClaim == null)
                return RedirectToAction("Login", "Account");

            int usuarioId = int.Parse(idClaim.Value);

            var usuario = await _usuarioService.ObtenerPorIdAsync(usuarioId);

            if (usuario == null)
                return NotFound();

            var model = new PerfilViewModel
            {
                Id = usuario.Id,
                Nombre = usuario.Nombre,
                Apellidos = usuario.Apellidos,
                Correo = usuario.Correo,
                Activo = usuario.Activo,
                Rol = usuario.Rol?.Nombre,
                FotoUrl = usuario.FotoUrl
            };

            return View(model);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(PerfilViewModel model)
        {
            var idClaim = User.FindFirst(ClaimTypes.NameIdentifier);

            if (idClaim == null)
                return RedirectToAction("Login", "Account");

            int usuarioId = int.Parse(idClaim.Value);

            if (model.Foto != null && model.Foto.Length > 0)
            {
                var extension = Path.GetExtension(model.Foto.FileName);

                var fileName = $"{usuarioId}{extension}";

                var fotoUrl = await _storageService.UploadProfileImageAsync(
                    model.Foto,
                    fileName);

                if (string.IsNullOrEmpty(fotoUrl))
                {
                    TempData["Error"] = "No fue posible subir la fotografía.";
                    return RedirectToAction(nameof(Index));
                }

                await _usuarioService.ActualizarFotoAsync(
                    usuarioId,
                    fotoUrl);
            }

            TempData["Success"] = "Perfil actualizado correctamente.";

            return RedirectToAction(nameof(Index));
        }



    }
}