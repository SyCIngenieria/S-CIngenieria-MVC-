using S_CIngenieria.Models.Seguridad;

namespace S_CIngenieria.Service
{
    public interface IUsuarioService
    {
        Task<Usuarios> GetUsuario(string NombreUsuario, string Contraseña);
        Task<Usuarios> SaveUsuario(Usuarios usuario);
        Task<List<Permisos>> GetPermisosPorUsuario(int usuarioId);
        Task<string?> GetRolNombrePorUsuario(int usuarioId);
        Task<Usuarios> GetUsuarioPorNombre(string nombreUsuario);
        Task<IEnumerable<Usuarios>> GetAllUsuarios(); 
        Task<Usuarios> GetUsuarioById(int id);
        Task<IEnumerable<Roles>> GetRoles(); 
        Task UpdateUsuario(Usuarios usuario); 
        Task<bool> DeleteUsuario(int id);
    }
}