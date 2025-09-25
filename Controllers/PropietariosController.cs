using Microsoft.AspNetCore.Mvc;
using Inmobiliaria.Models;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;

namespace Inmobiliaria.Controllers
{
    public class PropietariosController : Controller
    {
        private readonly RepositorioPropietario repo;

        public PropietariosController(IConfiguration configuration)
        {
            repo = new RepositorioPropietario(configuration);
        }

        [Authorize(Roles="Administrador,Empleado")]
        public IActionResult Index()
        {
            var lista = repo.ObtenerTodos();
            return View(lista);
        }

        [Authorize(Roles="Administrador,Empleado")]
        public IActionResult Create()
        {
            return View();
        }

        [Authorize(Roles="Administrador,Empleado")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Propietario propietario)
        {
            var p=repo.ObtenerPorDni(propietario.Dni);
            if (p != null)
            {
                ModelState.AddModelError("Dni", "Ya existe un propietario con este DNI.");
                return View(propietario);
            }
            if (ModelState.IsValid)
            {
                repo.Alta(propietario);
                return RedirectToAction(nameof(Index));
            }
            return View(propietario);
        }

        [Authorize(Roles="Administrador,Empleado")]
        public IActionResult Edit(int id)
        {
            var propietario = repo.ObtenerPorId(id);
            if (propietario == null)
            {
                return NotFound();
            }
            return View(propietario);
        }

        [Authorize(Roles="Administrador,Empleado")]
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

        [Authorize(Roles="Administrador")]
        public IActionResult Delete(int id)
        {
            var propietario = repo.ObtenerPorId(id);
            if (propietario == null)
            {
                return NotFound();
            }
            return View(propietario);
        }


        [Authorize(Roles ="Administrador,Empleado")]
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
