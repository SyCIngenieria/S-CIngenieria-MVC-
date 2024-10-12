﻿using S_CIngenieria.Models.Seguridad;
using S_CIngenieria.Models;
using Microsoft.EntityFrameworkCore;

namespace S_CIngenieria.Service
{
    public class UsuarioService : IUsuarioService
    {
        private readonly SyCIngenieriaContext _context;

        public UsuarioService(SyCIngenieriaContext context)
        {
            _context = context;
        }

        public async Task<Usuarios> GetUsuario(string NombreUsuario, string Contraseña)
        {

            string claveEncriptada = Utilidades.EncriptarClave(Contraseña);


            Usuarios usuario = await _context.Usuarios
                .Where(u => u.NombreUsuario == NombreUsuario && u.Contraseña == claveEncriptada)
                .FirstOrDefaultAsync();
            return usuario;
        }

        public async Task<Usuarios> SaveUsuario(Usuarios usuario)
        {

            _context.Usuarios.Add(usuario);
            await _context.SaveChangesAsync();
            return usuario;
        }

        public async Task<List<Roles>> GetRolesPorUsuario(int usuarioId)
        {
            return await _context.Roles
                .Where(r => _context.Usuarios
                    .Where(u => u.Id == usuarioId)
                    .Select(u => u.FkRol)
                    .Contains(r.Id))
                .ToListAsync();
        }


        public async Task<List<Permisos>> GetPermisosPorUsuario(int usuarioId)
        {

            var rolId = await _context.Usuarios
                .Where(u => u.Id == usuarioId)
                .Select(u => u.FkRol)
                .FirstOrDefaultAsync();


            return await _context.Permisos
                .Where(p => _context.RelacionalModulosRoles
                    .Where(rmr => rmr.FkRol == rolId)
                    .Select(rmr => rmr.Id)
                    .Contains(p.FkRelacionalModulosRoles))
                .ToListAsync();
        }
    }
}