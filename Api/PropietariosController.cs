using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Inmobiliaria.Models; // ajustá el namespace
using InmobiliariaConlara.Services;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
namespace InmobiliariaConlara.Api
{
    [ApiController]
    [Route("api/[controller]")]
    public class PropietariosController : ControllerBase
    {
        private readonly IConfiguration configuration;
        private readonly RepositorioPropietario repo; // tu clase que accede a la BD
        private readonly SeguridadService seguridadService = new SeguridadService();

        public PropietariosController(IConfiguration configuration)
        {
            this.configuration = configuration;
            this.repo = new RepositorioPropietario(configuration);
        }

        [HttpPost("login")]
        public IActionResult Login([FromForm] string Usuario, [FromForm] string Clave)
        {
           /* {


                return Ok(new { UsuarioRecibido = Usuario, ClaveRecibida = Clave });

            }*/
            try
            {
                // Buscar propietario por email
                var propietario = repo.ObtenerPorEmail(Usuario);
                if (propietario == null)
                    return Unauthorized("El Usuario no existe");

                //var hashed = seguridadService.HashearContraseña(Clave);
                var hashed = seguridadService.HashearContraseña(Clave).Trim();
                var claveGuardada = propietario.Clave.Trim();

                // Comparar hash con el guardado
                if (hashed != claveGuardada)
                {
                    return Unauthorized("Usuario o clave incorrectos "+hashed+" - "+propietario.Clave);
                }
                    

                // Si todo ok, generar el token
                var secretKey = configuration["TokenAuthentication:SecretKey"];
                var issuer = configuration["TokenAuthentication:Issuer"];
                var audience = configuration["TokenAuthentication:Audience"];

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
                return BadRequest("desde api" + ex.Message);
            }
        }
        }
    }

