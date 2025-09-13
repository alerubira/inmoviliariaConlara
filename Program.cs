using Microsoft.AspNetCore.Authentication.Cookies;
var builder = WebApplication.CreateBuilder(args);

// 🔹 Registrar todos los servicios primero
builder.Services.AddControllersWithViews()
       .AddSessionStateTempDataProvider(); // TempData via Session

builder.Services.AddSession(); // Habilita Session
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>//el sitio web valida con cookie
    {
        options.LoginPath = "/Usuarios/Login";
        options.LogoutPath = "/Usuarios/Logout";
        options.AccessDeniedPath = "/Home/Restringido";
        //options.ExpireTimeSpan = TimeSpan.FromMinutes(5);//Tiempo de expiración
    });
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("Empleado", policy => policy.RequireRole("Empleado", "Administrador"));
    options.AddPolicy("Administrador", policy => policy.RequireRole("Administrador"));
});
  

// 🔹 Construir la app
var app = builder.Build();

// Middleware de sesión debe ir antes de UseRouting
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
app.UseAuthorization();
app.UseAuthentication();

// Rutas MVC
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
