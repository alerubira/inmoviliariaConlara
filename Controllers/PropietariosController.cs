using Microsoft.AspNetCore.Mvc;
using Inmobiliaria.Models;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;

namespace Inmobiliaria.Controllers
{
    public class PropietariosController : Controller
    {
        private readonly RepositorioPropietario repo;

        public PropietariosController(IConfiguration configuration)
        {
            repo = new RepositorioPropietario(configuration);
        }

        public IActionResult Index()
        {
            var lista = repo.ObtenerTodos();
            return View(lista);
        }
public IActionResult Create()
{
    return View();
}        
        [HttpPost]
[ValidateAntiForgeryToken]
public IActionResult Create(Propietario propietario)
{
    if (ModelState.IsValid)
    {
        repo.Alta(propietario);
        return RedirectToAction(nameof(Index));
    }
    return View(propietario);
}

        // Puedes agregar aquí las acciones Create, Edit, Eliminar, etc.
    }
}