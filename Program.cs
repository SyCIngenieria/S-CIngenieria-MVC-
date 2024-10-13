using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using S_CIngenieria.Models;
using S_CIngenieria.Service;

namespace S_CIngenieria
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            // Configurar la base de datos
            builder.Services.AddDbContext<SyCIngenieriaContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            // Inyectar servicios personalizados
            builder.Services.AddScoped<IUsuarioService, UsuarioService>();
            builder.Services.AddScoped<IFilesService, FilesService>();

            // Configurar autenticación con cookies
            builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options =>
                {
                    options.LoginPath = "/Login/IniciarSesion";  // Página de inicio de sesión
                    options.ExpireTimeSpan = TimeSpan.FromMinutes(20);  // Tiempo de expiración de la cookie
                });

            // Registrar cache distribuida para las sesiones
            builder.Services.AddDistributedMemoryCache();  // Necesario para almacenar datos de sesión

            // Configuración de sesiones
            builder.Services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(30);  // Duración de la sesión
                options.Cookie.HttpOnly = true;  // Solo accesible a través de HTTP, mejora la seguridad
                options.Cookie.IsEssential = true;  // Necesario para asegurar que la sesión funcione bajo RGPD
            });

            // Filtro para evitar almacenamiento en caché de las respuestas
            builder.Services.AddControllersWithViews(options =>
            {
                options.Filters.Add(new ResponseCacheAttribute
                {
                    NoStore = true,
                    Location = ResponseCacheLocation.None,
                });
            });

            var app = builder.Build();

            // Configurar el pipeline HTTP
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            // Habilitar autenticación y autorización
            app.UseAuthentication();
            app.UseAuthorization();

            // Habilitar el uso de sesiones
            app.UseSession();

            // Configurar las rutas
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Login}/{action=IniciarSesion}/{id?}");

            app.Run();
        }
    }
}