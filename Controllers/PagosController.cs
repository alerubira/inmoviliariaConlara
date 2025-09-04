using Microsoft.AspNetCore.Mvc;
using Inmobiliaria.Models;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
namespace Inmobiliaria.Controllers{
    public class PagosController : Controller
    {
        private readonly RepositorioPagos repo;
        
        private readonly RepositorioContratos repositorioContratos;
        public PagosController(IConfiguration configuration)
        {
            repo = new RepositorioPagos(configuration);
    
            repositorioContratos = new RepositorioContratos(configuration);
        }

        public IActionResult Index()
        {
            var lista = repo.ObtenerTodos();

            // Asigna al pago la direccion del inmueble 
            foreach (var pago in lista)
            {
                if (pago.IdPagos.HasValue)
                {//hacer en el repositoriContratos obtenetpor idContrato la direccion del inmueble
                    Inmuebles in = repositorioInmuebles.ObtenerPorId(pago.IdInquilino.Value);
                    pago.NombreInquilino = nI != null ? nI.ToString() : "";
                }
                else
                {

                    pago.DireccionInmueble = null;
                 }
              //  contrato.NombreInquilino=RepositorioInquilino.ObtenerPorId(contrato.IdInquilino).ToString() ?? "";
                var inm = repositorioInmuebles.ObtenerPorId(pago.IdInmuebles);
                pago.DireccionInmueble = inm != null ? inm.Direccion : "";
                pago.Precio = inm != null ? inm.Precio : 0;
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