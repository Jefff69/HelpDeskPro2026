using HelpDeskPro2026.Interfaces;
using HelpDeskPro2026.Services;
using HelpDeskPro2026.ViewModels;
using Microsoft.AspNetCore.Authentication;
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

        // GET: Perfil
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


            var usuario = await _usuarioService.ObtenerPorIdAsync(usuarioId);

            if (usuario == null)
                return NotFound();

            if (!ModelState.IsValid)
            {
                model.Id = usuario.Id;
                model.Correo = usuario.Correo;
                model.Rol = usuario.Rol?.Nombre;
                model.Activo = usuario.Activo;
                model.FotoUrl = usuario.FotoUrl;

                TempData["Error"] = "Hay errores en los datos del perfil.";

                return View(model);
            }

            try
            {
                // Guardamos los datos personales
                await _usuarioService.ActualizarPerfilAsync(
                    usuarioId,
                    model.Nombre,
                    model.Apellidos);

                // Volver a cargar el usuario actualizado
                usuario = await _usuarioService.ObtenerPorIdAsync(usuarioId);

                if (usuario == null)
                {
                    return NotFound();
                }

                // Actualizar fotografía si seleccionó una nueva
                if (model.Foto != null && model.Foto.Length > 0)
                {
                    var extension = Path.GetExtension(model.Foto.FileName);

                    var fileName = $"{usuarioId}{extension}";

                    var fotoUrl = await _storageService.UploadProfileImageAsync(
                        model.Foto,
                        fileName);

                    if (string.IsNullOrEmpty(fotoUrl))
                    {
                        TempData["Error"] =
                            "El nombre y apellido se guardaron, pero no fue posible subir la fotografía.";

                        return RedirectToAction(nameof(Index));
                    }

                    await _usuarioService.ActualizarFotoAsync(
                        usuarioId,
                        fotoUrl);

                    usuario.FotoUrl = fotoUrl;
                }

                // Actualizar los datos de la sesión
                var claims = new List<Claim>
        {
            new Claim(
                ClaimTypes.NameIdentifier,
                usuario.Id.ToString()),

            new Claim(
                ClaimTypes.Name,
                usuario.NombreCompleto),

            new Claim(
                ClaimTypes.Email,
                usuario.Correo),

            new Claim(
                "FotoUrl",
                usuario.FotoUrl ?? ""),

            new Claim(
                ClaimTypes.Role,
                usuario.Rol?.Nombre ?? "Usuario")
        };

                var identity = new ClaimsIdentity(
                    claims,
                    "Cookies");

                var principal = new ClaimsPrincipal(identity);

                await HttpContext.SignInAsync(
                    "Cookies",
                    principal);

                TempData["Success"] =
                    "Los datos de tu perfil se actualizaron correctamente.";

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                TempData["Error"] =
                    "Ocurrió un error al actualizar el perfil: " + ex.Message;

                return RedirectToAction(nameof(Index));
            }
        }





    }
}