using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

using InmobiliariaConlara.Models;
using DotNetEnv;
var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;
// Cargar variables del archivo .env
Env.Load("claves.env");



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
    })
    .AddJwtBearer(options =>//la api web valida con token
    {
        var secreto = configuration["TokenAuthentication:SecretKey"];
        if (string.IsNullOrEmpty(secreto))
            throw new Exception("Falta configurar TokenAuthentication:Secret");
        options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = configuration["TokenAuthentication:Issuer"],
            ValidAudience = configuration["TokenAuthentication:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(System.Text.Encoding.ASCII.GetBytes(secreto)),
        };
    });

// 🔹 Políticas de autorización
builder.Services.AddAuthorization(options =>
{
options.AddPolicy("Empleado", policy => policy.RequireRole("Empleado", "Administrador"));
options.AddPolicy("Administrador", policy => policy.RequireRole("Administrador"));
});
builder.Services.AddControllers(); // ← agrega soporte para controladores API
builder.Services.AddEndpointsApiExplorer(); // ← necesario para Swagger
//builder.Services.AddSwaggerGen(); // ← genera la documentación


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
app.MapControllers();
var endpointDataSource = app.Services.GetRequiredService<Microsoft.AspNetCore.Routing.EndpointDataSource>();
foreach (var endpoint in endpointDataSource.Endpoints)
{
    Console.WriteLine(endpoint.DisplayName);
}


app.Run();
