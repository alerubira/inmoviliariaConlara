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

        public IActionResult Edit(int id)
        {
            var propietario = repo.ObtenerPorId(id);
            if (propietario == null)
            {
                return NotFound();
            }
            return View(propietario);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Propietario propietario)
        {
            if (id != propietario.IdPropietario)
                return NotFound();

            if (ModelState.IsValid)
            {
                repo.Modificacion(propietario);
                return RedirectToAction(nameof(Index));
            }
            return View(propietario);
        }
        public IActionResult Delete(int id)
        {
            var propietario = repo.ObtenerPorId(id);
            if (propietario == null)
            {
                return NotFound();
            }
            return View(propietario);
        }
        
        [HttpGet]
        public IActionResult BuscarPropietarioPorFraccionApellido( String term)
        {
              if (string.IsNullOrEmpty(term) || term.Length < 3)
                    {
                        return Json(new { success = false,message = "Ingrese al menos tres caracteres", data = new List<object>() });
                    }

                 var lista = repo.BuscarPorFraccionApellido(term);
                    if (lista == null || lista.Count == 0)
                        {
                            return Json(new { success = false, message = "No se encontraron Propietarios.", data = new List<object>() });
                        }
                 var resultado = lista.Select(p => new
                        {
                            id = p.IdPropietario,
                            texto = p.ToString() // Usa el override
                        });

                return Json(new { success = true, data = resultado });
        }
        
}
    }
