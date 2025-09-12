using Microsoft.AspNetCore.Mvc;
using Inmobiliaria.Models;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;

namespace Inmobiliaria.Controllers
{
    public class InquilinosController : Controller
    {
        private readonly RepositorioInquilino repo;

        public InquilinosController(IConfiguration configuration)
        {
            repo = new RepositorioInquilino(configuration);
        }

        /*public IActionResult Index()
        {
            var lista = repo.ObtenerTodos();
            return View(lista);
        }*/
        public IActionResult Index(int pageNumber = 1, int pageSize = 5)
            {
                var lista = repo.ObtenerPaginado(pageNumber, pageSize);
                var totalRegistros = repo.ContarInquilinos();
                var totalPaginas = (int)Math.Ceiling((double)totalRegistros / pageSize);

                ViewBag.PaginaActual = pageNumber;
                ViewBag.TotalPaginas = totalPaginas;

                return View(lista);
            }

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Inquilino inquilino)
        {
            var i=repo.ObtenerPorDni(inquilino.Dni);
            if (i != null)
            {
                ModelState.AddModelError("Dni", "Ya existe un inquilino con este DNI.");
                return View(inquilino);
            }
            if (ModelState.IsValid)
            {
                repo.Alta(inquilino);
                return RedirectToAction(nameof(Index));
            }
            return View(inquilino);
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
        public IActionResult Edit(int id, Inquilino inquilino)
        {
            if (id != inquilino.IdInquilino)
                return NotFound();

            if (ModelState.IsValid)
            {
                repo.Modificacion(inquilino);
                return RedirectToAction(nameof(Index));
            }
            return View(inquilino);
        }
        public IActionResult Delete(int id)
        {
            var inquilino = repo.ObtenerPorId(id);
            if (inquilino == null)
            {
                return NotFound();
            }
            return View(inquilino);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id, String bandera)
        {
            var inquilino = repo.ObtenerPorId(id);
            if (inquilino == null)
            {
                return NotFound();
            }
            if (id != inquilino.IdInquilino)
                return NotFound();

            if (ModelState.IsValid)
            {
                repo.Baja(id);
                return RedirectToAction(nameof(Index));
            }
            return View(inquilino);
        }
         [HttpGet]
        public IActionResult BuscarInquilinoPorFraccionApellido( String term)
        {
              if (string.IsNullOrEmpty(term) || term.Length < 3)
                    {
                        return Json(new { success = false, data = new List<object>() });
                    }

                 var lista = repo.BuscarPorFraccionApellido(term);
                 if(lista==null||lista.Count==0)
                 {
                    return Json(new { success = false, message = "No se encontraron Inquilinos.", data = new List<object>() });
                 }

                 var resultado = lista.Select(i => new
                        {
                            id =i.IdInquilino,
                            texto = i.ToString() // Usa el override
                        });

                return Json(new { success = true, data = resultado });
        }
}
    }
