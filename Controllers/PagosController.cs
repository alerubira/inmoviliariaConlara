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
        public IActionResult Create(int id)
        {
            var contrato=repositorioContratos.ObtenerPorId(id);
            if (contrato == null)
            {
                return NotFound();
            }
           var pago=new Pagos();
              pago.IdContratos=id;
              pago.Importe=contrato.Monto;
              pago.FechaPago=DateTime.Now;
              pago.NumeroCuota = contrato.CuotasPagas + 1;
              pago.MesPago=contrato.MesInicio+pago.NumeroCuota -1;
              if (pago.MesPago>12) pago.MesPago=pago.MesPago -12;
              pago.Concepto="Alquiler mes :"+ ((enMeses)pago.MesPago).ToString();
            
          
            return View(pago);
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
        public IActionResult Delete(int id, String bandera)

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

        public IActionResult PorInquilino(int? id)
        {
            if (id == null || id <= 0)
            {
                return BadRequest();
            }

            var lista = repo.obtenerPorInquilino(id.Value);

            Console.WriteLine($"ID: {id.Value} - Cantidad de pagos: {lista.Count}");

            // Asigna al pago la direccion del inmueble 
            foreach (var pago in lista)
            {
                var contrato = repositorioContratos.obtenerDireccionPrecioInmueblePorIdContrato(pago.IdContratos);

                pago.DireccionInmueble = contrato != null ? contrato.DireccionInmueble : "";
                pago.Importe = contrato != null ? (contrato.Precio ?? 0) : 0;
            }
            ViewBag.IdInquilino = id.Value;
            return View("PorInquilino", lista);
        }

    }

}