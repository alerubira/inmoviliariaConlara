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
    public class InmueblesController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly IConfiguration _configuration;
        private readonly SeguridadService _seguridadService;
        private readonly IWebHostEnvironment _environment; // <-- agregar

        public InmueblesController(IWebHostEnvironment environment,DataContext context, IConfiguration config, SeguridadService seguridadService)
        {
            _context = context;
            _configuration = config;
            _seguridadService = seguridadService;
             _environment = environment; // <-- asignar
        }
      
    
        [HttpGet]
        public async Task<ActionResult<List<Inmuebles>>> Get()
        {
            try
            {
                string usuario = User?.Claims?.FirstOrDefault(c => c.Type == ClaimTypes.Name)?.Value ?? "";

                var propietario = await _context.Propietario.SingleOrDefaultAsync(x => x.email == usuario);
                if (propietario == null) return NotFound("Propietario no encontrado");
                var inmuebles = await (from i in _context.Inmuebles
                       join t in _context.TipoInmueble
                       on i.IdTipoInmueble equals t.IdTipoInmueble
                       where i.IdPropietario == propietario.IdPropietario
                       select new Inmuebles
                       {
                           IdInmuebles = i.IdInmuebles,
                           Direccion = i.Direccion,
                           IdPropietario = i.IdPropietario,
                           Tipo = t.Nombre,
                           imagen = i.imagen,
                           Valor = i.Valor,
                           Disponible = i.Disponible,
                           Latitud = i.Latitud,
                           Longitud = i.Longitud,
                           Ambientes = i.Ambientes,
                           Superficie = i.Superficie,
                           Existe = i.Existe,
                           IdTipoInmueble = i.IdTipoInmueble
                       }).ToListAsync();


                return Ok(inmuebles);
            }
            catch (Exception ex)
            {
                return BadRequest("desdede api :"+ex.Message);
            }
        }
         [HttpPut("actualizar")]
        public async Task<ActionResult<Inmuebles>> Actualizar([FromBody] Inmuebles datosActualizados)
        {
            try
            {
                // Obtener el email del propietario autenticado desde el token
                string usuario = User?.Identity?.Name ?? "";
               
                if (string.IsNullOrEmpty(usuario))
                    return Unauthorized("Token inválido o expirado.");

                //  Buscar el propietario actual en la base de datos
                var propietario = await _context.Propietario.FirstOrDefaultAsync(p => p.email == usuario);
                if (propietario == null)
                    return NotFound("Propietario no encontrado.");
                var idClaim=User?.Claims?.FirstOrDefault(c=>c.Type=="id")?.Value;    
                if(datosActualizados.IdPropietario.ToString() != idClaim)
                    return Unauthorized("No tienes permiso para actualizar este Inmueble.");    
                var inmueble = await _context.Inmuebles.FirstOrDefaultAsync(i => i.IdInmuebles == datosActualizados.IdInmuebles);
                if (inmueble == null)
                    return NotFound("Inmueble no encontrado.");    

                //  Actualizar los campos permitidos
                   inmueble.Disponible = datosActualizados.Disponible;


                //  Guardar cambios
                _context.Inmuebles.Update(inmueble);
                await _context.SaveChangesAsync();

                // Devolver el inmueble actualizado
                return Ok(inmueble);
            }
            catch (Exception ex)
            {
                return BadRequest("Error al actualizar: " + ex.Message);
            }
        }
        [HttpPost("cargar")]
     public async Task<ActionResult<Inmuebles>> CargarInmueble(
    [FromForm] IFormFile imagen, 
    [FromForm] string inmueble) // JSON como string
    {
            try
            {
                // Obtener propietario del token
                string usuario = User?.Identity?.Name ?? "";
                if (string.IsNullOrEmpty(usuario))
                    return Unauthorized("Token inválido o expirado.");

                var propietario = await _context.Propietario.FirstOrDefaultAsync(p => p.email == usuario);
                if (propietario == null)
                    return NotFound("Propietario no encontrado.");

                // Convertir JSON string a objeto Inmuebles
                var datosInmueble = JsonSerializer.Deserialize<Inmuebles>(inmueble);
                if (datosInmueble == null)
                    return BadRequest("Datos de inmueble inválidos.");
                datosInmueble.IdPropietario = propietario.IdPropietario;
                 _context.Inmuebles.Add(datosInmueble);
                await _context.SaveChangesAsync();

                //  insertar foto al inmueble
                   //  Guardar la imagen en wwwroot/Uploads
                string wwwPath = _environment.WebRootPath; // asegúrate de inyectar IWebHostEnvironment _environment
                string uploadPath = Path.Combine(wwwPath, "Uploads");

                if (!Directory.Exists(uploadPath))
                    Directory.CreateDirectory(uploadPath);

                // Nombre único de la imagen: "inmueble_<Id>.ext"
                string fileName = $"inmueble_{datosInmueble.IdInmuebles}{Path.GetExtension(imagen.FileName)}";
                string filePath = Path.Combine(uploadPath, fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await imagen.CopyToAsync(stream);
                }

                // Guardar la ruta relativa en el objeto Inmuebles
                datosInmueble.imagen = Path.Combine("/Uploads", fileName);

                //  Actualizar el inmueble con la URL de la imagen
                _context.Inmuebles.Update(datosInmueble);
                await _context.SaveChangesAsync();

                return Ok(datosInmueble);
            }
            catch (Exception ex)
            {
                return BadRequest("Error al cargar inmueble: " + ex.Message);
            }
    }

    }
}