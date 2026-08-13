using HelpDeskPro2026.Interfaces;
using HelpDeskPro2026.Models;
using HelpDeskPro2026.Services;
using HelpDeskPro2026.ViewModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace HelpDeskPro2026.Controllers
{
    public class AccountController : Controller
    {
        private readonly ISupabaseAuthService _authService;
        private readonly IUsuarioService _usuarioService;

        public AccountController(
            ISupabaseAuthService authService,
            IUsuarioService usuarioService)
        {
            _authService = authService;
            _usuarioService = usuarioService;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View(new LoginViewModel());
        }


        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var supabaseUser = await _authService.SignInAsync(model.Email, model.Password);

            if (supabaseUser == null)
            {
                ModelState.AddModelError("", "Correo o contraseña incorrectos.");
                return View(model);
            }

            var usuario = await _usuarioService.ObtenerPorSupabaseUserIdAsync(supabaseUser.Id);

            if (usuario == null)
            {
                ModelState.AddModelError("", "El usuario no está registrado en el sistema.");
                return View(model);
            }

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, usuario.Id.ToString()),
                new Claim(ClaimTypes.Name, usuario.NombreCompleto),
                new Claim(ClaimTypes.Email, usuario.Correo),
                new Claim("FotoUrl", usuario.FotoUrl ?? ""),
                new Claim(ClaimTypes.Role, usuario.Rol?.Nombre ?? "Usuario")
            };

            var identity = new ClaimsIdentity(
                claims,
                CookieAuthenticationDefaults.AuthenticationScheme);

            var principal = new ClaimsPrincipal(identity);

            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                principal);

            return RedirectToAction("Index", "Home");
        }


        [Authorize]
        public async Task<IActionResult> Logout()
        {
            await _authService.SignOutAsync();

            await HttpContext.SignOutAsync(
                CookieAuthenticationDefaults.AuthenticationScheme);

            return RedirectToAction("Login");
        }

    }
}