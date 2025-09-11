var builder = WebApplication.CreateBuilder(args);

// 🔹 Registrar todos los servicios primero
builder.Services.AddControllersWithViews()
       .AddSessionStateTempDataProvider(); // TempData via Session

builder.Services.AddSession(); // Habilita Session

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

// Rutas MVC
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
