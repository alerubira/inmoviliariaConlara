using Microsoft.AspNetCore.Mvc;
using Inmobiliaria.Models;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
namespace Inmobiliaria.Controllers{
    public class InmueblesController : Controller
    {
        private readonly RepositorioInmuebles repo;
        private readonly RepositorioTipoInmueble repositorioTipoInmueble;

        public InmueblesController(IConfiguration configuration)
        {
            repo = new RepositorioInmuebles(configuration);
            repositorioTipoInmueble = new RepositorioTipoInmueble(configuration);
        }

        public IActionResult Index()
        {
            var lista = repo.ObtenerTodos();
            return View(lista);
        }
        public IActionResult Create()
        {
               var tiposInmuebles = repositorioTipoInmueble.ObtenerTodos();
               ViewBag.TipoInmuebles = tiposInmuebles;
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Inmuebles inmueble)
        {
            if (ModelState.IsValid)
            {
                repo.Alta(inmueble);
                return RedirectToAction(nameof(Index));
            }
            return View(inmueble);
        }

        public IActionResult Edit(int id)
        {
            var inmueble = repo.ObtenerPorId(id);
            if (inmueble == null)
            {
                return NotFound();
            }
            return View(inmueble);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Inmuebles inmueble)
        {
            if (id != inmueble.IdInmueble)
                return NotFound();

            if (ModelState.IsValid)
            {
                repo.Modificacion(inmueble);
                return RedirectToAction(nameof(Index));
            }
            return View(inmueble);
        }
        public IActionResult Delete(int id)
        {
            var inmueble = repo.ObtenerPorId(id);
            if (inmueble == null)
            {
                return NotFound();
            }
            return View(inmueble);
        }
          [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id,String bandera)
        {
            var inmueble = repo.ObtenerPorId(id);
            if (inmueble == null)
            {
                return NotFound();
            }
            if (id != inmueble.IdInmueble)
                return NotFound();

            if (ModelState.IsValid)
            {
                repo.Baja(id);
                return RedirectToAction(nameof(Index));
            }
            return View(inmueble);
        }
}
}