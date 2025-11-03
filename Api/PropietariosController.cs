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

namespace InmobiliariaConlara.Api
{
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    public class PropietariosController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly IConfiguration _configuration;
        private readonly SeguridadService _seguridadService;

        public PropietariosController(DataContext context, IConfiguration config, SeguridadService seguridadService)
        {
            _context = context;
            _configuration = config;
            _seguridadService = seguridadService;
        }
[HttpGet]
		public async Task<ActionResult<Propietario>> Get()
		{
			try
            {
				
				//string usuario = User?.Identity?.Name ?? "";
                string usuario = User?.Claims?.FirstOrDefault(c => c.Type == ClaimTypes.Name)?.Value ?? "";

				
				var res = await _context.Propietario.SingleOrDefaultAsync(x => x.email == usuario);
				return Ok(res);
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}
        [HttpPost("login")]
        [AllowAnonymous]
        public IActionResult Login([FromForm] string Usuario, [FromForm] string Clave)
        {
            try
            {
                var propietario = _context.Propietario
                    .FirstOrDefault(p => p.email == Usuario);

                if (propietario == null)
                    return Unauthorized("El Usuario no existe");

                var hashed = _seguridadService.HashearContraseña(Clave).Trim();
                var claveGuardada = propietario.Clave.Trim();

                if (hashed != claveGuardada)
                    return Unauthorized("Usuario o clave incorrectos");

                var secretKey = _configuration["TokenAuthentication:SecretKey"];
                var issuer = _configuration["TokenAuthentication:Issuer"];
                var audience = _configuration["TokenAuthentication:Audience"];

                var key = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(secretKey));
                var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                var claims = new[]
                {
                    new Claim(ClaimTypes.Name, propietario.email),
                    new Claim("id", propietario.IdPropietario.ToString())
                };

                var token = new JwtSecurityToken(
                    issuer: issuer,
                    audience: audience,
                    claims: claims,
                    expires: DateTime.Now.AddHours(5),
                    signingCredentials: creds
                );

                var tokenString = new JwtSecurityTokenHandler().WriteToken(token);

                return Ok(tokenString);
            }
            catch (Exception ex)
            {
                return BadRequest("desde api: " + ex.Message);
            }
        }
        [HttpPut("actualizar")]
        public async Task<ActionResult<Propietario>> Actualizar([FromBody] Propietario datosActualizados)
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
                    return Unauthorized("No tienes permiso para actualizar este propietario.");    

                //  Actualizar los campos permitidos
                propietario.Nombre = datosActualizados.Nombre;
                propietario.Apellido = datosActualizados.Apellido;
                propietario.Telefono = datosActualizados.Telefono;
                propietario.Dni = datosActualizados.Dni;


                //  Si quisieras actualizar la clave:
                // if (!string.IsNullOrEmpty(datosActualizados.Clave))
                //     propietario.Clave = _seguridadService.HashearContraseña(datosActualizados.Clave);

                //  Guardar cambios
                _context.Propietario.Update(propietario);
                await _context.SaveChangesAsync();

                // Devolver el propietario actualizado
                return Ok(propietario);
            }
            catch (Exception ex)
            {
                return BadRequest("Error al actualizar: " + ex.Message);
            }
        }
         [HttpPut("cambiarClave")]
        public async Task<IActionResult> CambiarClave([FromForm] string claveActual, [FromForm] string claveNueva)
        {
            try
            {
                // Obtener el email del propietario desde el token
                var email = User?.Identity?.Name;
                if (string.IsNullOrEmpty(email))
                    return Unauthorized("Usuario no autenticado.");

                // Buscar al propietario
                var propietario = await _context.Propietario.SingleOrDefaultAsync(p => p.email == email);
                if (propietario == null)
                    return NotFound("Propietario no encontrado.");

                // Verificar contraseña actual
                var hashedClaveActual = _seguridadService.HashearContraseña(claveActual).Trim();
                if (hashedClaveActual != propietario.Clave.Trim())
                    return Unauthorized("La contraseña actual es incorrecta.");

                // Actualizar a la nueva contraseña
                propietario.Clave = _seguridadService.HashearContraseña(claveNueva).Trim();
                _context.Propietario.Update(propietario);
                await _context.SaveChangesAsync();

                return NoContent(); // Devuelve 204, sin contenido
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
