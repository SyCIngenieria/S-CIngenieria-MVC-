using System.ComponentModel.DataAnnotations.Schema;

namespace S_CIngenieria.Models.Seguridad
{
    public class RelacionalModulosRoles
    {
        public int Id { get; set; }

        [ForeignKey("Modulos")]
        public int FkModulo { get; set; }

        [ForeignKey("Roles")]
        public int FkRol { get; set; }


        public Modulos? Modulo { get; set; }
        public Roles? Rol { get; set; }

        public ICollection<Permisos> Permisos { get; set; } = new List<Permisos>();
    }
}
