using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Inmobiliaria.Models;
using InmobiliariaConlara.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;
using Microsoft.AspNetCore.Hosting; // para IWebHostEnvironment


namespace InmobiliariaConlara.Api
{
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    public class ContratosController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly IConfiguration _configuration;
        private readonly SeguridadService _seguridadService;
        private readonly IWebHostEnvironment _environment; // <-- agregar

        public ContratosController(IWebHostEnvironment environment, DataContext context, IConfiguration config, SeguridadService seguridadService)
        {
            _context = context;
            _configuration = config;
            _seguridadService = seguridadService;
            _environment = environment; // <-- asignar
        }
        [HttpGet("Inmueble")]
        public async Task<ActionResult<List<Contratos>>> Get(int idInmueble)
        {
            try
            {
                if (idInmueble <= 0) return BadRequest("Id Inmueble no valido");
            
                string usuario = User?.Claims?.FirstOrDefault(c => c.Type == ClaimTypes.Name)?.Value ?? "";
                Inmuebles inmueble = await _context.Inmuebles.SingleOrDefaultAsync(x => x.IdInmuebles == idInmueble);
                if (inmueble == null) return NotFound("Inmueble no encontrado");
                var idClaim = User?.Claims?.FirstOrDefault(c => c.Type == "id")?.Value; 
                if(inmueble.IdPropietario.ToString() != idClaim)
                {
                    return Forbid("No tiene permiso para ver los contratos de este inmueble");
                }
                var propietario = await _context.Propietario.SingleOrDefaultAsync(x => x.email == usuario);
                if (propietario == null) return NotFound("Propietario no encontrado");

                var contratos = await (from i in _context.Inmuebles
                                     join t in _context.TipoInmueble on i.IdTipoInmueble equals t.IdTipoInmueble
                                     join c in _context.Contratos on i.IdInmuebles equals c.IdInmuebles
                                     where i.IdPropietario == propietario.IdPropietario
                                         && c.Vigente == true   //  solo contratos vigentes
                                         && i.IdInmuebles == idInmueble
                                     select new Contratos
                                     {
                                         IdContrato = c.IdContrato,
                                         IdInmuebles = c.IdInmuebles,
                                         IdInquilino = c.IdInquilino,
                                         DireccionInmueble = i.Direccion,
                                         NombreInquilino = (from inq in _context.Inquilino
                                                            where inq.IdInquilino == c.IdInquilino
                                                            select inq.Nombre + " " + inq.Apellido).FirstOrDefault(),
                                         FechaDesde = c.FechaDesde,
                                         FechaHasta = c.FechaHasta,
                                         Monto = c.Monto,
                                         Vigente = c.Vigente,
                                         Existe = c.Existe,
                                         CantidadCuotas = c.CantidadCuotas,
                                         CuotasPagas = c.CuotasPagas,
                                         MesInicio = c.MesInicio
                                         

                                     }).ToListAsync();


                                        return Ok(contratos);
                        }
                        catch (Exception ex)
                        {
                            return BadRequest("desdede api :"+ex.Message);
                        }
        }
    }
}
      