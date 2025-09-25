using Microsoft.AspNetCore.Authentication.Cookies;
using InmobiliariaConlara.Models;

var builder = WebApplication.CreateBuilder(args);

// 🔹 Registrar servicios MVC y Session
builder.Services.AddControllersWithViews()
       .AddSessionStateTempDataProvider();

builder.Services.AddSession();

// 🔹 Registrar repositorios
builder.Services.AddScoped<RepositorioUsuario>();

// 🔹 Configuración de autenticación con cookies
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Account/Login";       // Página de login
        options.LogoutPath = "/Account/Logout";     // Página de logout
        options.AccessDeniedPath = "/Home/Restringido"; // Página sin permisos
        options.ExpireTimeSpan = TimeSpan.FromMinutes(20); // ⏱ Tiempo de expiración
        options.SlidingExpiration = true;           // 🔄 Extiende sesión si hay actividad
    });

// 🔹 Políticas de autorización
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("Empleado", policy => policy.RequireRole("Empleado", "Administrador"));
    options.AddPolicy("Administrador", policy => policy.RequireRole("Administrador"));
});

var app = builder.Build();

// Middleware de sesión
app.UseSession();

// Middleware de errores
app.UseMiddleware<Inmobiliaria.Middleware.ErrorHandlingMiddleware>();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

// 🔹 Orden correcto
app.UseAuthentication(); 
app.UseAuthorization();

// Rutas MVC
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Account}/{action=Login}/{id?}");

app.Run();
