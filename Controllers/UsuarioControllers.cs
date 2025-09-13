using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;
using InmobiliariaConlara.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Net.Http.Headers;
//using Newtonsoft.Json.Serialization;

namespace InmobiliariaConlara.Controllers
{
    public class UsuarioController : Controller
    {
        private readonly IConfiguration configuration;
        private readonly IWebHostEnvironment environment;
        private readonly RepositorioUsuario repositorio;

        public UsuarioController(IConfiguration configuration, IWebHostEnvironment environment, RepositorioUsuario repositorio)
        {
            this.configuration = configuration;
            this.environment = environment;
            this.repositorio = repositorio;
        }
        // GET: Usuarios
        //[Authorize(Policy = "Administrador")]
        public ActionResult Index()
        {
            var usuarios = repositorio.ObtenerTodos();
            return View(usuarios);
        }
        // GET: Usuarios/Details/5
		/*[Authorize(Policy = "Administrador")]
		public ActionResult Details(int id)
		{
			var e = repositorio.ObtenerPorId(id);
			return View(e);
		}*/

		// GET: Usuarios/Create
		//[Authorize(Policy = "Administrador")]
		public ActionResult Create()
		{
			ViewBag.Roles = Usuario.ObtenerRoles();
			return View();
		}

		// POST: Usuarios/Create
		[HttpPost]
		[ValidateAntiForgeryToken]
		//[Authorize(Policy = "Administrador")]
		public ActionResult Create(Usuario u)
		{
            if (!ModelState.IsValid)
            {
                ViewBag.Roles = Usuario.ObtenerRoles();
                return View();
            }
				
			
			
				string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
								password: u.Clave,
								salt: System.Text.Encoding.ASCII.GetBytes(configuration["Salt"]),
								prf: KeyDerivationPrf.HMACSHA1,
								iterationCount: 1000,
								numBytesRequested: 256 / 8));
				u.Clave = hashed;
				u.Rol = User.IsInRole("Administrador") ? u.Rol : (int)enRoles.Empleado;
				//var nbreRnd = Guid.NewGuid();//posible nombre aleatorio
				int res = repositorio.Alta(u);
				/*if (u.AvatarFile != null && u.IdUsuario > 0)
				{
					string wwwPath = environment.WebRootPath;
					string path = Path.Combine(wwwPath, "Uploads");
					if (!Directory.Exists(path))
					{
						Directory.CreateDirectory(path);
					}
					//Path.GetFileName(u.AvatarFile.FileName);//este nombre se puede repetir
					string fileName = "avatar_" + u.IdUsuario + Path.GetExtension(u.AvatarFile.FileName);
					string pathCompleto = Path.Combine(path, fileName);
					u.Avatar = Path.Combine("/Uploads", fileName);
					// Esta operación guarda la foto en memoria en la ruta que necesitamos
					using (FileStream stream = new FileStream(pathCompleto, FileMode.Create))
					{
						u.AvatarFile.CopyTo(stream);
					}
					repositorio.Modificacion(u);
				}*/
				return RedirectToAction(nameof(Index));
			
			
				//ViewBag.Roles = Usuario.ObtenerRoles();
				//return View();
			
		}

    }
}