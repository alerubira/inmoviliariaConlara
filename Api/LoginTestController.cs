using Microsoft.AspNetCore.Mvc;

namespace InmobiliariaConlara.Api
{
    [ApiController]
    [Route("api/[controller]")]
    public class LoginTestController : ControllerBase
    {
        [HttpPost("login")]
        public IActionResult Login([FromForm] string Usuario, [FromForm] string Clave)
        {
            return Ok(new { UsuarioRecibido = Usuario, ClaveRecibida = Clave });
        }
    }
}
