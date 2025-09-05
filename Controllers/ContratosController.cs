using Microsoft.AspNetCore.Mvc;
using Inmobiliaria.Models;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
namespace Inmobiliaria.Controllers{
    public class ContratosController : Controller
    {
        private readonly RepositorioContratos repo;
        private readonly RepositorioInquilino repositorioInquilino;
        private readonly RepositorioInmuebles repositorioInmuebles;
        public ContratosController(IConfiguration configuration)
        {
            repo = new RepositorioContratos(configuration);
            repositorioInquilino = new RepositorioInquilino(configuration);
            repositorioInmuebles = new RepositorioInmuebles(configuration);
        }

        public IActionResult Index()
        {
            var lista = repo.ObtenerTodos();

            // Asigna el contrato el nombre del inquilino , la direccion del inmueble y el precio del inmueble a cada contrato
            foreach (var contrato in lista)
            {
                if (contrato.IdInquilino.HasValue)
                {
                    Inquilino nI = repositorioInquilino.ObtenerPorId(contrato.IdInquilino.Value);
                    contrato.NombreInquilino = nI != null ? nI.ToString() : "";
                }
                else
                {
                    contrato.NombreInquilino = "";
                }
                //  contrato.NombreInquilino=RepositorioInquilino.ObtenerPorId(contrato.IdInquilino).ToString() ?? "";
                var inm = repositorioInmuebles.ObtenerPorId(contrato.IdInmuebles);
                contrato.DireccionInmueble = inm != null ? inm.Direccion : "";
                contrato.Precio = inm != null ? inm.Precio : 0;
            }

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

            if (contrato == null)
            {
                return NotFound();
            }
            if (contrato.IdInquilino.HasValue)
            {
                Inquilino nI = repositorioInquilino.ObtenerPorId(contrato.IdInquilino.Value);
                contrato.NombreInquilino = nI != null ? nI.ToString() : "";
            }
            else
            {
                contrato.NombreInquilino = "";
            }
            //  contrato.NombreInquilino=RepositorioInquilino.ObtenerPorId(contrato.IdInquilino).ToString() ?? "";
            var inm = repositorioInmuebles.ObtenerPorId(contrato.IdInmuebles);
            contrato.DireccionInmueble = inm != null ? inm.Direccion : "";
            contrato.Precio = inm != null ? inm.Precio : 0;

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
                contrato.Vigente = true;
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
        public IActionResult Delete(int id, String bandera)
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
         public IActionResult buscarPorFraccionDireccion( String term)
        {
              if (string.IsNullOrEmpty(term) || term.Length < 3)
                    {
                        return Json(new { success = false, data = new List<object>() });
                    }

                 var lista = repo.buscarPorFraccionDireccion(term);

                 var resultado = lista.Select(i => new
                        {
                            id =i.IdContrato,
                            Direccion = i.DireccionInmueble,
                            Precio=i.Precio
                        });

                return Json(new { success = true, data = resultado });
        }
}
}