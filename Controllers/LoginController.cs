using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using S_CIngenieria.Models;
using S_CIngenieria.Models.Seguridad;
using S_CIngenieria.Service;
using System.Security.Claims;

namespace S_CIngenieria.Controllers
{

    public class LoginController : Controller
    {
        private readonly IUsuarioService _usuarioService;
        private readonly IFilesService _filesService;
        private readonly SyCIngenieriaContext _context;

        public LoginController(IUsuarioService usuarioService, IFilesService filesService, SyCIngenieriaContext context)
        {
            _usuarioService = usuarioService;
            _filesService = filesService;
            _context = context;
        }

        public IActionResult Registro()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Registro(Usuarios usuario, IFormFile Imagen)
        {
            if (Imagen == null || Imagen.Length == 0)
            {
                ViewData["Mensaje"] = "Debe cargar una imagen.";
                return View(usuario);
            }

            if (usuario.FkRol < 1 || usuario.FkRol > 3)
            {
                ViewData["Mensaje"] = "El rol seleccionado no es válido. Debe ser 1 (Administrador), 2 (Cliente) o 3 (Empresa).";
                return View(usuario);
            }

            Stream image = Imagen.OpenReadStream();
            string urlImagen = await _filesService.SubirArchivo(image, Imagen.FileName);

            usuario.Contraseña = Utilidades.EncriptarClave(usuario.Contraseña);
            usuario.fotoPerfil = urlImagen;
            usuario.FechaCreacion = DateTime.Now;
            usuario.FechaExpira = DateTime.Now.AddYears(1);

            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors);
                foreach (var error in errors)
                {
                    Console.WriteLine(error.ErrorMessage);
                }

                ViewData["Mensaje"] = "Todos los campos obligatorios deben ser completados correctamente.";
                return View(usuario);
            }

            Usuarios usuarioCreado = await _usuarioService.SaveUsuario(usuario);

            if (usuarioCreado.Id > 0)
            {
                return View("~/Views/Login/RegistroExitoso.cshtml");
            }

            ViewData["Mensaje"] = "No se pudo crear el usuario.";
            return View(usuario);
        }


        [HttpGet]
        public IActionResult IniciarSesion()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> IniciarSesion(string NombreUsuario, string Contraseña)
        {
            
            if (string.IsNullOrEmpty(NombreUsuario) || string.IsNullOrEmpty(Contraseña))
            {
                ViewData["Mensaje"] = "Por favor, complete todos los campos.";
                return View();
            }

            var usuario = await _usuarioService.GetUsuario(NombreUsuario, Contraseña);

            
            if (usuario == null)
            {
                ViewData["Mensaje"] = "Nombre de usuario o contraseña incorrectos.";
                return View();
            }

          
            HttpContext.Session.SetString("Usuario", usuario.NombreUsuario);
            return RedirectToAction("Index", "Home");
        }

        [HttpPost("CerrarSesion")]
        public async Task<IActionResult> CerrarSesion()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("IniciarSesion", "Login");
        }
    }
}
