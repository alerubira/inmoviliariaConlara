using Microsoft.AspNetCore.Mvc;
using Inmobiliaria.Models;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
namespace Inmobiliaria.Controllers{
    public class InmueblesController : Controller
    {
        private readonly RepositorioInmuebles repo;
        private readonly RepositorioTipoInmueble repositorioTipoInmueble;
        private readonly RepositorioPropietario repoPropietario;
        public InmueblesController(IConfiguration configuration)
        {
            repo = new RepositorioInmuebles(configuration);
            repositorioTipoInmueble = new RepositorioTipoInmueble(configuration);
            repoPropietario = new RepositorioPropietario(configuration);
        }

        public IActionResult Index()
        {
            var lista = repo.ObtenerTodos();

            // Asigna el propietario a cada inmueble
            foreach (var inmueble in lista)
            {
                inmueble.Duenio = repoPropietario.ObtenerPorId(inmueble.IdPropietario);
                inmueble.TipoInmueble = repositorioTipoInmueble.ObtenerPorId(inmueble.IdTipoInmueble)?.Nombre;
            }

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
                inmueble.Habilitado = true;
                repo.Alta(inmueble);
                return RedirectToAction(nameof(Index));
            }
            ViewBag.TipoInmuebles = repositorioTipoInmueble.ObtenerTodos();
            return View(inmueble);
        }

        public IActionResult Edit(int id)
        {
            var inmueble = repo.ObtenerPorId(id);
            inmueble.Duenio = repoPropietario.ObtenerPorId(inmueble.IdPropietario);
            inmueble.TipoInmueble = repositorioTipoInmueble.ObtenerPorId(inmueble.IdTipoInmueble)?.Nombre;
            if (inmueble == null)
            {
                return NotFound();
            }
            ViewBag.TipoInmuebles = repositorioTipoInmueble.ObtenerTodos();
            return View(inmueble);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Inmuebles inmueble)
        {


            if (id != inmueble.IdInmueble)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                repo.Modificacion(inmueble);
                return RedirectToAction(nameof(Index));
            }
            ViewBag.TipoInmuebles = repositorioTipoInmueble.ObtenerTodos();
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
        public IActionResult Delete(int id, String bandera)
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
        public IActionResult BuscarInmueblePorFraccionDireccion( String term)
        {
              if (string.IsNullOrEmpty(term) || term.Length < 3)
                    {
                        return Json(new { success = false, data = new List<object>() });
                    }

                 var lista = repo.BuscarPorFraccionDireccion(term);

                 var resultado = lista.Select(i => new
                 {
                     id = i.IdInmueble,
                     direccion = i.Direccion,
                     precio=i.Precio
                });

                return Json(new { success = true, data = resultado });
        }
}
}