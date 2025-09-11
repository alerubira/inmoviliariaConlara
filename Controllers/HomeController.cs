using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using InmobiliariaConlara.Models;

namespace Inmobiliaria.Controllers
{
    public class HomeController : Controller
    {
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error(string? mensaje)
        {
            if (!string.IsNullOrEmpty(mensaje))
            {
                TempData["Error"] = mensaje;
            }

            return View(new ErrorViewModel
            {
                RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier
            });
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}

