using Microsoft.AspNetCore.Mvc;
using Inmobiliaria.Models;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
namespace Inmobiliaria.Controllers{
    public class PagosController : Controller
    {
        private readonly RepositorioPagos repo;
        
        private readonly RepositorioContratos repositorioContratos;
       // private readonly RepositorioInmuebles repositorioInmuebles;
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
                var contrato = repositorioContratos.obtenerDireccionPrecioInmueblePorIdContrato(pago.IdContratos);
              
                pago.DireccionInmueble = contrato != null ? contrato.DireccionInmueble : "";
                pago.Importe = contrato != null ? (contrato.Precio ?? 0) : 0;
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
        public IActionResult Create(Pagos pago)
        {
           
            if (ModelState.IsValid)
            {
               
                repo.Alta(pago);
                return RedirectToAction(nameof(Index));
            }
             //ViewBag.TipoInmuebles = repositorioTipoInmueble.ObtenerTodos();
            return View(pago);
        }

        public IActionResult Edit(int id)
        {
            var pago = repo.ObtenerPorId(id);
           
            if (pago == null)
            {
                return NotFound();
            }
             
                var contrato = repositorioContratos.obtenerDireccionPrecioInmueblePorIdContrato(pago.IdContratos);
                pago.DireccionInmueble = contrato != null ? contrato.DireccionInmueble : "";
            //    pago.Importe =contrato != null ? (contrato.Precio ?? 0) : 0;

           // ViewBag.TipoInmuebles = repositorioTipoInmueble.ObtenerTodos();
            return View(pago);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Pagos pago)
        { 
            

            if (id != pago.IdPagos)
            {
                return NotFound();
            }
           

            if (ModelState.IsValid)
            {
              
                repo.Modificacion(pago);
                return RedirectToAction(nameof(Index));
            }
           // ViewBag.TipoInmuebles = repositorioTipoInmueble.ObtenerTodos();
            return View(pago);
        }
        public IActionResult Delete(int id)
        {
            var pago = repo.ObtenerPorId(id);
            if (pago == null)
            {
                return NotFound();
            }
            return View(pago);
        }
          [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id,String bandera)
        {
            var pago = repo.ObtenerPorId(id);
            if (pago == null)
            {
                return NotFound();
            }
            if (id != pago.IdPagos)
                return NotFound();

            if (ModelState.IsValid)
            {
                repo.Baja(id);
                return RedirectToAction(nameof(Index));
            }
            return View(pago);
        }
}
}