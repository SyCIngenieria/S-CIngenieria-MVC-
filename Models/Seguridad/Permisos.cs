using System.ComponentModel.DataAnnotations.Schema;

namespace S_CIngenieria.Models.Seguridad
{
    public class Permisos
    {
        public int Id { get; set; }

        [ForeignKey("RelacionalModulosRoles")]
        public int FkRelacionalModulosRoles { get; set; }

        public RelacionalModulosRoles? RelacionalModulosRoles { get; set; }

        public bool Lectura { get; set; }
        public bool Editar { get; set; }
        public bool Actualizar { get; set; }
        public bool Insertar { get; set; }
        public bool Eliminar { get; set; }
        public bool Exportar { get; set; }
        public bool Importar { get; set; }
    }
}
