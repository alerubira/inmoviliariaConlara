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
    public class PagosController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly IConfiguration _configuration;
        private readonly SeguridadService _seguridadService;
        private readonly IWebHostEnvironment _environment; 

        public PagosController(IWebHostEnvironment environment, DataContext context, IConfiguration config, SeguridadService seguridadService)
        {
            _context = context;
            _configuration = config;
            _seguridadService = seguridadService;
            _environment = environment; 
        }
        [HttpGet("Contrato")]
        public async Task<ActionResult<List<Pagos>>> Get(int idContrato)
        {
            try
            {
                if (idContrato <= 0) return BadRequest("Id Contrato no valido");

                string usuario = User?.Claims?.FirstOrDefault(c => c.Type == ClaimTypes.Name)?.Value ?? "";
                Contratos contrato = await _context.Contratos.SingleOrDefaultAsync(x => x.IdContrato == idContrato);
                if (contrato == null) return NotFound("Contrato no encontrado");

                Inmuebles inmueble = await _context.Inmuebles.SingleOrDefaultAsync(x => x.IdInmuebles == contrato.IdInmuebles);
                
                if (inmueble == null) return NotFound("Inmueble no encontrado");
                var idClaim = User?.Claims?.FirstOrDefault(c => c.Type == "id")?.Value; 
                if(inmueble.IdPropietario.ToString() != idClaim)
                {
                    return Forbid("No tiene permiso para ver los pagos de este contrato");
                }
                var propietario = await _context.Propietario.SingleOrDefaultAsync(x => x.email == usuario);
                if (propietario == null) return NotFound("Propietario no encontrado");
              var pagos = await (
                            from p in _context.Pagos
                            join c in _context.Contratos on p.IdContratos equals c.IdContrato
                            join i in _context.Inmuebles on c.IdInmuebles equals i.IdInmuebles
                            where p.IdContratos == idContrato
                            select new
                            {
                                p.IdPagos,
                                p.IdContratos,
                                p.FechaPago,
                                p.Importe,
                                p.Concepto,
                                p.NumeroCuota,
                                DireccionInmueble = i.Direccion,
                                FechaInicioContrato = c.FechaDesde,
                                FechaFinContrato = c.FechaHasta
                            }
                        ).ToListAsync();

                       return Ok(pagos);

                        }
                        catch (Exception ex)
                        {
                            return BadRequest("desdede api :"+ex.Message);
                        }
        }
    }
}
      