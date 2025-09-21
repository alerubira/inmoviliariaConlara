using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using InmobiliariaConlara.Models;

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
            var user = repositorio.Login(email,password);

    if (user == null)
    {
        ViewBag.Error = "Credenciales inválidas";
        return View();
    }

    // Creamos las claims
    var claims = new List<Claim>
    {
        new Claim(ClaimTypes.Name, user.Email),
        new Claim(ClaimTypes.Role, user.Rol == 1 ? "Administrador" : "Empleado") 
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

    // Redirección según rol
    if (user.Rol == 1)
        return RedirectToAction("Index", "LayoutAdmin");
    else
        return RedirectToAction("Index", "LayoutEmpleado");
}


        // GET: Account/Logout
        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Account");
        }
    }
}
