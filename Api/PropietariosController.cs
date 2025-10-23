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
				
				string usuario = User?.Identity?.Name ?? "";
				
				var res = await _context.Propietario.SingleOrDefaultAsync(x => x.eMail == usuario);
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
                    .FirstOrDefault(p => p.eMail == Usuario);

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
                    new Claim(ClaimTypes.Name, propietario.eMail),
                    new Claim("id", propietario.IdPropietario.ToString())
                };

                var token = new JwtSecurityToken(
                    issuer: issuer,
                    audience: audience,
                    claims: claims,
                    expires: DateTime.Now.AddHours(1),
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
    }
}
