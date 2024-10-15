using S_CIngenieria.Models.Seguridad;
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
        public async Task<Usuarios> GetUsuarioPorNombre(string nombreUsuario)
        {
            return await _context.Usuarios
                .Include(u => u.Rol)
                .FirstOrDefaultAsync(u => u.NombreUsuario == nombreUsuario);
        }

        public async Task<Usuarios> SaveUsuario(Usuarios usuario)
        {

            _context.Usuarios.Add(usuario);
            await _context.SaveChangesAsync();
            return usuario;
        }

        public async Task<string?> GetRolNombrePorUsuario(int usuarioId)
        {
            return await (from u in _context.Usuarios
                          join r in _context.Roles on u.FkRol equals r.Id
                          where u.Id == usuarioId
                          select r.nombre).FirstOrDefaultAsync();
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
        public async Task<IEnumerable<Usuarios>> GetAllUsuarios()
        {
            return await _context.Usuarios.ToListAsync();
        }
        public async Task<Usuarios> GetUsuarioById(int id)
        {
            return await _context.Usuarios.FindAsync(id);
        }


        public async Task<IEnumerable<Roles>> GetRoles()
        {
            return await _context.Roles.ToListAsync();
        }
        public async Task UpdateUsuario(Usuarios usuario)
        {
            _context.Usuarios.Update(usuario);
            await _context.SaveChangesAsync();
        }


        public async Task<bool> DeleteUsuario(int id)
        {
            var usuario = await _context.Usuarios.FindAsync(id);
            if (usuario == null) return false;

            _context.Usuarios.Remove(usuario);
            await _context.SaveChangesAsync();
            return true;
        }

    }
}