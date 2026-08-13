using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HelpDeskPro2026.Controllers
{
    [Authorize]
    public class CatalogoController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}