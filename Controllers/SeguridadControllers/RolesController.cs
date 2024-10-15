﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using S_CIngenieria.Models;
using S_CIngenieria.Models.Seguridad;
using System.Threading.Tasks;

namespace S_CIngenieria.Controllers.SeguridadControllers
{
    [Authorize(Roles = "Administrador")]  
    public class RolesController : Controller
    {
        private readonly SyCIngenieriaContext _context;

        public RolesController(SyCIngenieriaContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var roles = _context.Roles.ToList();
            return View(roles);
        }

     
        public IActionResult Crear()
        {
            return View();
        }

       
        [HttpPost]
        public async Task<IActionResult> Crear(Roles rol)
        {
            if (!ModelState.IsValid)
            {
                ViewData["Mensaje"] = "Todos los campos obligatorios deben ser completados correctamente.";
                return View(rol);
            }

            _context.Roles.Add(rol);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }

  
        public async Task<IActionResult> Editar(int id)
        {
            var rol = await _context.Roles.FindAsync(id);
            if (rol == null)
            {
                return NotFound();
            }
            return View(rol);
        }

       
        [HttpPost]
        public async Task<IActionResult> Editar(int id, Roles rol)
        {
            if (id != rol.Id)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                ViewData["Mensaje"] = "Todos los campos obligatorios deben ser completados correctamente.";
                return View(rol);
            }

            _context.Roles.Update(rol);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }

       
        public async Task<IActionResult> Eliminar(int id)
        {
            var rol = await _context.Roles.FindAsync(id);
            if (rol == null)
            {
                return NotFound();
            }

            return View(rol);
        }

    
        [HttpPost, ActionName("Eliminar")]
        public async Task<IActionResult> ConfirmarEliminar(int id)
        {
            var rol = await _context.Roles.FindAsync(id);
            if (rol == null)
            {
                return NotFound();
            }

            _context.Roles.Remove(rol);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }
    }
}
