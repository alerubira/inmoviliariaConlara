using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using InmobiliariaConlara.Models;
using Microsoft.AspNetCore.Authorization;

namespace Inmobiliaria.Controllers
{
    [Authorize] 
    public class HomeController : Controller
    {
        [AllowAnonymous] // sin restriccion
        public IActionResult Restringido()
        {
            return View();
        } 

        [Authorize(Roles = "Administrador")] // solo admin
        public IActionResult SoloAdmin()
        {
            return Content("📌 Solo el administrador puede ver esta página.");
        }

        [Authorize(Roles = "Empleado,Administrador")] // los dos roles
        public IActionResult SoloEmpleado()
        {
            return Content("📌 Empleados y administradores pueden ver esta página.");
        }

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
