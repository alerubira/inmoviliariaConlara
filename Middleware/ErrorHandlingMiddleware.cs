using System.Net;
using Microsoft.AspNetCore.Http;
using System.Web;

namespace Inmobiliaria.Middleware
{
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;

        public ErrorHandlingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                // Codificamos el mensaje para la URL
                var mensaje = HttpUtility.UrlEncode(ex.Message);
                context.Response.Redirect($"/Home/Error?mensaje={mensaje}");
            }
        }
    }
}
