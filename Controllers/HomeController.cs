using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using S_CIngenieria.Models;
using S_CIngenieria.Models.Seguridad;
using S_CIngenieria.Service;
using System.Security.Claims;
using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;

namespace S_CIngenieria.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUsuarioService _usuarioService;

        public HomeController(ILogger<HomeController> logger, IUsuarioService usuarioService)
        {
            _logger = logger;
            _usuarioService = usuarioService;
        }

        public async Task<IActionResult> Index()
        {
            Usuarios usuario = null;
            ClaimsPrincipal claimsUser = HttpContext.User;

            if (claimsUser.Identity.IsAuthenticated)
            {
                // Obtener el nombre del usuario desde los claims
                string nombreUsuario = claimsUser.Claims
                    .Where(c => c.Type == ClaimTypes.Name)
                    .Select(c => c.Value).SingleOrDefault();

                // Obtener el usuario completo de la base de datos
                usuario = await _usuarioService.GetUsuarioPorNombre(nombreUsuario);

                if (usuario != null)
                {
                    ViewData["nombreUsuario"] = usuario.Nombres + " " + usuario.Apellidos; // Concatenar nombres y apellidos
                    ViewData["fotoPerfil"] = usuario.fotoPerfil; // Foto de perfil
                }
            }

            // Si no se encuentra el usuario, puedes agregar un mensaje de error si lo deseas
            if (usuario == null)
            {
                ViewData["Mensaje"] = "Usuario no encontrado.";
            }

            return View(usuario); // Pasar el objeto usuario a la vista
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpPost]
        public async Task<IActionResult> CerrarSesion()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("IniciarSesion", "Login");
        }
        [AllowAnonymous]
        public IActionResult AccesoDenegado()
        {
            return View();
        }
    }
}

