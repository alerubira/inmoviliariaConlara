using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using InmobiliariaConlara.Models;
using Microsoft.AspNetCore.Authorization;

namespace InmobiliariaConlara.Controllers
{
    public class AccountController : Controller
    {
        private readonly RepositorioUsuario repositorio;
        private const string GlobalSalt = "MiSaltSecreto123"; // mismo que en Create

        public AccountController(RepositorioUsuario repo)
        {
            repositorio = repo;
        }

        // GET: Account/Login
        [HttpGet]
        public IActionResult Login(string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }

        // POST: Account/Login
        [HttpPost]
        public async Task<IActionResult> Login(string email, string password)
        {
            var user = repositorio.Login(email, password);

            if (user == null)
            {
                ViewBag.Error = "Credenciales inválidas";
                return View();
            }

            // Creamos las claims
            var claims = new List<Claim>
    {
        new Claim(ClaimTypes.Name, user.Email),
        new Claim(ClaimTypes.Role, user.Rol == 1 ? "Administrador" : "Empleado") ,
        new Claim("UserId", user.IdUsuario.ToString()) //id del usuario

        // asumimos que Rol es int en tu modelo
            };

            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var principal = new ClaimsPrincipal(identity);

            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                principal,
                new AuthenticationProperties
                {
                    IsPersistent = true,
                    ExpiresUtc = DateTime.UtcNow.AddMinutes(30)
                });

            return RedirectToAction("Index", "Home");
        }


        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Logout()
        {
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Account");
        }
  
        public IActionResult Perfil()
        {
            var email = User.Identity?.Name; // lo guardamos al loguear
            if (string.IsNullOrEmpty(email))
                return RedirectToAction("Login");

            var user = repositorio.ObtenerPorEmail(email);
            if (user == null)
                return RedirectToAction("Login");

            return View(user); // <- pasa el usuario a la vista
        }
    }
}
