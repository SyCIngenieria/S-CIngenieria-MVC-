using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using S_CIngenieria.Models;
using S_CIngenieria.Models.Seguridad;
using S_CIngenieria.Service;
using System.Security.Claims;


namespace S_CIngenieria.Controllers.SeguridadControllers
{
    public class UsuariosController : Controller
    {
        private readonly IUsuarioService _usuarioService;
        private readonly IFilesService _filesService;
        private readonly SyCIngenieriaContext _context;

        public UsuariosController(IUsuarioService usuarioService, IFilesService filesService, SyCIngenieriaContext context)
        {
            _usuarioService = usuarioService;
            _filesService = filesService;
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var usuarios = await _usuarioService.GetAllUsuarios(); 
            return View(usuarios);
        }




        public async Task<IActionResult> Editar(int id)
        {
            var usuarios = await _context.Usuarios.FindAsync(id);
            if (usuarios == null)
            {
                return NotFound();
            }
            return View(usuarios);
        }


        [HttpPost]
        public async Task<IActionResult> Editar(int id, Usuarios usuarios, IFormFile Imagen)
        {
            if (id != usuarios.Id)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                ViewData["Mensaje"] = "Todos los campos obligatorios deben ser completados correctamente.";
                return View(usuarios);
            }
            if (Imagen == null || Imagen.Length == 0)
            {
                ViewData["Mensaje"] = "Debe cargar una imagen.";
                return View(usuarios);
            }

            if (usuarios.FkRol < 1 || usuarios.FkRol > 3)
            {
                ViewData["Mensaje"] = "El rol seleccionado no es válido. Debe ser 1 (Administrador), 2 (Cliente) o 3 (Empresa).";
                return View(usuarios);
            }

            Stream image = Imagen.OpenReadStream();
            string urlImagen = await _filesService.SubirArchivo(image, Imagen.FileName);

            _context.Usuarios.Update(usuarios);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }




        [HttpPost]
        public async Task<IActionResult> Eliminar(int id)
        {
            var resultado = await _usuarioService.DeleteUsuario(id); 
            if (!resultado)
            {
                ViewData["Mensaje"] = "No se pudo eliminar el usuario.";
                return RedirectToAction(nameof(Index));
            }

            return RedirectToAction(nameof(Index));
        }


        public IActionResult Crear()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Crear(Usuarios usuario, IFormFile Imagen)
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

    }
}
