using Microsoft.AspNetCore.Mvc;
using Inmobiliaria.Models;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
namespace Inmobiliaria.Controllers{
    public class ContratosController : Controller
    {
        private readonly RepositorioContratos repo;
       // private readonly RepositorioTipoInmueble repositorioTipoInmueble;
       // private readonly RepositorioPropietario repoPropietario;
        public ContratosController(IConfiguration configuration)
        {
            repo = new RepositorioContratos(configuration);
           // repositorioTipoInmueble = new RepositorioTipoInmueble(configuration);
            //repoPropietario = new RepositorioPropietario(configuration);
        }

        public IActionResult Index()
        {
            var lista = repo.ObtenerTodos();

            // Asigna el propietario a cada inmueble
           /* foreach (var inmueble in lista)
            {
                inmueble.Duenio = repoPropietario.ObtenerPorId(inmueble.IdPropietario);
                inmueble.TipoInmueble = repositorioTipoInmueble.ObtenerPorId(inmueble.IdTipoInmueble)?.Nombre;  
            }*/

            return View(lista);
        }
        public IActionResult Create()
        {
              // var contratos = repositorioTipoInmueble.ObtenerTodos();
               //ViewBag.TipoInmuebles = contratos;
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Contratos contrato)
        {
           
            if (ModelState.IsValid)
            {
                contrato.Vigente = true;
                repo.Alta(contrato);
                return RedirectToAction(nameof(Index));
            }
             //ViewBag.TipoInmuebles = repositorioTipoInmueble.ObtenerTodos();
            return View(contrato);
        }

        public IActionResult Edit(int id)
        {
            var contrato = repo.ObtenerPorId(id);
             //contrato.Duenio = repoPropietario.ObtenerPorId(contrato.IdPropietario);
            //contrato.TipoInmueble = repositorioTipoInmueble.ObtenerPorId(contrato.IdTipoInmueble)?.Nombre;
            if (contrato == null)
            {
                return NotFound();
            }
           // ViewBag.TipoInmuebles = repositorioTipoInmueble.ObtenerTodos();
            return View(contrato);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Contratos contrato)
        { 
            

            if (id != contrato.IdContrato)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                repo.Modificacion(contrato);
                return RedirectToAction(nameof(Index));
            }
           // ViewBag.TipoInmuebles = repositorioTipoInmueble.ObtenerTodos();
            return View(contrato);
        }
        public IActionResult Delete(int id)
        {
            var contrato = repo.ObtenerPorId(id);
            if (contrato == null)
            {
                return NotFound();
            }
            return View(contrato);
        }
          [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id,String bandera)
        {
            var contrato = repo.ObtenerPorId(id);
            if (contrato == null)
            {
                return NotFound();
            }
            if (id != contrato.IdContrato)
                return NotFound();

            if (ModelState.IsValid)
            {
                repo.Baja(id);
                return RedirectToAction(nameof(Index));
            }
            return View(contrato);
        }
}
}